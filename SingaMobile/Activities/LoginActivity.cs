using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace SingaMobile.Activities
{
    [Activity(Label = "SingaMobile", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme.Dark")]
    public class LoginActivity : AppCompatActivity
    {
        private EditText _emailTxt, _passwordTxt;
        private Button _loginBtn;
        private TextView _signupLink;

        private void InitializeControls()
        {
            _emailTxt = FindViewById<EditText>(Resource.Id.input_email);
            _passwordTxt = FindViewById<EditText>(Resource.Id.input_password);
            _loginBtn = FindViewById<Button>(Resource.Id.btn_login);
            _signupLink = FindViewById<TextView>(Resource.Id.link_signup);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_login);
            InitializeControls();

            _loginBtn.Click += Login;
            _signupLink.Click += SignUp;
        }

        private void Login(object sender, System.EventArgs e)
        {
            _loginBtn.Enabled = false;

            if (!IsInputValid())
            {
                OnLoginFailed();
                return;
            }

            var progressDialog = new ProgressDialog(this, Resource.Style.AppTheme_Dark_Dialog);
            progressDialog.Indeterminate = true;
            progressDialog.SetMessage("Authenticating...");
            progressDialog.Show();

            var email = _emailTxt.Text;
            var password = _passwordTxt.Text;

            // TODO: Authentication

            new Handler().PostDelayed(() =>
            {
                OnLoginSuccess();
                // onLoginFailed();
                progressDialog.Dismiss();
            }, 3000);

            _loginBtn.Enabled = true;
        }

        private void OnLoginSuccess()
        {
            StartActivity(typeof(MainActivity));
            Finish();
            OverridePendingTransition(Resource.Animation.push_left_in, Resource.Animation.push_left_out);
        }

        private void OnLoginFailed()
        {
            Toast.MakeText(ApplicationContext, "Login failed", ToastLength.Long).Show();
        }

        private void SignUp(object sender, System.EventArgs e)
        {
            StartActivity(typeof(SignupActivity));
            Finish();
            OverridePendingTransition(Resource.Animation.push_left_in, Resource.Animation.push_left_out);
        }

        private bool IsInputValid()
        {
            bool valid = true;

            var email = _emailTxt.Text;
            var password = _passwordTxt.Text;

            if (string.IsNullOrEmpty(email) || !Android.Util.Patterns.EmailAddress.Matcher(email).Matches())
            {
                _emailTxt.Error = "enter a valid email address";
                valid = false;
            }
            else
            {
                _emailTxt.Error = null;
            }

            if (string.IsNullOrEmpty(password) || password.Length < 4 || password.Length > 10)
            {
                _passwordTxt.Error = "between 4 and 10 alphanumeric characters";
                valid = false;
            }
            else
            {
                _passwordTxt.Error = null;
            }

            return valid;
        }
    }
}


