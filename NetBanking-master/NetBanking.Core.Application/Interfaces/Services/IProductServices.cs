using NetBanking.Core.Application.ViewModel.Products;

namespace NetBanking.Core.Application.Interfaces.Services
{
    public interface IProductServices
    {
        Task<ProductVM> GetAllProducts();
        Task<DashBoardStatitics> GetDashBoard();
    }
}