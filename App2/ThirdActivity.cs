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
using Android.Database.Sqlite;

namespace App2
{
    [Activity(Label = "你好")]
    public class ThirdActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            CreateData();

            // Create your application here
            SetContentView(Resource.Layout.Third);
            Button btnc = FindViewById<Button>(Resource.Id.createdatabase);
            btnc.Click += delegate
            {
                //要跳转的地址
                ContentValues cv = new ContentValues();
                cv.Put("name", "111222");
                cv.Put("sex", "女");
                cv.Put("age", "20");
                cv.Put("phone", "13212125421");
                long a = Database.Insert("book", null, cv);
                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetMessage("保存成功");
                callDialog.SetNeutralButton("确定", delegate
                {
                    var four = new Intent(this, typeof(FourActivity));
                    //如果不写参数则是无参数跳转

                    StartActivity(four);
                });
                callDialog.Show();
            };
        }

        SQLiteDatabase Database = null;//在最外称定义这个

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