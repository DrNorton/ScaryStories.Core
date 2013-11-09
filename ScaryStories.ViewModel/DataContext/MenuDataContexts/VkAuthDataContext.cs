using System;
using System.Windows;
using System.Windows.Navigation;

using Microsoft.Phone.Controls;

using ScaryStories.Services;
using ScaryStories.ViewModel.DataContext.Base;

using VkontakteDataLayer;

using VkontakteInfrastructure.Interfaces;
using VkontakteInfrastructure.Model;

using VkontakteServiceLayer;
using VkontakteCore;

namespace ScaryStories.ViewModel.DataContext.MenuDataContexts
{
    public class VkAuthDataContext : IContext
    {
        private VkService _vkService;
        private Action<string> _onAuthCompleteAction;

        public VkAuthDataContext(VkService service) {
            VkService = service;
        }

        public void Run()
        {
          
            
        }

        public VkService VkService {
            get {
                return _vkService;
            }
            set {
                _vkService = value;
            }
        }

        public string DataContextCode
        {
            get { return "VkAuth"; }
        }
    }
}
