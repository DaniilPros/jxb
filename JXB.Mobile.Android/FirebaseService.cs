using Android.App;
using Android.Content;
using Firebase.Iid;

namespace JXB.Mobile.Android
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseService : FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            StateHolder.Instance.RegisterForNotifications(refreshedToken);
            //send token to server
        }
    }
}