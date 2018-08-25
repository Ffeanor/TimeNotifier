using System;
using Android.App;
using Android.Widget;
using Android.OS;
//using Android.Support.Design.Widget;
//using Android.Support.V7.App;
using Android.Views;
using Android.Content;

namespace TimeNotifier
{
	[Activity(Label = "@string/app_name", MainLauncher = true)]
	public class MainActivity : Activity
	{
        Button button;
        Notification.Builder builder;
        NotificationManager notificationManager;


        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.content_main);
            button = FindViewById<Button>(Resource.Id.button1);
            button.Click += Button_Click;

            // Instantiate the builder and set notification elements:
           builder = new Notification.Builder(this)
                .SetContentTitle("Sample Notification")
                .SetContentText("Hello World! This is my first notification!")
                .SetDefaults(NotificationDefaults.All)
                .SetSmallIcon(Resource.Drawable.qgis);

            

            // Get the notification manager:
            notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;            
        }

        private void Button_Click(object sender, EventArgs e) {
            // Build the notification:
            Notification notification = builder.Build();

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
            
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        //private void FabOnClick(object sender, EventArgs eventArgs)
        //{
        //    View view = (View) sender;
        //    Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
        //        .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        //}
	}
}

