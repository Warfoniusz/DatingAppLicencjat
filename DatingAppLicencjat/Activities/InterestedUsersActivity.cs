using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DatingAppLicencjat.Adapters;
using DatingAppLicencjat.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace DatingAppLicencjat.Activities
{

    [Activity(Label = "InterestedUsersActivity")]
    public class InterestedUsersActivity : Activity
    {
        postData postData;
        RecyclerView interestedRecyclerView;
        InterestedUsersAdapter interestedAdapter;
        List<userProfileModel> userList;
        private TextView postTitle;
        public int postId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.interested_users);
            postTitle = (TextView) FindViewById(Resource.Id.post_name_in_interested_users);
            interestedRecyclerView = (RecyclerView)FindViewById(Resource.Id.post_recycler_on_interested_users);
            postData = JsonConvert.DeserializeObject<postData>(Intent.GetStringExtra("data"));
            int postId = postData.postId;
            string postName = postData.postTitle;
            postTitle.Text = postName;
            GetInterestedUsers();


            async void GetInterestedUsers()
            {
                try
                {
                    userList = new List<userProfileModel>();
                    string url = "https://licencjatapi.azurewebsites.net/api/getInfo/get_interested_users/" + postId;
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
                    List<userProfileModel> users = JsonConvert.DeserializeObject<List<userProfileModel>>(contentBody);
                    foreach(var user in users)
                    {
                        userProfileModel newUser = new userProfileModel();
                        newUser.userId = user.userId;
                        newUser.username = user.username;
                        newUser.profile_photo_base64 = user.profile_photo_base64;
                        userList.Add(user);
                    }
                }
                catch
                { 
                
                }
                finally

                {
                    interestedRecyclerView.SetLayoutManager(new Android.Support.V7.Widget.GridLayoutManager(interestedRecyclerView.Context, 2));
                    interestedAdapter = new InterestedUsersAdapter(userList);
                    interestedRecyclerView.SetAdapter(interestedAdapter);
                    interestedAdapter.acceptClick += InterestedAdapter_acceptClick;
                    interestedAdapter.denyClick += InterestedAdapter_denyClick;

                }
            }
        }

        private async void InterestedAdapter_denyClick(object sender, InterestedUsersAdapterClickEventArgs e)
        {
            try
            {
                var data = new { userId = userList[e.Position].userId, postId = postId, acceptanceStatus = "denied" };
                string url = "https://licencjatapi.azurewebsites.net/api/getinfo/change_followed_status";
                var uri = new Uri(url);
                var content = JsonConvert.SerializeObject(data);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = uri,
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                HttpClient client = new HttpClient(clientHandler);
                var result = await client.SendAsync(request);
                if(result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Toast.MakeText(this, "Zaakceptowano użytkownika.", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Coś poszło nie tak.", ToastLength.Short).Show();
                }
            }

            catch(Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }

        private async void InterestedAdapter_acceptClick(object sender, InterestedUsersAdapterClickEventArgs e)
        {
            try
            {
                var data = new { userId = userList[e.Position].userId, postId = postData.postId, acceptanceStatus = "accepted" };
                string url = "https://licencjatapi.azurewebsites.net/api/getinfo/change_followed_status";
                var uri = new Uri(url);
                var content = JsonConvert.SerializeObject(data);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = uri,
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                HttpClient client = new HttpClient(clientHandler);
                var result = await client.SendAsync(request);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Toast.MakeText(this, "Zaakceptowano użytkownika.", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Coś poszło nie tak.", ToastLength.Short).Show();
                }
            }

            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }
    }
}