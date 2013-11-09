using ApiCore.Friends;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ApiCore.Wrappers.UserInfo
{
    public class UserInfoFactory:BaseFactory
    {
        public UserInfoFactory(ApiManager manager)
            :base(manager)
        {

        }

        public UserInfo Get(int? ownerId)
        {
            this.Manager.Method("users.get");
            if (ownerId != null)
            {
                this.Manager.Params("uids", ownerId);
            }
            this.Manager.Params("fields", "uid, first_name, last_name, nickname, sex, bdate (birthdate), city, country, timezone, photo, photo_medium, photo_big, photo_rec");
            this.Manager.Params("name_case", "nom");
            string resp = this.Manager.Execute().GetResponseString();
            if (this.Manager.MethodSuccessed)
            {
                XmlDocument x = this.Manager.GetXmlDocument(resp);
                if (x.SelectSingleNode("/response").InnerText.Equals("0"))
                {
                    return null;
                }

              

                return this.Build(x);
            }
            return null;
        }
        private UserInfo Build(XmlDocument x)
        {
            XmlNode msgNode = x.SelectSingleNode("/response/user");
                    UserInfo user = new UserInfo();
                    XmlUtils.UseNode(msgNode);
                    user.Id = XmlUtils.Int(UserFields.Id);
                    user.FirstName = XmlUtils.String(UserFields.FirstName);
                    user.LastName = XmlUtils.String(UserFields.LastName);
                    user.NickName = XmlUtils.String(UserFields.NickName);
                    user.Rating = XmlUtils.Int(UserFields.Rating);
                    user.Sex = (FriendSex)XmlUtils.Int(UserFields.Sex);
                    user.BirthDate = XmlUtils.String(UserFields.BirthDate);
                    user.City = XmlUtils.Int(UserFields.City);
                    user.Country = XmlUtils.Int(UserFields.Country);
                    user.Timezone = XmlUtils.String(UserFields.Timezone);
                    user.Photo = XmlUtils.String(UserFields.Photo);
                    user.PhotoMedium = XmlUtils.String(UserFields.PhotoMedium);
                    user.PhotoBig = XmlUtils.String(UserFields.PhotoBig);
                    user.Online = XmlUtils.Bool(UserFields.Online);

                    user.Domain = XmlUtils.String(UserFields.Domain);
                    user.HomePhone = XmlUtils.String(UserFields.HomePhone);
                    user.MobilePhone = XmlUtils.String(UserFields.MobilePhone);
                    user.HasMobile = XmlUtils.Bool(UserFields.HasMobile);
                    user.University = XmlUtils.Int(UserFields.University);
                    user.UniversityName = XmlUtils.String(UserFields.UniversityName);
                    user.Faculty = XmlUtils.Int(UserFields.Faculty);
                    user.FacultyName = XmlUtils.String(UserFields.FacultyName);
                    user.Graduation = XmlUtils.Int(UserFields.Graduation);
                  
            
            return user;
        }
    }
}
