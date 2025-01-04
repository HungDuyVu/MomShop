using MOMShop.Utils.APIResponse;
using MOMShop.Utils.Payment;
using System.Threading.Tasks;

namespace MOMShop.Services.Interfaces.PaymentService
{
    public interface IPaymentService
    {
        void ReceiveNotify(PaymentRequestModel input);
    }
}
