using DrugStore.Models;
using DrugStore.Models.Entities;

namespace DrugStore.Services;
public interface IVnPayService
{
    string CreatePaymentUrl(HoaDon model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);
}