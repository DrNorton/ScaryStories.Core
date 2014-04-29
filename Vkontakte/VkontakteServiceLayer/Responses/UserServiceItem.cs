using System;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace VkontakteServiceLayer.Responses
{
    [DataContract]
    public class UserServiceItem
    {
        [DataMember(Name = "uid")]
        public string Uid { get; set; }
        
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }
        
        [DataMember(Name = "last_name")]
        public string LastName { get; set; }
        
        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }
        
        [DataMember(Name = "sex")]
        public string Sex { get; set; }
        
        [DataMember(Name = "bdate")]
        public string BrithDate { get; set; }
        
        [DataMember(Name = "city")]
        public string City { get; set; }
        
        [DataMember(Name = "country")]
        public string Country { get; set; }
        
        [DataMember(Name = "timezone")]
        public string Timezone { get; set; }
        
        [DataMember(Name = "photo")]
        public string Photo { get; set; }
        
        [DataMember(Name = "photo_medium")]
        public string PhotoMedium { get; set; }
        
        [DataMember(Name = "photo_big")]
        public string PhotoBig { get; set; }
        
        [DataMember(Name = "domain")]
        public string Domain { get; set; }
        
        [DataMember(Name = "has_mobile")]
        public string HasMobile { get; set; }
        
        [DataMember(Name = "rate")]
        public string Rate { get; set; }

        [DataMember(Name = "home_phone")]
        public string HomePhone { get; set; }
        
        [DataMember(Name = "mobile_phone")]
        public string MobilePhone { get; set; }

        [DataMember(Name = "university")]
        public string University { get; set; }
        [DataMember(Name = "university_name")]
        public string UniversityName { get; set; }
        [DataMember(Name = "faculty")]
        public string Faculty { get; set; }
        [DataMember(Name = "faculty_name")]
        public string FacultyName { get; set; }
        [DataMember(Name = "graduation")]
        public string Graduation { get; set; }
        [DataMember(Name = "online")]
        public string Online { get; set; }
    }
}
