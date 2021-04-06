using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using DatingAppLicencjat.Adapters;
using System.Collections.Generic;
using DatingAppLicencjat.Models;
using System;
using DatingAppLicencjat.Activities;

namespace DatingAppLicencjat
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        RecyclerView postRecyclerView;
        PostAdapter postAdapter;
        List<postModel> postList;
        RelativeLayout layoutStatus;
        ImageView newPost;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            postRecyclerView = (RecyclerView)FindViewById(Resource.Id.postRecycleView);
            layoutStatus = (RelativeLayout)FindViewById(Resource.Id.layoutStatus);
            newPost = (ImageView)FindViewById(Resource.Id.addNewImageOnMainView);
            newPost.Click += NewPost_Click;
            CreateData();
            SetupRecyclerView();

        }

        private void NewPost_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(NewPostActivity));
        }

        void CreateData()
        {
            postList = new List<postModel>();
            postList.Add(new postModel { username = "Wincenty", description = "Szukam kolegów na piwo", city = "Mikołów" });
            postList.Add(new postModel { username = "Karolina", description = "Potrzebny koneser wina", city = "Warszawa" });
            postList.Add(new postModel { username = "Karol", description = "Melanż w klubie", city = "Katowice" });
        }

        void SetupRecyclerView()
        {
            postRecyclerView.SetLayoutManager(new Android.Support.V7.Widget.LinearLayoutManager(postRecyclerView.Context));
            postAdapter = new PostAdapter(postList);
            postRecyclerView.SetAdapter(postAdapter);
        }
        
    }
}