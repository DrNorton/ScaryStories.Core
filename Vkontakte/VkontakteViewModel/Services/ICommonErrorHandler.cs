using VkontakteInfrastructure.Model;

namespace VkontakteViewModel.Services
{
    public interface ICommonErrorHandler
    {
        bool HandleError(Error error);
    }
}