using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DatingAppLicencjat.Resources.values;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace DatingAppLicencjat.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class NewPostActivity : AppCompatActivity
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        EditText postDescription;
        EditText postCity;
        EditText postTitle;
        Button submitBTN;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.addNewPost);

            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Create Post";
            postTitle = (EditText)FindViewById(Resource.Id.newPostTitle);
            postDescription = (EditText)FindViewById(Resource.Id.newPostDescription);
            postCity = (EditText)FindViewById(Resource.Id.newPostCity);
            submitBTN = (Button)FindViewById(Resource.Id.newPostSubmitBTN);

            submitBTN.Click += SubmitBTN_Click;



        }

        private async void SubmitBTN_Click(object sender, EventArgs e)
        {
            var data = new { userId = Constants.userId, fullName = Constants.fullname, postTitle = postTitle.Text, postDescription = postDescription.Text, postCity = postCity.Text };
            string url = "https://licencjatapi.azurewebsites.net/api/login/addPost";
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

            if(result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Toast.MakeText(this, "Pomyślnie dodano ogłoszenie.", ToastLength.Short).Show();                
                StartActivity(typeof(MainActivity));
                Finish();
            }
            else
            {
                Toast.MakeText(this, "Nie udało się dodać ogłoszenia.", ToastLength.Short).Show();
            }
        }
    }
}