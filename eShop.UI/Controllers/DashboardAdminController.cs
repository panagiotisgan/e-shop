using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using eShop.DataAccess;
using eShop.DataAccess.AdditionalDetailsModels;
using eShop.DataAccess.IRepositories;
using eShop.DataAccess.IServices;
//SOS To Reference sto Model prepei na fugei
using eShop.UI.ViewModels;
using eShop.WebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Web;
using eShop.DataAccess.DTOs;

namespace eShop.UI.Controllers
{
    [Authorize(Roles = "User")]
    public class DashboardAdminController : Controller
    {
        private readonly UserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly ICredentialRepository _credentialRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ILoginService _loginService;
        public DashboardAdminController(IUserRepository userRepository, ICredentialRepository credentialRepository,
            ICountryRepository countryRepository, IStateRepository stateRepository, ICityRepository cityRepository,ILoginService loginService)
        {
            this._userRepository = userRepository;
            this._credentialRepository = credentialRepository;
            this._countryRepository = countryRepository;
            this._stateRepository = stateRepository;
            this._cityRepository = cityRepository;
            this._loginService = loginService;
            this._userService = new UserService(_userRepository, _credentialRepository,
                _countryRepository, _stateRepository, _cityRepository);
            //this.countries = _countryRepository.GetAll().ToList();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            CreateUserVM createUserVM = new CreateUserVM();
            //List<Country> countries = countryServices.GetCountries().ToList();
            var countries = await _countryRepository.GetAllAsync();

            createUserVM.Countries = countries.Select(country => new SelectListItem
            {
                Text = country.Name,
                Value = country.Id.ToString(),
                Selected = false
            }).ToList();

            return View(createUserVM);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult UserLogin()
        {


            return View("UserLogin");
        }

        [AllowAnonymous]
        [HttpPost()]
        [ValidateAntiForgeryToken]
        public IActionResult UserLogin(AuthenticateCredentials authenticate)
        {
            var responseMessage = GetAuthentication(authenticate);

            if (responseMessage.Result.IsAuthenticated)
            {
                //Request.HttpContext.
                //var res = GetRequest<IEnumerable<User>>("Users", $"{responseMessage.Result.Token}");

                #region ADD JWToken To Cookie

                SetCookie("JWTCookie", responseMessage.Result.Token, 60);
                
                #endregion

                //HttpContext.Session.SetString("JWToken", responseMessage.Result.Token);
                return Redirect("~/DashboardAdmin/Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("JWTCookie");
            return Redirect("~/DashboardAdmin/UserLogin");
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult Home()
        {
            if(User.HasClaim(x => x.Type == ClaimTypes.Name))
            {
                //TO DO Anloga me to claim na ferw ta stoixeia tou xrhsth
            }
            
            return View();
        }

        private async Task<AuthenticationResultDTO> GetAuthentication(AuthenticateCredentials authenticate)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44371/api/Users/");
                var jsonObject = JsonConvert.SerializeObject(authenticate);
                var buffer = System.Text.Encoding.UTF8.GetBytes(jsonObject);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                AuthenticationResultDTO userAuthentiateResult = new AuthenticationResultDTO();

                var responseTask = await client.PostAsync("authenticate", byteContent);

                string responseContent = String.Empty;


                if (responseTask.IsSuccessStatusCode)
                {

                    responseContent = await responseTask.Content.ReadAsStringAsync();
                    userAuthentiateResult = JsonConvert.DeserializeObject<AuthenticationResultDTO>(responseContent);

                    //result.Content.ReadAsStringAsync<>()

                    return userAuthentiateResult;
                }
                else
                {
                    return userAuthentiateResult;
                }

            }
        }


        private void SetCookie(string key,string value,int? expireTimeToMinute)
        {
            CookieOptions cookieOptions = new CookieOptions();
            if (expireTimeToMinute.HasValue)
                cookieOptions.Expires = DateTime.Now.AddMinutes(expireTimeToMinute.Value);
            else
                cookieOptions.Expires = DateTime.Now.AddMilliseconds(10);
            
            Response.Cookies.Append(key, value, cookieOptions);
        }

        //todo na φτιάξω τα Headers έχω σπίτι παράδειγμα που παίζει
        //private async Task<T> GetRequest<T>(string controllerUrl, string token, string methodName = "") where T : class
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        //HttpResponseMessage result = new HttpResponseMessage();
        //        client.BaseAddress = new Uri("https://localhost:44371/api/");

        //        if (String.IsNullOrWhiteSpace(methodName))
        //        {
        //            var response = client.GetAsync($"{controllerUrl}").Result;
        //            var result = await response.Content.ReadAsStringAsync();
        //            var initialObject = JsonConvert.DeserializeObject<T>(result);
        //            return initialObject;
                    
        //        }
        //        else
        //        {
        //            var response = client.GetAsync($"{controllerUrl}+/{methodName}").Result;
        //            var result = await response.Content.ReadAsStringAsync();
        //            var initialObject = JsonConvert.DeserializeObject<T>(result);
        //            return initialObject;
        //        }

        //        //HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, client.BaseAddress);
        //        //httpRequestMessage.Headers.Accept.Add("application-json");
        //    }
        //}
    }
}