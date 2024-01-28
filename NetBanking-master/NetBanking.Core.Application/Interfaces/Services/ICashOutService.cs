using NetBanking.Core.Application.ViewModel.AvancedeEfectivo;

namespace NetBanking.Core.Application.Interfaces.Services
{
    public interface ICashOutService
    {
        Task<SaveCashOutViewModel> MakeAvance(SaveCashOutViewModel model);
    }
}
