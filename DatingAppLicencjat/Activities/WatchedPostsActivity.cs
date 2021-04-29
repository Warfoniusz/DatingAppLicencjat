using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DatingAppLicencjat.Adapters;
using DatingAppLicencjat.Models;
using DatingAppLicencjat.Resources.values;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace DatingAppLicencjat.Activities
{
    [Activity(Label = "Activity1", Theme = "@style/AppTheme")]
    public class WatchedPostsActivity : Android.Support.V7.App.AppCompatActivity
    {
        List<postModel> postList;
        RecyclerView postRecyclerView;
        PostAdapter postAdapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.watchedPosts);
            postRecyclerView = (RecyclerView)FindViewById(Resource.Id.postRecycleViewOnWatched);
            GetFollowedPosts();
        }



        async void GetFollowedPosts()
        {
            try
            {
                postList = new List<postModel>();
                string url = "https://licencjatapi.azurewebsites.net/api/getInfo/getFollowedPosts/" + Constants.userId;
                var uri = new Uri(url);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
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
                    newPost.acceptanceStatus = post.acceptanceStatus;
                    postList.Add(newPost);
                }
            }

            catch (Exception ex)
            {

            }
            finally
            {
                postRecyclerView.SetLayoutManager(new Android.Support.V7.Widget.LinearLayoutManager(postRecyclerView.Context));
                postAdapter = new PostAdapter(postList);
                postRecyclerView.SetAdapter(postAdapter);
            }
        }
    }
}