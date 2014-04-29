using System;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using VkontakteInfrastructure.Model;

namespace VkontakteViewModel.ItemsViewModel
{
    public class UserViewModel : BaseViewModel
    {
        private readonly User user;

        public UserViewModel(User user)
        {
            this.user = user;
        }


        public string FullNameAndNickName
        {
            get 
            { 
                var result = new StringBuilder();
                result.Append(FirstName);
                result.Append(" ");
                result.Append(LastName);
                if(!String.IsNullOrEmpty(Nickname))
                {
                    result.Append("(");
                    result.Append(Nickname);
                    result.Append(")");
                }
                return result.ToString();
            }
        }
        //public UserViewModel()
        //{
        //}

        public string Uid { get { return user.Uid; }}

        public string FirstName { get { return user.FirstName; } }

        public string LastName { get { return user.LastName; } }

        public string Nickname { get { return user.Nickname; } }

        public string Sex { get { return user.Sex == "1" ? "М." : "Ж."; } }

        public string BrithDate { get { return user.BrithDate; } }

        public string City { get { return user.City; } }

        public string Country { get { return user.Country; } }

        public string Timezone { get { return user.Timezone; } }

        public string Photo { get { return user.Photo; } }

        public string PhotoMedium { get { return user.PhotoMedium; } }

        public string PhotoBig { get { return user.PhotoBig; } }

        public string Domain { get { return user.Domain; } }

        public string HasMobile { get { return user.HasMobile; } }

        public string Rate { get { return user.Rate; } }

        public string HomePhone { get { return user.HomePhone; } }

        public string MobilePhone { get { return user.MobilePhone; } }

        public string University { get { return user.University; } }

        public string UniversityName { get { return user.UniversityName; } }

        public string Faculty { get { return user.Faculty; } }

        public string FacultyName { get { return user.FacultyName; } }

        public string Graduation { get { return user.Graduation; } }

        public bool Online { get { return user.Online; } }

    }
}
