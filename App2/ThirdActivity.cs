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
    [Activity(Label = "���")]
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
                //Ҫ��ת�ĵ�ַ
                ContentValues cv = new ContentValues();
                cv.Put("name", "111222");
                cv.Put("sex", "Ů");
                cv.Put("age", "20");
                cv.Put("phone", "13212125421");
                long a = Database.Insert("book", null, cv);
                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetMessage("����ɹ�");
                callDialog.SetNeutralButton("ȷ��", delegate
                {
                    var four = new Intent(this, typeof(FourActivity));
                    //�����д���������޲�����ת

                    StartActivity(four);
                });
                callDialog.Show();
            };
        }

        SQLiteDatabase Database = null;//������ƶ������

        /// <summary>
        /// �������ݿ�
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