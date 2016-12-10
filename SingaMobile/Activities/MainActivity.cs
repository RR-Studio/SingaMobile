using Android.App;
using Android.OS;

namespace SingaMobile.Activities
{
    [Activity(Label = "MainActivity",Theme = "@style/AppTheme.Dark")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
        }
    }
}