using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ApiCore
{
    public partial class BrowserAuthorization : Form
    {
        public int Permissions;
        public int AppId;

        public SessionInfo si;
        public bool LoginInfoReceived = false;

        public BrowserAuthorization()
        {
            InitializeComponent();
        }

        private void LoginWnd_Shown(object sender, EventArgs e)
        {
            this.LoginBrowser.Navigate("https://oauth.vk.com/authorize?client_id=" + this.AppId.ToString() + "&display=popup&response_type=token&&scope=" + this.Permissions.ToString());
        }

        private void LoginBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.ToString().Contains("#"))
            {
                var url=e.Url.ToString();
                var pas = url.Split('#')[1];
                string[] split=pas.Split('&');
                Hashtable h = new Hashtable();
                foreach (string str in split)
                {
                    string[] kv = str.Split('=');
                    h[kv[0]] = kv[1];
                }

                this.si = new SessionInfo();
                this.si.AppId = this.AppId;
                this.si.Permissions = this.Permissions;
                this.si.Expire = Convert.ToInt32(h["expires_in"]);
                this.si.Token = (string)h["access_token"];
                this.si.UserId = (string)h["user_id"];
                this.LoginInfoReceived = true;
                this.Close();
            }
            
        }

        private void LoginBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //this.wnd.LogIt("loading: "+ e.Url );
            
        }


    }
}
