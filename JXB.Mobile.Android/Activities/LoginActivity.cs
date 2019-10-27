using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Button;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

namespace JXB.Mobile.Android.Activities
{
    [Activity(
        Label = "LoginActivity", 
        Theme = "@style/AppTheme", 
        MainLauncher = true, 
        ScreenOrientation = ScreenOrientation.SensorPortrait, 
        LaunchMode = LaunchMode.SingleTask)]
    public class LoginActivity : Activity
    {
        private EditText email;
        private MaterialButton button;
        private TextView footer;
        private ProgressBar loader;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginActivity);

            button = FindViewById<MaterialButton>(Resource.Id.LoginButton);
            button.Click += LoginClick;
            button.Visibility = ViewStates.Gone;
            email = FindViewById<EditText>(Resource.Id.Email);
            email.Visibility = ViewStates.Gone;
            footer = FindViewById<TextView>(Resource.Id.Footer);
            footer.Visibility = ViewStates.Gone;
            loader = FindViewById<ProgressBar>(Resource.Id.Loader);
            GetData();
        }

        async Task GetData()
        {
            await StateHolder.Instance.ReadSettings(GetExternalFilesDir(Environment.DirectoryDocuments).Path + "/settings.json");
            if (StateHolder.Instance.User != null)
            {
                await Login(StateHolder.Instance.User.Email);
            }
            else
            {
                RunOnUiThread(() =>
                {
                    email.Visibility = ViewStates.Visible;
                    button.Visibility = ViewStates.Visible;
                    footer.Visibility = ViewStates.Visible;
                    loader.Visibility = ViewStates.Gone;
                });
            }
        }

        private void LoginClick(object sender, System.EventArgs e)
        {
            var emailAddress = email.Text;
            if (!string.IsNullOrEmpty(emailAddress))
            {
                Login(emailAddress);
            }
        }

        async Task Login(string email)
        {
            await StateHolder.Instance.Login(email);

            RunOnUiThread(() =>
            {
                var intent = new Intent(this, typeof(QuestionsActivity));
                intent.AddFlags(ActivityFlags.ClearTask);
                intent.AddFlags(ActivityFlags.NoAnimation);
                StartActivity(intent);
                Finish();
            });
        }
    }
}