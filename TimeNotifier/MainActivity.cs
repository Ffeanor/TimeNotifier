using System;
using Android.App;
using Android.Widget;
using Android.OS;
//using Android.Support.Design.Widget;
//using Android.Support.V7.App;
using Android.Views;
using Android.Content;
using System.Timers;

namespace TimeNotifier
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button button;
        Notification.Builder builder;
        NotificationManager notificationManager;

        Button buttonStart;
        Button buttonStop;
        Button buttonClear;

        TextView textViewTime;
        

        NumberPicker numberPickerWorkTime;
        NumberPicker numberPickerRestTime;
        NumberPicker numberPickerCount;

        Timer timer;
        double time = .0;

        double workTime = .0;
        double restTime = .0;
        int circleCount = 0;


        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.content_main);
            button = FindViewById<Button>(Resource.Id.button1);
            button.Click += Button_Click;

            buttonStart = FindViewById<Button>(Resource.Id.buttonStart);
            buttonStart.Click += ButtonStart_Click;
            buttonStop = FindViewById<Button>(Resource.Id.buttonStop);
            buttonStop.Click += ButtonStop_Click;
            buttonClear = FindViewById<Button>(Resource.Id.buttonClear);
            buttonClear.Click += ButtonClear_Click;

            textViewTime = FindViewById<TextView>(Resource.Id.textViewTime);
            textViewTime.Text = time.ToString();

            

            numberPickerWorkTime = FindViewById<NumberPicker>(Resource.Id.numberPickerWorkTime);
            numberPickerWorkTime.MaxValue = 1000;
            numberPickerWorkTime.MinValue = 0;
            numberPickerWorkTime.WrapSelectorWheel = true;
            numberPickerRestTime = FindViewById<NumberPicker>(Resource.Id.numberPickerRestTime);
            numberPickerRestTime.MaxValue = 1000;
            numberPickerRestTime.MinValue = 0;
            numberPickerRestTime.WrapSelectorWheel = true;
            numberPickerCount = FindViewById<NumberPicker>(Resource.Id.numberPickerCircleCount);
            numberPickerCount.MaxValue = 1000;
            numberPickerCount.MinValue = 0;
            numberPickerCount.WrapSelectorWheel = true;

            timer = new Timer();
            timer.Interval = 500;
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;


            // Instantiate the builder and set notification elements:
            builder = new Notification.Builder(this)
                .SetContentTitle("Sample Notification")
                .SetContentText("Hello World! This is my first notification!")
                .SetDefaults(NotificationDefaults.Vibrate)
                .SetSmallIcon(Resource.Drawable.qgis);



            // Get the notification manager:
            notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;
        }

        //private void EditTextWork_KeyPress(object sender, View.KeyEventArgs e) {
        //    if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter) {
        //        e.Handled = true;
        //        //DismissKeyboard();
        //        var editText = (EditText)sender;
        //        Toast.MakeText(this, "Value Entered=" + editText.Text + " from editTextData" + editText.Tag, ToastLength.Long).Show();
        //        Double.TryParse(editTextWork.Text, out time);
        //        time *= 1000;
        //        RunOnUiThread(() => {
        //            textViewTime.Text = time.ToString();
        //        });
        //    }
        //    else
        //        e.Handled = false;
        //}

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            if (workTime > 0) {
                if (workTime == numberPickerWorkTime.Value) {
                    // Build the notification:
                    Notification notification = builder.Build();
                    notificationManager.Notify(0, notification);
                }
                workTime -= 0.1;
                RunOnUiThread(() => {
                    textViewTime.Text = workTime.ToString("0.0");
                });
            }
            else if (restTime > 0) {
                if (restTime == numberPickerRestTime.Value) {
                    // Build the notification:
                    Notification notification = builder.Build();
                    notificationManager.Notify(0, notification);
                }
                restTime -= 0.1;
                RunOnUiThread(() => {
                    textViewTime.Text = restTime.ToString("0.0");
                });
            }
            else {
                // Build the notification:
                Notification notification = builder.Build();
                notificationManager.Notify(0, notification);

                if (circleCount < 1) {
                    ButtonStop_Click(sender, null);
                    return;
                }

                circleCount--;
                restTime = numberPickerRestTime.Value;
                workTime = numberPickerWorkTime.Value;

                workTime -= 0.1;
                RunOnUiThread(() => {
                    textViewTime.Text = workTime.ToString("0.0");
                });
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e) {
            timer.Stop();
            workTime = .0;
            restTime = .0;
            circleCount = 0;

            textViewTime.Text = workTime.ToString();
        }

        private void ButtonStop_Click(object sender, EventArgs e) {
            timer.Stop();
        }

        private void ButtonStart_Click(object sender, EventArgs e) {
            workTime = numberPickerWorkTime.Value;
            restTime = numberPickerRestTime.Value;
            circleCount = numberPickerCount.Value;
            time = workTime;
            textViewTime.Text = numberPickerWorkTime.Value.ToString();

            timer.Start();

            // Build the notification:
            Notification notification = builder.Build();
            notificationManager.Notify(0, notification);
        }

        private void Button_Click(object sender, EventArgs e) {
            //// Build the notification:
            //Notification notification = builder.Build();

            //// Publish the notification:
            //const int notificationId = 0;
            //notificationManager.Notify(notificationId, notification);

        }

        public override bool OnCreateOptionsMenu(IMenu menu) {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item) {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings) {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}

