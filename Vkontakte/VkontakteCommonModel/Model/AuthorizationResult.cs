using System;

namespace VkontakteInfrastructure.Model
{
    public enum AuthorzationStatus
    {
        Unknown,
        Success,
        Error
    }

    public class AuthorizationContext : IEntity
    {
        
        public string CurrentUserId { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresInSeconds { get; set; }
        public DateTime AuthroizationTime { get; set; }

        public string ApplicationId { get; set; }
    }

    public class AuthorizationResult
    {
        public AuthorzationStatus Status { get; set; }
        public AuthorizationContext Context { get; set; }
        public string Description { get; set; }
    }
}
