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
using MySqlConnector;
using MySqlConnection = MySqlConnector.MySqlConnection;
using MySqlCommand = MySqlConnector.MySqlCommand;
using MySqlDbType = MySqlConnector.MySqlDbType;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DatingAppLicencjat.Activities
{
    [Activity(Theme = "@style/AppTheme", MainLauncher = false)]
    public class RegisterActivity : AppCompatActivity
    {
        EditText username;
        EditText password;
        EditText email;
        EditText passwordConfirm;
        TextView goBackToLogin;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.registerForm);

            username = FindViewById<EditText>(Resource.Id.nameRegisterText);
            password = FindViewById<EditText>(Resource.Id.passwordRegisterText);
            passwordConfirm = FindViewById<EditText>(Resource.Id.paswordRepeatRegisterText);
            email = FindViewById<EditText>(Resource.Id.emailRegisterText);
            TextView goBackToLogin = FindViewById<TextView>(Resource.Id.clickAlreadyRegistered);
            Button regButton = FindViewById<Button>(Resource.Id.registerButton);
            goBackToLogin.Click += GoBackToLogin_Click;
            regButton.Click += RegButton_Click;
            
        }

        private void GoBackToLogin_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LoginActivity));
            Finish();
        }

        private async void RegButton_Click(object sender, EventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            try
            {
                if(username.Text.Length > 20)
                {
                    Toast.MakeText(this, "Podana nazwa jest za długa.", ToastLength.Short).Show();
                    return;
                }
                else if (!email.Text.Contains("@"))
                {
                    Toast.MakeText(this, "Wprowadź poprawny adres e-mail.", ToastLength.Short).Show();
                    return;
                }
                else if(password.Text.Length < 6)
                {
                    Toast.MakeText(this, "Hasło musi mieć więcej niż 6 znaków.", ToastLength.Short).Show();
                    return;
                }
                else if(password.Text != passwordConfirm.Text)
                {
                    Toast.MakeText(this, "Podane hasła różnią się od siebie.", ToastLength.Short).Show();
                    return;
                }

                var data = new { fullName = username.Text, password = password.Text, email = email.Text };
                var content = JsonConvert.SerializeObject(data);
                var buffer = Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                HttpClient client = new HttpClient(clientHandler);
                string url = "https://licencjatapi.azurewebsites.net/api/login/test";
                var uri = new Uri(url);
                var result = await client.PostAsync(uri, new StringContent(content, Encoding.UTF8, "application/json"));


                if(result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Toast.MakeText(this, "Rejestracja pomyślna.", ToastLength.Short).Show();
                    StartActivity(typeof(LoginActivity));
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Istnieje już konto z podanym mailem.", ToastLength.Short).Show();
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}