
using System;
using System.Windows;
using System.Windows.Navigation;

using VkontakteCore;

using VkontakteDataLayer;

using VkontakteInfrastructure.Interfaces;
using VkontakteInfrastructure.Model;

using VkontakteServiceLayer;
namespace ScaryStories.Services
{
    public interface IVkService
    {
        event Action OnBack;
        event Action<Action<string>> OnLoginDemand;
        bool ClientOnAutorization { get; set; }
        bool IsAuthorized { get; }
        Uri GetUri();
        void ParseUri(Uri uri);
        void RestoreContext();
        void SentToWall(string text);
    }

    public class VkService : IVkService
    {
        public event Action OnBack;
        public event Action<Action<string>> OnLoginDemand;
        public bool ClientOnAutorization { get; set; }

        private VkontakteApi vkontakteApi;
        private const string AppId = "3356524";

        public Uri GetUri()
        {
            var uri = new Uri(VkontakteCore.AuthorizationHelper.GetAuthorizationUrl(AppId));
            return uri;
        }

        public void ParseUri(Uri uri)
        {
            var result = VkontakteCore.AuthorizationHelper.ParseNavigatedUrl(uri.ToString());
            if (result.Status == AuthorzationStatus.Success)
            {

                this.GetService<IVkontakteApi>().Initialize(result.Context);
                if (OnBack != null) {
                    OnBack();
                }
              
            }
        }

        public void RestoreContext()
        {
            GetService<IVkontakteApi>().RestoreContext();
        }

        

        public void SentToWall(string text) {
            if (IsAuthorized) {
                    RestoreContext();
                    var uid = GetService<IVkontakteApi>().GetCurrentUid();
                    var textmessage = text;
                   vkontakteApi.WallPost(
                       uid,
                       textmessage,
                       () => MessageBox.Show("Запись была добавлена на вашу стену"),
                       errorResponse => MessageBox.Show("При добавлении записи на стену, произошла ошибка")
                       );
            }
            else {
                OnLoginDemand(SentToWall);
                ClientOnAutorization = true;
            }
        }


        protected T GetService<T>() where T : class
        {
            if (typeof(T) == typeof(IEntityDataStorage))
            {
                return new EntityStorage() as T;
            }

            if (typeof(T) == typeof(IVkontakteApi))
            {
                if (vkontakteApi == null) vkontakteApi = new VkontakteApi(GetService<IEntityDataStorage>());
                return vkontakteApi as T;
            }




            throw new Exception("service not found");
        }

        public bool IsAuthorized
        {
            get { return GetService<IVkontakteApi>().Authorized(); }

        }


    }
}
