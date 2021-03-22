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

namespace DatingAppLicencjat.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class RegisterActivity : AppCompatActivity
    {
        EditText username;
        EditText password;
        EditText email;
        EditText passwordConfirm;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.registerForm);

            username = FindViewById<EditText>(Resource.Id.nameRegisterText);
            password = FindViewById<EditText>(Resource.Id.passwordRegisterText);
            passwordConfirm = FindViewById<EditText>(Resource.Id.paswordRepeatRegisterText);
            email = FindViewById<EditText>(Resource.Id.emailRegisterText);
            Button regButton = FindViewById<Button>(Resource.Id.registerButton);
            regButton.Click += RegButton_Click;
            
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

                MySqlConnection conn = new MySqlConnection(Constants.connectionString);
                MySqlDataReader reader;
                MySqlCommand checkEmailCommand = new MySqlCommand(Constants.checkIfAlreadyRegisteredQuery, conn);
                checkEmailCommand.Parameters.Add("@email", MySqlDbType.VarChar).Value = email.Text;
                conn.Open();
                reader = checkEmailCommand.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    Toast.MakeText(this, "Istnieje już konto z takim adresem email.", ToastLength.Short).Show();
                    reader.Close();
                    return;
                }
                else
                {
                    reader.Close();
                }


                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;


                cmd.CommandText = Constants.InsertNewUserQuery;
                cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = username.Text;
                cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = password.Text;
                cmd.Parameters.Add("@password_salt", MySqlDbType.VarChar).Value = password.Text;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email.Text;
                cmd.Parameters.Add("@timestamp", MySqlDbType.DateTime).Value = dtNow;
                await cmd.ExecuteNonQueryAsync();

                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}