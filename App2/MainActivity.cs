using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Database.Sqlite;

namespace App2
{
    [Activity(Label = "MyDemo", MainLauncher = true, Icon = "@drawable/logo")]
    public class MainActivity : Activity
    {
        int count = 1;

        SQLiteDatabase Database = null;//在最外称定义这个
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            CreateData();
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            EditText txtPhone = FindViewById<EditText>(Resource.Id.txtPhone);
            Button button2 = FindViewById<Button>(Resource.Id.MyButton2);
            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            button.Click += delegate
            {
                //创建 是否类型提示框

                var callDialog = new AlertDialog.Builder(this);

                //提示框信息

                callDialog.SetMessage("是否开始通话？");

                //确定按钮的文字和事件

                callDialog.SetNeutralButton("通话", delegate

                {

                    //创建打电话的事件

                    var call = new Intent(Intent.ActionCall);

                    //要打给的电话号是多少

                    call.SetData(Android.Net.Uri.Parse("tel:" + txtPhone.Text));

                    //执行这个事件

                    StartActivity(call);

                });

                //取消按钮的文字和事件

                callDialog.SetNegativeButton("取消", delegate { });

                //显示出来。

                callDialog.Show();
            };

            button2.Click += delegate {
                //要跳转的地址
                var second = new Intent(this, typeof(SecondActivity));
                //带过去的参数（key,value)
                second.PutExtra("tel", txtPhone.Text);
                //执行跳转
                StartActivity(second);
            };


            Button commit = FindViewById<Button>(Resource.Id.commit);
            commit.Click += Commit_Click;
        }

        private void Commit_Click(object sender, EventArgs e)
        {
            EditText ename = FindViewById<EditText>(Resource.Id.name);
            EditText esex = FindViewById<EditText>(Resource.Id.sex);
            EditText eage = FindViewById<EditText>(Resource.Id.age);
            EditText emobile = FindViewById<EditText>(Resource.Id.mobile);
            ContentValues cv = new ContentValues();
            cv.Put("name", ename.Text.Trim());
            cv.Put("sex", esex.Text);
            cv.Put("age", eage.Text);
            cv.Put("phone", emobile.Text);
            long a = Database.Insert("book", null, cv);
            var callDialog = new AlertDialog.Builder(this);
            callDialog.SetMessage("保存成功");
            callDialog.Show();
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        private void CreateData()
        {
            string strDataName = "1122.db3";
            Database = this.OpenOrCreateDatabase(strDataName, FileCreationMode.Private, null);
            string strTable = "create table if not exists  book(name,sex,age,phone)";
            Database.ExecSQL(strTable);

        }

    }
}

