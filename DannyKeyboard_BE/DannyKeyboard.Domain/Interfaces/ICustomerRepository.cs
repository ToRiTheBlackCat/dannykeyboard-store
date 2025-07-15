using DannyKeyboard.Domain.Entities;

namespace DannyKeyboard.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task InsertCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
    }
}
