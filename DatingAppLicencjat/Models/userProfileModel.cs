using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingAppLicencjat.Models
{
    class userProfileModel
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string profile_photo_base64 { get; set; }
        public Image userPhoto { get; set; }

    }
}