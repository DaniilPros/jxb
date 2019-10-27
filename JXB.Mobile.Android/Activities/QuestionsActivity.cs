using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using JXB.Mobile.Android.Adapters;
using JXB.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JXB.Mobile.Android.Activities
{
    [Activity(
        Label = "QuestionsActivity", 
        ScreenOrientation = ScreenOrientation.SensorPortrait, 
        LaunchMode = LaunchMode.SingleTop)]
    public class QuestionsActivity : Activity
    {
        RecyclerView list;
        QuestionsAdapter adapter;
        Dictionary<string, Answer> answers = new Dictionary<string, Answer>();
        private TextView title;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuestionsActivity);

            list = FindViewById<RecyclerView>(Resource.Id.QuestionsList);
            var lm = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            list.SetLayoutManager(lm);
            
            adapter = new QuestionsAdapter();
            adapter.OnSelected += Adapter_OnLeft;
            list.SetAdapter(adapter);
            list.Visibility = ViewStates.Gone;

            title = FindViewById<TextView>(Resource.Id.Title);
            title.Visibility = ViewStates.Gone;

            //var toolbar = FindViewById<Toolbar>(Resource.Id.Toolbar);
            //toolbar.Title = "I would like to...";

            if (!StateHolder.Instance.User.IsNew)
            {
                OpenActivity();
            }
            else
            {
                GetData();
            }
        }

        async Task GetData()
        {
            adapter.Questions = (await StateHolder.Instance.GetQuestions()).ToList();
            RunOnUiThread(() =>
            {
                adapter.NotifyDataSetChanged();
                list.Visibility = ViewStates.Visible;
                title.Visibility = ViewStates.Visible;
            });
        }

        private void Adapter_OnLeft(int id, Answer answer)
        {
            var q = adapter.Questions[id];
            answers.Add(q.Id, answer);
            if (id < adapter.ItemCount - 1)
            {
                list.SmoothScrollToPosition(id + 1);
                FindViewById<TextView>(Resource.Id.Title).Visibility = ViewStates.Visible;
            }
            if (id == adapter.ItemCount - 2)
            {
                StateHolder.Instance.SubmitAnswers(answers);
                FindViewById<TextView>(Resource.Id.Title).Visibility = ViewStates.Invisible;
                OpenActivity();
            }
        }

        void OpenActivity()
        {
            Task.Run(async () =>
            {
                await Task.Delay(1500);
                RunOnUiThread(() =>
                {
                    var intent = new Intent(this, typeof(ActivityActivity));
                    intent.AddFlags(ActivityFlags.ClearTask);
                    intent.AddFlags(ActivityFlags.NoAnimation);
                    StartActivity(intent);
                    Finish();
                });
            });
        }
    }
}