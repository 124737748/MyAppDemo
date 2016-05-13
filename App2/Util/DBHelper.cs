using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Database.Sqlite;
using Android.Database;
public class DBHelper: Activity
{
    SQLiteDatabase Database = null;//在最外称定义这个

    /// <summary>
    /// 创建数据库
    /// </summary>
    public void CreateData()
    {
        string strDataName = "1122.db3";
        Database = this.OpenOrCreateDatabase(strDataName, FileCreationMode.Private, null);
        string strTable = "create table if not exists  book(name,sex,age,phone)";
        Database.ExecSQL(strTable);

    }
}