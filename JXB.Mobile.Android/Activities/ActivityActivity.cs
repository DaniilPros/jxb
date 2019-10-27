using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Button;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using JXB.Mobile.Android.Adapters;
using System;
using System.Threading.Tasks;

namespace JXB.Mobile.Android.Activities
{
    [Activity(Label = "ActivityActivity",
        ScreenOrientation = ScreenOrientation.SensorPortrait,
        LaunchMode = LaunchMode.SingleTop)]
    public class ActivityActivity : Activity
    {
        ActivityAdapter adapter;
        SwipeRefreshLayout refresher;
        private RecyclerView list;
        private MaterialButton checkin;
        private TextView message;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ActivityActivity);

            list = FindViewById<RecyclerView>(Resource.Id.Activity);
            var lm = new LinearLayoutManager(this, LinearLayoutManager.Vertical, false);
            list.SetLayoutManager(lm);
            list.Visibility = ViewStates.Gone;

            adapter = new ActivityAdapter();
            list.SetAdapter(adapter);

            refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.Refresher);
            refresher.Refresh += Refresher_Refresh;
            refresher.Refreshing = true;

            checkin = FindViewById<MaterialButton>(Resource.Id.Checkin);
            checkin.Visibility = ViewStates.Gone;
            checkin.Click += Checkin_Click;

            message = FindViewById<TextView>(Resource.Id.Message);

            GetData();

            StateHolder.Instance.OnCheckIn +=  () => GetData();
            StateHolder.Instance.OnNew += () => GetData();
            StateHolder.Instance.OnRate += ShowRate;
        }

        void ShowRate()
        {
            RunOnUiThread(() =>
            {
                var intent = new Intent(this, typeof(RateActivity));
                StartActivity(intent);
            });
        }

        private async void Checkin_Click(object sender, EventArgs e)
        {
            await StateHolder.Instance.CheckIn(adapter.Activity.Id);
            await GetData();
        }

        async Task GetData()
        {
            adapter.Activity = await StateHolder.Instance.GetActivity();
            RunOnUiThread(() =>
            {
                if (adapter.Activity != null)
                {
                    list.Visibility = ViewStates.Visible;
                    message.Visibility = ViewStates.Gone;

                    if (DateTimeOffset.UtcNow > adapter.Activity.Time + TimeSpan.FromMinutes(2))
                    {
                        checkin.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        checkin.Visibility = ViewStates.Gone;
                    }
                }
                else
                {
                    list.Visibility = ViewStates.Gone;
                    message.Visibility = ViewStates.Visible;
                }
                adapter.NotifyDataSetChanged();
                refresher.Refreshing = false;
            });
        }

        private async void Refresher_Refresh(object sender, System.EventArgs e)
        {
            await GetData();
        }
    }
}