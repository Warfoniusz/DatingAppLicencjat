using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DatingAppLicencjat.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : Android.Support.V7.App.AppCompatActivity
    {
        EditText login;
        EditText password;
        TextView goToRegisterText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);

            TextView goToRegisterText = FindViewById<TextView>(Resource.Id.clickToRegisterText);
            login = FindViewById<EditText>(Resource.Id.emailLoginText);
            password = FindViewById<EditText>(Resource.Id.passwordLoginText);
            Button loginButton = (Button)FindViewById(Resource.Id.loginButton);
            loginButton.Click += LoginButton_Click;
            goToRegisterText.Click += GoToRegisterText_Click;

            
          
        }

        private void GoToRegisterText_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterActivity));
            Finish();
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (login.Text.Equals(""))
                {
                    Toast.MakeText(this, "Podaj email.", ToastLength.Short).Show();
                    return;
                }
                if (password.Text.Equals(""))
                {
                    Toast.MakeText(this, "Podaj hasło.", ToastLength.Short).Show();
                    return;
                }

                string url = "https://licencjatapi.azurewebsites.net/api/login/login";
                var uri = new Uri(url);
                var data = new { password = password.Text, email = login.Text };
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

                if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    Toast.MakeText(this, "Zalogowano pomyślnie.", ToastLength.Short).Show();
                    StartActivity(typeof(MainActivity));
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Podales nieprawidlowe dane.", ToastLength.Short).Show();
                    return;
                }
            }
            catch
            {

            }
            
        }
    }
}