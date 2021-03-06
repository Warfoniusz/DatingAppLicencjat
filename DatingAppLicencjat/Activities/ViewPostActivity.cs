using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
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
using Refractored.Controls;

namespace DatingAppLicencjat.Activities
{
    [Activity(Theme = "@style/AppTheme", MainLauncher = false)]
    public class ViewPostActivity : Android.Support.V7.App.AppCompatActivity
    {
        public postModel clickedPost = new postModel();
        TextView postTitle, postDescription, postCity;
        Button BTNInterested;
        private CircleImageView userPhoto;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.viewPost);
            clickedPost = JsonConvert.DeserializeObject<postModel>(Intent.GetStringExtra("data"));
            postTitle = (TextView)FindViewById(Resource.Id.viewPostTitle);
            postDescription = (TextView)FindViewById(Resource.Id.viewPostDescription);
            postCity = (TextView)FindViewById(Resource.Id.viewPostCity);
            BTNInterested = (Button)FindViewById(Resource.Id.viewPostButtonInterested);
            userPhoto = (CircleImageView) FindViewById(Resource.Id.user_photo_in_view_post);
            BTNInterested.Click += BTNInterested_Click;

            Byte[] bitmapData = Convert.FromBase64String(Constants.user_photo_in_view_post);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bitmapData);
            Android.Graphics.Bitmap bitImage = Android.Graphics.BitmapFactory.DecodeStream(ms);
            userPhoto.SetImageBitmap(bitImage);
            postCity.Text = clickedPost.city;
            postTitle.Text = clickedPost.username;
            postDescription.Text = clickedPost.description;

        }


        private async void BTNInterested_Click(object sender, EventArgs e)
        {
            var data = new { userId = Constants.userId, postId = clickedPost.id, acceptanceStatus = "waiting" };
            string url = "https://licencjatapi.azurewebsites.net/api/GetInfo/clickInterested";
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
                Toast.MakeText(this, "Dodano ogłoszenie do obserwowanych.", ToastLength.Short).Show();
                BTNInterested.Clickable = false;
            }
            else
            {
                Toast.MakeText(this, "Coś poszło nie tak.", ToastLength.Short).Show();
            }
            
        }
    }
}