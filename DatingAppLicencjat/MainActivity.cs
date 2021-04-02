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

namespace DatingAppLicencjat
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        RecyclerView postRecyclerView;
        PostAdapter postAdapter;
        List<postModel> postList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            postRecyclerView = (RecyclerView)FindViewById(Resource.Id.postRecycleView);
            CreateData();
            SetupRecyclerView();

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