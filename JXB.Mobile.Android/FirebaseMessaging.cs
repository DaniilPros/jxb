using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Firebase.Iid;
using Firebase.Messaging;

namespace JXB.Mobile.Android
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseMessaging : FirebaseMessagingService
    {
        public const string FirebaseSenderId = "211602675503";
        public const string FirebaseServerId = "AIzaSyDsnZOKZjxq4KQOKNyg0JXW7Duk-jOupfo";

        public override void OnMessageReceived(RemoteMessage message)
        {
            if (message.Data != null)
            {
                var data = message.Data["data"];
                ProcessNotification(data);
                Debug.WriteLine("remote message " + data);
            }
            base.OnMessageReceived(message);
        }

        public static void RefreshToken()
        {
            var instanceID = FirebaseInstanceId.Instance;
            var t = FirebaseInstanceId.Instance.Token;

            Task.Run(async () =>
            {
                try
                {
                    var httpClient = new HttpClient();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("https://iid.googleapis.com/iid/info/" + t)
                    };
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("key", "=" + FirebaseServerId);   //fuck
                    var result = await httpClient.SendAsync(request);
                    var data = await result.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(data) && data.Contains("error"))
                    {
                        instanceID.DeleteInstanceId();
                        t = FirebaseInstanceId.Instance.Token;
                    }
                    else
                    {
                        StateHolder.Instance.RegisterForNotifications(t);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("refresh token " + e);
                }
            });
        }

        public static void ProcessNotification(string data)
        {
            Debug.WriteLine("processing notififcation");
            StateHolder.Instance.OnNotificationReceived(data);
        }
    }
}