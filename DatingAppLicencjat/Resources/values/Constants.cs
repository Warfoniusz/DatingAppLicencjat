using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DatingAppLicencjat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingAppLicencjat.Resources.values
{
    public static class Constants
    {
        public static string user_photo_in_view_post;
        public static List<string> base64photosInInterested;

        public static List<postModel> posts;

        public static string fullname = "";

        public static int userId;

        public static string connectionString = "server=185.243.53.232;port=3306;uid=wurf;pwd=GoxFxCRul8N0p2dl;database=wurf";

        #region MYSQL INSERT NEW USER QUERY

        public static string InsertNewUserQuery = $"insert into users (username, password_salt, password, email, created_at) values (@username, @password_salt, @password, @email, @timestamp)";

        #endregion

        #region MYSQL CHECK IF EMAIL ALREADY REGISTERED

        public static string checkIfAlreadyRegisteredQuery = $"select * from users WHERE email LIKE @email";

        #endregion
    }

}