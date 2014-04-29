namespace VkontakteViewModel.Services
{
    public interface ISimpleNavigationService
    {
        void NavigatToAuthorizationPage();
        void NavigateToProfilePage(string uid);
        void NavigateToHomePage();
        void NavigateToPhotoAlbumPage(string uid);
        void NavigateToPhotosViewPage(string uid, string albumUid);
        void NavigateToMessageConversationPage(string uid);
        void NavigateToMessagesPage();
        void NavigateToFriendsPage();
    }
}