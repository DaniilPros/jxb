using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;

namespace JXB.Mobile.Android
{
    [Application]
    class Jxb : Application
    {
        public Jxb(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
            System.Threading.Tasks.Task.Run(() => Init());
        }
        void Init()
        {
            if (FirebaseApp.GetApps(this).Count > 0)
            {
                return;
            }
            var options = new FirebaseOptions.Builder()
                                             .SetApplicationId("junction-x")
                                             .SetApiKey(FirebaseMessaging.FirebaseServerId)
                                             .SetGcmSenderId(FirebaseMessaging.FirebaseSenderId)
                                             //.SetStorageBucket("irt-payment.appspot.com")
                                             .Build();
            FirebaseApp.InitializeApp(this, options);

            FirebaseMessaging.RefreshToken();
        }
    }
}