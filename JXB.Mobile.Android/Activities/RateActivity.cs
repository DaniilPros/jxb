using Android.App;
using Android.OS;
using Android.Widget;

namespace JXB.Mobile.Android.Activities
{
    [Activity(Label = "RateActivity")]
    public class RateActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RateActivity);

            //var star1 = FindViewById<ImageButton>(Resource.Id.StarOne);
            //star1.SetImageResource(Resource.Drawable.ic_star_border);
            //var star2 = FindViewById<ImageButton>(Resource.Id.StarTwo);
            //star2.SetImageResource(Resource.Drawable.ic_star_border);
            //var star3 = FindViewById<ImageButton>(Resource.Id.StarThree);
            //star3.SetImageResource(Resource.Drawable.ic_star_border);
            //var star4 = FindViewById<ImageButton>(Resource.Id.StarFour);
            //star4.SetImageResource(Resource.Drawable.ic_star_border);
            //var star5 = FindViewById<ImageButton>(Resource.Id.StarFive);
            //star5.SetImageResource(Resource.Drawable.ic_star_border);
        }
    }
}