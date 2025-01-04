using Microsoft.AspNetCore.Mvc;
using MOMShop.Dto.Customer;
using MOMShop.Dto.Users;
using System.Collections.Generic;
using System;
using MOMShop.Utils.APIResponse;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace MOMShop.Controllers
{
    [Route("api/info")]
    [ApiController]
    public class GHNController : ControllerBase
    {
        [HttpGet("get-province")]
        public async Task<APIResponse> GetAll()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        // Đặt header Content-Type và token
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "4b6bd573-15bf-11ee-8506-6ead57e9219a");
                        // Gọi đến API và nhận về dữ liệu
                        HttpResponseMessage response = await client.GetAsync("https://services-staging.ghtklab.com/services/shipment/order/?ver=1.5");

                        // Kiểm tra phản hồi từ API
                        if (response.IsSuccessStatusCode)
                        {
                            // Đọc dữ liệu nhận được từ API
                            string jsonData = await response.Content.ReadAsStringAsync();
                            return new APIResponse(jsonData);
                        }
                        else
                        {
                            // Xử lý lỗi nếu có
                            Console.WriteLine("Lỗi trong quá trình gọi API: " + response.StatusCode);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Xử lý ngoại lệ nếu có
                        Console.WriteLine("Lỗi: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new APIResponse("ok");
        }
    }
}
