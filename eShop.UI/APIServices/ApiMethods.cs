using eShop.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShop.UI.APIServices
{
    public class ApiMethods
    {
        public async Task<bool> PostAsync<T>(string controllerName,T obj,string jwtAuth) where T: BaseEntity
        {
            using (HttpClient client = new HttpClient())
            {
                

                client.BaseAddress = new Uri("https://localhost:44371/api/" + controllerName);
                //Thelw to jwt token pou epistrefete otan kanei success login o xrhsths 
                //Θα το βρω στο HttpContext λογικα στον Controller και θα το περάσω εδώ
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", );
                var jsonObject = JsonConvert.SerializeObject(obj);
                var buffer = System.Text.Encoding.UTF8.GetBytes(jsonObject);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                if (!String.IsNullOrWhiteSpace(jwtAuth))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtAuth);

                var task = await client.PostAsync(client.BaseAddress,byteContent);

                return task.IsSuccessStatusCode;
                //if (task.IsSuccessStatusCode)
                //{
                //    //var responseContent = await task.Content.ReadAsStringAsync();
                //    //var result = JsonConvert.DeserializeObject<T>(responseContent);
                //    //return result;
                //}                
            }

            //return null;
        }
    }
}
