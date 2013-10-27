
namespace ScaryStories.Services.Dto
{
    public class RemoteCheckingUpdateDto {
        private long _newStoriesCount;

        public long NewStoriesCount {
            get {
                return _newStoriesCount;
            }
            set {
                _newStoriesCount = value;
            }
        }
    }
}
