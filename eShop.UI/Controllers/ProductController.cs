using eShop.DataAccess;
using eShop.DataAccess.IRepositories;
using eShop.Model;
using eShop.UI.APIServices;
using eShop.UI.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductUnitOfWork _productRepository;
        private readonly IImageUnitOfWork _imageUnitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private ApiMethods apiMethods;

        public ProductController(ICategoryRepository categoryRepository, IProductUnitOfWork productRepository,
            IImageUnitOfWork imageUnitOfWork,
            IWebHostEnvironment hostingEnvironment)
        {
            this._categoryRepository = categoryRepository;
            this._productRepository = productRepository;
            this._imageUnitOfWork = imageUnitOfWork;
            this._hostingEnvironment = hostingEnvironment;
            apiMethods = new ApiMethods();
        }
        public IActionResult Home()
        {
            ProductVM productVM = new ProductVM();
            var categories = _categoryRepository.GetAll().ToList();

            return View("Home", new ProductVM()
            {
                Categories = categories.Select(cat => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = cat.Name,
                    Value = cat.Id.ToString(),
                    Selected = false
                }).ToList()
            });
        }

        public async Task<IActionResult> HomeV2()
        {
            ProductVM productVM = new ProductVM();
            var categories = _categoryRepository.GetAll().ToList();

            var products = await this._productRepository.ProductRepository.GetAllAsync();
            ProductListVM productListVM;
            foreach (var prod in products)
            {
                //object obj = new { ProductId = prod.Id, Name = prod.Name, Price = prod.Price, Quantity = prod.StockQty, ImagePath = String.Empty }; 
                productListVM = new ProductListVM { Name = prod.Name, Price = prod.Price, ProductId = prod.Id, Quantity = prod.StockQty };
                var res = await this._imageUnitOfWork.ImageDbRepository.GetImagesByProductIdAsync(prod.Id);
                var firstImageOfList = res.FirstOrDefault();
                if (firstImageOfList != null)
                {
                    productListVM.ImagePath = firstImageOfList.ImagePath;                    
                }
                productVM.TableData.Add(productListVM);
            }
            var cat = categories.Select(cat => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = cat.Name,
                Value = cat.Id.ToString(),
                Selected = false
            }).ToList();
            productVM.Categories = cat;
            return View("HomeV2", productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductVM productVM)
        {
            var jwtCookie = HttpContext.Request.Cookies["JWTCookie"];
            //var jwt = cookies.FirstOrDefault(x => x.Key == "JWTCookie");
            Product product = new Product();
            List<string> fileNames = new List<string>();

            if (productVM.Images.Count > 0)
            {
                foreach (var image in productVM.Images)
                {
                    var imageName = await UploadImage(image);
                    fileNames.Add(imageName);
                }
            }
            if (product != null)
            {
                
                product.CategoryId = productVM.CategoryId;
                product.Name = productVM.Name;
                product.Price = productVM.Price;
                product.StockQty = productVM.StockQty;
                product.Code = productVM.Code;
                product.SetInStock(productVM.StockQty);
                foreach (var name in fileNames)
                {
                    //Image image = new Image();
                    //image.ProductId = result.Id;
                    //image.ImagePath = name;
                    //var send = await apiMethods.PostAsync<Image>()

                    product.SetImages(String.Empty,name);
                }
            }

            var result = await apiMethods.PostAsync<Product>("Products", product, !String.IsNullOrWhiteSpace(jwtCookie) ? jwtCookie : null);
                                    
            if (result)
                return RedirectToAction("Home", nameof(Product));
            else
            {
                TempData.Add("ErrorMessage", "Αποτυχία στην αποθήκευση του προϊόντος.");
                return RedirectToAction("Home", nameof(Product));
            }
        }

        //[HttpGet]
        //public IActionResult GetData()
        //{
        //    try
        //    {
        //        var data = _productRepository.GetAll();
        //        return Json(new { data });
        //    }
        //    catch(Exception ex)
        //    {
        //        throw;
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> LoadData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                var start = HttpContext.Request.Form["start"].FirstOrDefault();

                var length = HttpContext.Request.Form["length"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"]
                    .FirstOrDefault() + "][name]"].FirstOrDefault();

                // Sort Column Direction (asc, desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;

                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                var productsData = _productRepository.ProductRepository.GetQueryable();

                ////Sorting  
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    productsData = productsData.OrderBy(sortColumn + " " + sortColumnDirection);
                //}
                Image image;
                foreach (var product in productsData)
                {
                    image = new Image();
                    image = await this._imageUnitOfWork.ImageDbRepository.GetByIdAsync(product.Id);
                }

                if (!string.IsNullOrEmpty(searchValue))
                {
                    productsData = productsData.Where(m => m.Name == searchValue || m.Name.Contains(searchValue));
                }

                //total number of rows counts   
                recordsTotal = productsData.Count();
                //Paging   
                var data = productsData.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<string> UploadImage(IFormFile image)
        {
            string uniqueFileName = String.Empty;
            if (image != null)
            {
                string filePath = "";
                List<string> filesNames = new List<string>();
                try
                {
                    uniqueFileName = GetUniqueFileName(image.FileName);
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    filePath = Path.Combine(uploads, uniqueFileName);


                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return uniqueFileName;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

    }
}
