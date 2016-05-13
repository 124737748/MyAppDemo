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
using Android.Database;

namespace App2
{
    [Activity(Label = "FourActivity")]
    public class FourActivity : Activity
    {
        public SQLiteDatabase Database { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string strDataName = "1122.db3";
            Database = this.OpenOrCreateDatabase(strDataName, FileCreationMode.Private, null);
            EditText fl = FindViewById<EditText>(Resource.Id.editText1);
            ICursor data = Database.RawQuery("select * from book", null);
            string ss = string.Empty;
            while (data.MoveToNext())
            {
                ss+=data.GetString(0)+"|";
            }

            Toast.MakeText(this, ss,ToastLength.Long).Show();


            data.Close();
            Database.Close();

            // Create your application here
        }
    }
}