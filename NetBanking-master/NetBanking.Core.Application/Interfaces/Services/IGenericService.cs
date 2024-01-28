namespace NetBanking.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel, Model>
        where SaveViewModel : class
        where ViewModel : class
        where Model : class
    {
        Task<SaveViewModel> Add(SaveViewModel vm);
        Task Delete(int id);
        Task<List<ViewModel>> GetAllViewModel();
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task Update(SaveViewModel vm, int id);
    }
}