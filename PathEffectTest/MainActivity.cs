using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PathEffectTest
{
    /// <summary>
    /// PathEffect用于定义绘制效果,有6个子类
    /// 测试PathEffect绘制效果的测试代码
    /// </summary>
    [Activity(Label = "PathEffectTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate
            {
                Intent i = new Intent(this, typeof(PathEffectTest));
                StartActivity(i);
            };
        }
    }
}

