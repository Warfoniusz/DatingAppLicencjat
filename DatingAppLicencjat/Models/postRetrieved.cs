using Android.App;
using Android.Content;
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
    class postRetrieved
    {
        public string fullName { get; set; }
        public string postTitle { get; set; }
        public string postDescription { get; set; }
        public string postCity { get; set; }
        public int userId { get; set; }

        public int postId { get; set; }
    }

}