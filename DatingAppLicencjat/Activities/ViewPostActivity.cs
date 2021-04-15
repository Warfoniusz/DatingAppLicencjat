using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DatingAppLicencjat.Models;
using DatingAppLicencjat.Resources.values;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DatingAppLicencjat.Activities
{
    [Activity(Theme = "@style/AppTheme", MainLauncher = false)]
    public class ViewPostActivity : Android.Support.V7.App.AppCompatActivity
    {
        TextView postTitle, postDescription, postCity;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.viewPost);
            postModel clickedPost = new postModel();
            clickedPost = JsonConvert.DeserializeObject<postModel>(Intent.GetStringExtra("data"));
            postTitle = (TextView)FindViewById(Resource.Id.viewPostTitle);
            postDescription = (TextView)FindViewById(Resource.Id.viewPostDescription);
            postCity = (TextView)FindViewById(Resource.Id.viewPostCity);

            postCity.Text = clickedPost.city;
            postTitle.Text = clickedPost.username;
            postDescription.Text = clickedPost.description;

        }
    }
}