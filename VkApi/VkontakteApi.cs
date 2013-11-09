using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using VkontakteInfrastructure.Interfaces;
using VkontakteInfrastructure.Model;
using VkontakteServiceLayer;
using VkontakteServiceLayer.Responses;
using Message = VkontakteInfrastructure.Model.Message;
using Photo = VkontakteInfrastructure.Model.Photo;
using PhotoAlbum = VkontakteInfrastructure.Model.PhotoAlbum;

namespace VkontakteCore
{
    public interface IVkontakteApi
    {
        void Initialize(string appId, string currentUserId, string accessToken);
        void Initialize(AuthorizationContext context);

        void GetFriends(Action<List<User>> getFriendsAction, Action<Error> errorAction);
        void GetCurrentUserProfile(Action<User> getUser, Action<Error> errorAction);
        void GetUserProfile(string uid, Action<User> getUserProfileComplete, Action<Error> getUserProfileError);
        void GetUserProfiles(List<String> uids, Action<List<User>> getUserProfileComplete, Action<Error> getUserProfileError);
        void GetPhotoAlbums(string uid, Action<List<PhotoAlbum>> getPhotoAlbums, Action<Error> getErrorAction);
        void GetPhotos(string uid, List<string> photoIds, Action<List<Photo>> getPhotosAction, Action<Error> errorAction);
        void GetPhotosByAlbum(string aid, string uid, Action<List<Photo>> getPhotosAction, Action<Error> getErrorAction);
        void GetMessages(Action<List<Message>> getListMessagesAction, Action<Error> errorAction);
        void GetMessageConversation(string uid, Action<List<Message>> messages, Action<Error> errorAction);
        void SendMessage(string uid, string textmessage, Action sendMessageCompleteAction,
                         Action<Error> sendMessageErrorAction);

        bool Authorized();
        void RestoreContext();

        void DeleteContext();
        string GetCurrentUid();
    }

    public class VkontakteApi : IVkontakteApi
    {
        private readonly IEntityDataStorage entityDataStorage;

        public VkontakteApi(IEntityDataStorage entityDataStorage)
        {
            this.entityDataStorage = entityDataStorage;
        }

        private AuthorizationContext context;
        private AuthorizationContext Context
        {
            get
            {
                if (context == null)
                {
                    var authorizationContext = entityDataStorage.LoadEntity<AuthorizationContext>();
                    context = authorizationContext;
                }
                return context;
            }
            set 
            { 
                context = value;
                entityDataStorage.SaveEntity(context);
            }
        }

        public void Initialize(string appId, string currentUserId, string accessToken)
        {
            Context = new AuthorizationContext()
            {
                ApplicationId = appId,
                CurrentUserId = currentUserId,
                AccessToken = accessToken
            };


        }

        public void Initialize(AuthorizationContext authorizationContext)
        {
            if (authorizationContext == null) throw new ArgumentNullException("authorizationContext");
            this.Context = authorizationContext;
        }

        public void GetFriends(Action<List<User>> getFriendsAction, Action<Error> errorAction)
        {
            var serviceLayer=new ServiceLayer();
            serviceLayer.GetFriends(Context,getFriendsAction,errorAction);
        }

        public void GetCurrentUserProfile(Action<User> getUser, Action<Error> errorAction)
        {
            if (Context == null)
            {
                errorAction.Invoke(new Error() {ErrorCode = "5"});
                return;
            }

            var serviceLayer = new ServiceLayer();
            serviceLayer.GetProfiles(Context,new string[] {Context.CurrentUserId},(listResult)=>
            {
                getUser.Invoke(listResult.First());
            },errorAction);
        }


        public void GetMessages(Action<List<Message>> getListMessagesAction, Action<Error> errorAction)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.GetMessages(Context,getListMessagesAction,errorAction);
        }

        public void GetMessageConversation(string uid, Action<List<Message>> getListMessagesAction, Action<Error> errorAction)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.GetMessageConversation(Context,uid, getListMessagesAction, errorAction);
        }

        public void GetUserProfile(string uid, Action<User> getUserProfileComplete, Action<Error> getUserProfileError)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.GetProfiles(Context,new []{uid}, (resultList)=>
            {
                getUserProfileComplete.Invoke(resultList.First());
            }, getUserProfileError);


        }

        public void GetUserProfiles(List<string> uids, Action<List<User>> getUserProfileComplete, Action<Error> getUserProfileError)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.GetProfiles(Context, uids.ToArray(), getUserProfileComplete, getUserProfileError);
        }

        public void GetPhotoAlbums(string uid, Action<List<PhotoAlbum>> getPhotoAlbums, Action<Error> getErrorAction)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.GetUserPhotoAlbums(Context, uid, getPhotoAlbums, getErrorAction);
        }

        public void GetPhotos(string uid, List<string> photoIds, Action<List<Photo>> getPhotosAction, Action<Error> errorAction)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.GetPhotos(Context, uid,photoIds, getPhotosAction, errorAction);
        }

        public void GetPhotosByAlbum(string aid, string uid, Action<List<Photo>> getPhotosAction, Action<Error> getErrorAction)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.GetPhotosByAlbum(Context,aid, uid, getPhotosAction, getErrorAction);
        }

        public void SendMessage(string uid, string textmessage, Action sendMessageCompleteAction , Action<Error> sendMessageErrorAction)
        {
            var serviceLayer = new ServiceLayer();
            serviceLayer.SendMessage(Context, uid, textmessage, sendMessageCompleteAction, sendMessageErrorAction);
        }

        public bool Authorized()
        {
            return entityDataStorage.LoadEntity<AuthorizationContext>()!=null;
        }

        public void RestoreContext()
        {
            Context = entityDataStorage.LoadEntity<AuthorizationContext>();
        }

        public void DeleteContext()
        {
            entityDataStorage.DeleteEntity<AuthorizationContext>();
        }

        public string GetCurrentUid()
        {
            return this.Context.CurrentUserId;
        }
    }
}
