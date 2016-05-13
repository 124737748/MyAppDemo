using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using Android.Graphics;
using Android.Database;
using Android.Database.Sqlite;
using Android.Views.InputMethods;

namespace App2
{
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : Activity
    {
        SQLiteDatabase Database = null;//在最外称定义这个
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            //string tel = Intent.GetStringExtra("tel");
            //TextView tv1 = FindViewById<TextView>(Resource.Id.textView1);
            //tv1.Text = tel;

            SetContentView(Resource.Layout.Second);
            var Mgr = (NotificationManager)GetSystemService(NotificationService);
            Button btnpush = FindViewById<Button>(Resource.Id.btnPush);
            btnpush.Click += delegate
            {
                Notification notify = new Notification(Resource.Drawable.Icon, "你有新的短消息");
                //响声，震动，LED闪灯需要调用这个属性
                notify.Defaults = NotificationDefaults.All;
                //注意RingtoneManager这个需要引用 Android.Media;获取系统声音
                Android.Net.Uri uri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
                notify.Sound = uri;//铃声路径
                //震动方式数组形式，{震动时间，间隔时间，震动时间}
                notify.Vibrate = new long[] { 1000, 300, 1000 };
                //Color静态类需要引用这个 Android.Graphics
                //LED等是自动循环的不用像震动那样设置循环间隔
                notify.LedARGB = Color.Green;
                notify.LedOffMS = 1000;//显示时间
                notify.LedOnMS = 3000;//关闭时间
                notify.Flags = NotificationFlags.ShowLights | notify.Flags;
                var pIntent = PendingIntent.GetActivity(this, 0,
                    new Intent(this, typeof(ThirdActivity)), PendingIntentFlags.UpdateCurrent);
                notify.SetLatestEventInfo(this, "通知头", "通知内容", pIntent);
                Mgr.Notify(0, notify);//通知ID和通知
            };

            Button esearch = FindViewById<Button>(Resource.Id.search);
            esearch.Click += Esearch_Click;

        }

        private void Esearch_Click(object sender, EventArgs e)
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            
            EditText econtent = FindViewById<EditText>(Resource.Id.content);
            TextView etxtview = FindViewById<TextView>(Resource.Id.txtview);
            imm.HideSoftInputFromWindow(econtent.WindowToken, 0);
            string strDataName = "1122.db3";
            Database = this.OpenOrCreateDatabase(strDataName, FileCreationMode.Private, null);
            ICursor data = Database.RawQuery("select * from book where name='"+ econtent.Text.Trim()+"'", null);
            string ss = string.Empty;
            if (data.Count > 0)
            {
                while (data.MoveToNext())
                {
                    ss += data.GetString(0) + "|" + data.GetString(1) + "|" + data.GetString(2) + "|" + data.GetString(3) + "|";
                }
            }
            else
            {
                ICursor data2 = Database.RawQuery("select * from book", null);
                while (data2.MoveToNext())
                {
                    ss += data2.GetString(0) + "|"+ data2.GetString(1) + "|"+ data2.GetString(2) + "|"+ data2.GetString(3) + "|";
                }
            }
            etxtview.SetText(ss.Trim('|'),TextView.BufferType.Normal);
            Toast.MakeText(this, ss.Trim('|'), ToastLength.Long).Show();
            data.Close();
            Database.Close();
        }
    }
}