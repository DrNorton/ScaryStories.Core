using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using VkontakteCore;
using VkontakteDataLayer;
using VkontakteInfrastructure.Interfaces;
using VkontakteViewModel.Resources;
using VkontakteViewModel.Services;

namespace VkontakteViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        static AppResource appResource = new AppResource();

        public AppResource Resource
        {
            get { return appResource; }
        }

        public bool IsOpenedFromPinned
        {
            get
            {
                return GetStateOrUrlParamNullable("pinToStart")=="1";
            }
        }

        public void OnPropertyChange(string propertyName)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        private static IVkontakteApi vkontakteApi = null;
        protected T GetService<T>() where T: class
        {
            if(typeof(T)==typeof(IEntityDataStorage))
            {
                return new EntityStorage() as T;
            }

            if (typeof(T) == typeof(IVkontakteApi))
            {
                if (vkontakteApi == null) vkontakteApi = new VkontakteApi(GetService<IEntityDataStorage>());
                return vkontakteApi as T;
            }

            if(typeof(T)==typeof(ISimpleNavigationService))
            {
                return new SimpleNavigationService() as T;
            }

            if (typeof(T) == typeof(ICommonErrorHandler))
            {
                return new CommonErrorHandler(GetService<IVkontakteApi>(), GetService<ISimpleNavigationService>()) as T;
            }
            throw new Exception("service not found");
        }



        public string GetStateOrUrlParam(string key)
        {
            var result = GetStateOrUrlParamNullable(key);
            if (result == null) throw new Exception("Key " + key + " not found");
            return result;
        }

        public string GetStateOrUrlParamNullable(string key)
        {
            if (PageState.ContainsKey(key))
            {
                return (string)PageState[key];
            }
            if (ApplicationPage.NavigationContext.QueryString.ContainsKey(key))
            {
                return ApplicationPage.NavigationContext.QueryString[key];
            }
            return null;
        }

        public T GetState<T>(string key)
        {
            if(PageState.ContainsKey(key))
            {
                return (T)PageState[key];
            }
            return default(T);
        }


        protected IDictionary<string, object> PageState
        {
            get { return ApplicationPage.State; }
        }

        public bool IsPortraitOrientation
        {
            get
            {
                return Orientation == PageOrientation.Portrait || 
                    Orientation == PageOrientation.PortraitUp ||
                    Orientation == PageOrientation.PortraitDown;
            }
        }

        public bool IsLandscapeOrientation
        {
            get
            {
                return Orientation == PageOrientation.Landscape ||
                    Orientation == PageOrientation.LandscapeLeft ||
                    Orientation == PageOrientation.LandscapeRight;
            }
        }

        protected PageOrientation Orientation
        {
            get { return ((PhoneApplicationFrame) Application.Current.RootVisual).Orientation; }
        }

        protected Dispatcher Dispatcher
        {
            get { return Deployment.Current.Dispatcher; }
        }

        protected PhoneApplicationPage ApplicationPage;

        public virtual void OnNavigatedTo(PhoneApplicationPage page, NavigationEventArgs e)
        {
            
            ApplicationPage = page;
        }

        public virtual void OnNavigatedFrom(PhoneApplicationPage page, NavigationEventArgs e)
        {
            
        }
    }
}
