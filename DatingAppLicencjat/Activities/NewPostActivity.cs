using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DatingAppLicencjat.Resources.values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingAppLicencjat.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class NewPostActivity : AppCompatActivity
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        EditText postDescription;
        EditText postCity;
        Button submitBTN;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.addNewPost);

            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Create Post";
            postDescription = (EditText)FindViewById(Resource.Id.newPostDescription);
            postCity = (EditText)FindViewById(Resource.Id.newPostCity);
            submitBTN = (Button)FindViewById(Resource.Id.newPostSubmitBTN);

            submitBTN.Click += SubmitBTN_Click;



        }

        private void SubmitBTN_Click(object sender, EventArgs e)
        {
            var data = new { fullName = Constants.fullname, postDescription = postDescription, postCity = postCity, postCreationDate = DateTime.Now };
        }
    }
}