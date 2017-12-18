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
using Com.Ajts.Androidmads.Sqliteimpex;
using Android.Database;
using Android.Util;

namespace SQLiteImporterExporterSample
{
    class DbQueries
    {
        private Context context;
        private SQLiteDatabase database;
        private SQLiteImporterExporter dbHelper;

        public DbQueries(Context context)
        {
            this.context = context;
        }

        public DbQueries Open()
        {
            dbHelper = new SQLiteImporterExporter(context, MainActivity.db);
            database = dbHelper.WritableDatabase;
            return this;
        }

        public void close()
        {
            dbHelper.Close();
        }

        public long InsertDetail(String stud_name)
        {
            ContentValues values = new ContentValues();
            values.Put("student_name", stud_name);
            return database.Insert("student_details", null, values);
        }

        public List<String> GetDetail()
        {
            List<String> list = new List<String>();
            try
            {
                ICursor cursor;
                database = dbHelper.ReadableDatabase;
                cursor = database.RawQuery("SELECT * FROM student_details", null);
                list.Clear();
                if (cursor.Count > 0)
                {
                    if (cursor.MoveToFirst())
                    {
                        do
                        {
                            list.Add(cursor.GetString(cursor.GetColumnIndex("student_name")));
                        } while (cursor.MoveToNext());
                    }
                }
                cursor.Close();
            }
            catch (Exception e)
            {
                Log.Verbose("Exception", e.ToString());
            }
            return list;
        }
    }
}