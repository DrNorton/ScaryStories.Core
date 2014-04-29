using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using VkontakteInfrastructure.Interfaces;
using VkontakteServiceLayer.Tools;

namespace VkontakteServiceLayer.Responses
{
    [DataContract(Name = "message")]
    public class Message
    {
        [DataMember(Name = "body")]
        public string Body { get; set; }
        
        [DataMember(Name = "title")]
        public string Title{ get; set; }

        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "uid")]
        public string Uid { get; set; }

        [DataMember(Name = "mid")]
        public string Mid { get; set; }

        [DataMember(Name = "read_state")]
        public string ReadState { get; set; }

        [DataMember(Name = "out")]
        public string Out { get; set; }
    }

    [DataContract]
    public class GetMessages : IServiceResult
    {
        public bool ResponseIsSuccess()
        {
            return Messages != null;
        }

        [DataMember(Name = "response")]
        public List<Object> Response { get; set; }

        public List<Message> Messages
        {

            get
            {
                if (Response == null) return null;
                return Response.Skip(1).Select(i => JsonConvert.DeserializeObject<Message>(i.ToString())).ToList();
            }
        }

        public Int64 Count
        {
            get { return (Int64) Response.First(); }
        }
    }


    [DataContract(Name = "message")]
    public class MessageHistory
    {
        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "mid")]
        public string Mid { get; set; }

        [DataMember(Name = "from_id")]
        public string Uid { get; set; }

        [DataMember(Name = "date")]
        public string Date { get; set; }

        [DataMember(Name = "read_state")]
        public string ReadState { get; set; }
    }

    [DataContract]
    public class GetMessagesHistory : IServiceResult
    {
        public bool ResponseIsSuccess()
        {
            return Messages != null;
        }

        [DataMember(Name = "response")]
        public List<Object> Response { get; set; }

        public List<MessageHistory> Messages
        {

            get
            {
                if (Response == null) return null;
                return Response.Skip(1).Select(i => JsonConvert.DeserializeObject<MessageHistory>(i.ToString())).ToList();
            }
        }

        public Int64 Count
        {
            get { return (Int64)Response.First(); }
        }
    }
}