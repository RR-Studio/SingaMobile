using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace SingaMobile.Activities
{
    [Activity(Label = "SignupActivity",Theme = "@style/AppTheme.Dark")]
    public class SignupActivity : AppCompatActivity
    {
        private EditText _nameTxt, _emailTxt, _passwTxt, _passwConfirmTxt;
        private Button _signupButton;
        private TextView _loginLink;

        private void InitializeControls()
        {
            _nameTxt = FindViewById<EditText>(Resource.Id.input_name);
            _emailTxt = FindViewById<EditText>(Resource.Id.input_email);
            _passwTxt = FindViewById<EditText>(Resource.Id.input_password);
            _passwConfirmTxt = FindViewById<EditText>(Resource.Id.input_reEnterPassword);

            _signupButton = FindViewById<Button>(Resource.Id.btn_signup);
            _loginLink = FindViewById<TextView>(Resource.Id.link_login);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_signup);
            InitializeControls();

            _signupButton.Click += SignUp;
            _loginLink.Click += Login;
        }

        private void Login(object sender, System.EventArgs e)
        {
            var intent = new Intent(ApplicationContext, typeof(LoginActivity));
            StartActivity(intent);
            Finish();
            OverridePendingTransition(Resource.Animation.push_left_in, Resource.Animation.push_left_out);
        }

        private void SignUp(object sender, System.EventArgs e)
        {
            if (!IsInputValid())
            {
                OnSignupFailed();
                return;
            }

            _signupButton.Enabled = false;

            var progressDialog = new ProgressDialog(this, Resource.Style.AppTheme_Dark_Dialog);
            progressDialog.Indeterminate = true;
            progressDialog.SetMessage("Creating Account...");
            progressDialog.Show();

            var name = _nameTxt.Text;
            var email = _emailTxt.Text;
            var password = _passwTxt.Text;
            var reEnterPassword = _passwConfirmTxt.Text;

            // TODO: Sign Up

            OnSignupSuccess();
        }

        private void OnSignupSuccess()
        {
            StartActivity(typeof(MainActivity));
            Finish();
            OverridePendingTransition(Resource.Animation.push_left_in, Resource.Animation.push_left_out);
        }

        private void OnSignupFailed()
        {
            Toast.MakeText(ApplicationContext, "Check typed data", ToastLength.Long).Show();
        }

        private bool IsInputValid()
        {
            bool valid = true;

            var name = _nameTxt.Text;
            var email = _emailTxt.Text;
            var password = _passwTxt.Text;
            var passwordConfirmation = _passwConfirmTxt.Text;

            if (string.IsNullOrEmpty(name) || name.Length < 3)
            {
                _nameTxt.Error = "at least 3 characters";
                valid = false;
            }
            else
            {
                _nameTxt.Error = null;
            }

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
                _passwTxt.Error = "between 4 and 10 alphanumeric characters";
                valid = false;
            }
            else
            {
                _passwTxt.Error = null;
            }

            if (string.IsNullOrEmpty(passwordConfirmation) || passwordConfirmation.Length< 4 || passwordConfirmation.Length> 10 || !(passwordConfirmation.Equals(password)))
            {
                _passwConfirmTxt.Error = "Password Do not match";
                valid = false;
            }
            else
            {
                _passwConfirmTxt.Error = null;
            }

            return valid;
        }
    }
}