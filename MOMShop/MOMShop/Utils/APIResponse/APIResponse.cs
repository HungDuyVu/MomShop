using MOMShop.Dto.Users;
using MOMShop.Entites;

namespace MOMShop.Utils.APIResponse
{
    public class APIResponse
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

        public APIResponse() { }

        public APIResponse(object data, string message)
        {
            Data = data;
            Message = message;
        }


        public APIResponse(object data)
        {
            Data = data;
            Message = "ok";
        }

        public APIResponse(string message)
        {
            Message = message;
        }
    }
}
