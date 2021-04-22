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
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DatingAppLicencjat.Resources.values;
using Android.Content;

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
        ImageView watchedPosts;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource        
            SetContentView(Resource.Layout.activity_main);
            watchedPosts = (ImageView)FindViewById(Resource.Id.watchedPostsOnMainView);
            postRecyclerView = (RecyclerView)FindViewById(Resource.Id.postRecycleView);
            layoutStatus = (RelativeLayout)FindViewById(Resource.Id.layoutStatus);
            newPost = (ImageView)FindViewById(Resource.Id.addNewImageOnMainView);
            newPost.Click += NewPost_Click;
            watchedPosts.Click += WatchedPosts_Click;
            GetPostList();

        }

        private void WatchedPosts_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(WatchedPostsActivity));
        }

        private void NewPost_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(NewPostActivity));
        }

        async void GetPostList()
        {
            
            try
            {
                postList = new List<postModel>();
                string url = "https://licencjatapi.azurewebsites.net/api/getInfo/getPostList";
                var uri = new Uri(url);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = uri
                };

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);
                var result = await client.SendAsync(request);
                var contentBody = await result.Content.ReadAsStringAsync();
                List<postRetrieved> posts = JsonConvert.DeserializeObject<List<postRetrieved>>(contentBody);
                foreach (var post in posts)
                {
                    postModel newPost = new postModel();
                    newPost.id = post.postId;
                    newPost.description = post.postDescription;
                    newPost.username = post.fullName;
                    newPost.title = post.postTitle;
                    newPost.city = post.postCity;
                    postList.Add(newPost);
                }
            }

            catch(Exception ex)
            {

            }
            finally
            {
                postRecyclerView.SetLayoutManager(new Android.Support.V7.Widget.LinearLayoutManager(postRecyclerView.Context));
                postAdapter = new PostAdapter(postList);
                postRecyclerView.SetAdapter(postAdapter);
                Constants.posts = postList;
                postAdapter.ItemClick += PostAdapter_ItemClick;
            }
            

        }

        private void PostAdapter_ItemClick(object sender, Adapter1ClickEventArgs e)
        {
            postModel clickedPost = new postModel();
            clickedPost.city = postList[e.Position].city;
            clickedPost.description = postList[e.Position].description;
            clickedPost.username = postList[e.Position].username;
            clickedPost.id = postList[e.Position].id;
            int postId = postList[e.Position].id;
            int ownerId = postList[e.Position].creatorId;
            Intent intent = new Intent(this, typeof(ViewPostActivity));
            intent.PutExtra("data", JsonConvert.SerializeObject(clickedPost));
            this.StartActivity(intent);
        }
    }
}