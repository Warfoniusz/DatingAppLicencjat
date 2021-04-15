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
    public class postModel
    {
        public string username { get; set; }
        public int id { get; set; }
        public int creatorId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string city { get; set; }
        public DateTime postCreationDate { get; set; }



    }
}