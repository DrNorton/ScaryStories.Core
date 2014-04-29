using System;

namespace VkontakteInfrastructure.Model
{
    public class Message : IEntity
    {
        public string Body { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Uid { get; set; }
        public string Mid { get; set; }
        public bool IsOut { get; set; }
        public bool IsNewMessage { get; set; }
    }
}