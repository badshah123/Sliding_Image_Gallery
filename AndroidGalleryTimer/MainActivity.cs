using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace AndroidGalleryTimer
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ImageView imageView;
        Button buttonStart, buttonStop;
        int index;
        System.Timers.Timer timer;

        int[] picturesTable = new int[5];
        //List<int> picturesList = new List<int>(5);
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            imageView = FindViewById<ImageView>(Resource.Id.imageView);
            buttonStart = FindViewById<Button>(Resource.Id.btnStart);
            buttonStop = FindViewById<Button>(Resource.Id.btnStop);

            loadPictures();
            buttonStart.Click += buttonStart_Clicked;
            buttonStop.Enabled = false;
            buttonStop.Click += buttonStop_Clicked;

        }
        /*public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }*/

        private void buttonStart_Clicked(object sender, EventArgs e)
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Enabled = true;
            //timer.Elapsed += Timer_Elapsed;
            timer.Elapsed += Timer_Elapsed2;
            timer.Start();
            buttonStop.Enabled = true;
            buttonStart.Enabled = false;

        }

        private void buttonStop_Clicked(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Dispose();
            timer = null;
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;

        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {

            RunOnUiThread(() =>
            {
                if (index < picturesTable.Length - 1)
                    index++;
                else
                    index = 0;
                imageView.SetImageResource(picturesTable[index]);
            }
            );

        }

        private void Timer_Elapsed2(object? sender, System.Timers.ElapsedEventArgs e)
        {

            RunOnUiThread(Action);

        }

        public void Action()
        {
            if (index < picturesTable.Length - 1)
                index++;
            else
                index = 0;
            imageView.SetImageResource(picturesTable[index]);
        }

        private void loadPictures()
        {
            string name;
            for (int i = 0; i < picturesTable.Length; i++)
            {
                name = "pic" + (i + 1).ToString();
                picturesTable[i] = (int)typeof(Resource.Drawable).GetField(name).GetValue(null);
            }
        }

        /*private void loadPictures2()
        {
            picturesList.AddRange(typeof(Resource.Drawable)
                .GetFields()
                .Where(x => x.Name.StartsWith("pic"))
                .Select(y => y.GetRawConstantValue())
                .Cast<int>());
        }

        public void Action2()
        {
            if (index < picturesList.Count - 1)
                index++;
            else
                index = 0;
            imageView.SetImageResource(picturesList[index]);
        }*/

    }
}