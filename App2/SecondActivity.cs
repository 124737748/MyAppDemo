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
        SQLiteDatabase Database = null;//������ƶ������
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
                Notification notify = new Notification(Resource.Drawable.Icon, "�����µĶ���Ϣ");
                //�������𶯣�LED������Ҫ�����������
                notify.Defaults = NotificationDefaults.All;
                //ע��RingtoneManager�����Ҫ���� Android.Media;��ȡϵͳ����
                Android.Net.Uri uri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
                notify.Sound = uri;//����·��
                //�𶯷�ʽ������ʽ��{��ʱ�䣬���ʱ�䣬��ʱ��}
                notify.Vibrate = new long[] { 1000, 300, 1000 };
                //Color��̬����Ҫ������� Android.Graphics
                //LED�����Զ�ѭ���Ĳ���������������ѭ�����
                notify.LedARGB = Color.Green;
                notify.LedOffMS = 1000;//��ʾʱ��
                notify.LedOnMS = 3000;//�ر�ʱ��
                notify.Flags = NotificationFlags.ShowLights | notify.Flags;
                var pIntent = PendingIntent.GetActivity(this, 0,
                    new Intent(this, typeof(ThirdActivity)), PendingIntentFlags.UpdateCurrent);
                notify.SetLatestEventInfo(this, "֪ͨͷ", "֪ͨ����", pIntent);
                Mgr.Notify(0, notify);//֪ͨID��֪ͨ
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