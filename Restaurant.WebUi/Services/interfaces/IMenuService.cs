using Restaurant.WebUi.ViewModel;

namespace Restaurant.WebUi.Services.interfaces
{
    public interface IMenuService
    {
        Task<List<MenuCategoryViewModel>> GetMenuAsync();
        Task<HomeIndexViewModel> GetHomeDataAsync();
    }
}
