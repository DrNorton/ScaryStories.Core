using System;
using System.Windows;
using System.Windows.Controls;
using VkontakteCore;
using VkontakteInfrastructure.Model;

namespace VkontakteViewModel.Services
{
    class CommonErrorHandler : ICommonErrorHandler
    {
        private readonly IVkontakteApi vkontakteApi;
        private readonly Application application;
        private readonly ISimpleNavigationService navigationService;

        public CommonErrorHandler(IVkontakteApi vkontakteApi, ISimpleNavigationService navigationService)
        {
            this.vkontakteApi = vkontakteApi;
            this.application = application;
            this.navigationService = navigationService;
        }

        public bool HandleError(Error error)
        {
            if (error.ErrorCode == "5")
            {
                vkontakteApi.DeleteContext();
                navigationService.NavigatToAuthorizationPage();
                return true;
            }
            return false;
        }
    }
}