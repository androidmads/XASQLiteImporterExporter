using Android.App;
using Android.Widget;
using Android.OS;
using Com.Ajts.Androidmads.Sqliteimpex;
using Environment = Android.OS.Environment;
using Exception = Java.Lang.Exception;
using String = System.String;
using System;
using Android.Util;

namespace SQLiteImporterExporterSample
{
    [Activity(Label = "SQLiteImporterExporterSample", MainLauncher = true)]
    public class MainActivity : Activity
    {
        SQLiteImporterExporter sqLiteImporterExporter;
        String path = Environment.ExternalStorageDirectory.AbsolutePath + "/";
        public static String db = "external_db_android.sqlite";

        DbQueries dbQueries;

        EditText edtName;
        ListView listView;
        ArrayAdapter<String> adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            dbQueries = new DbQueries(ApplicationContext);
            sqLiteImporterExporter = new SQLiteImporterExporter(ApplicationContext, db);

            edtName = (EditText)FindViewById(Resource.Id.edtName);
            listView = (ListView)FindViewById(Resource.Id.listView);

            ReadDB();

            FindViewById(Resource.Id.btnDBExists).Click += CheckClickEvent;
            FindViewById(Resource.Id.btnImportFromAssets).Click += ImportAssetsClickEvent;
            FindViewById(Resource.Id.btnImportFromExt).Click += ImportExternalClickEvent;
            FindViewById(Resource.Id.btnExportToExt).Click += ExxportExternalClickEvent;

            sqLiteImporterExporter.ImportListener = new ImportExportListener("I");
            sqLiteImporterExporter.ExportListener = new ImportExportListener("E");

        }

        private void ImportExternalClickEvent(object sender, EventArgs e)
        {
            try
            {
                sqLiteImporterExporter.ImportDataBase(path);
            }
            catch (Exception ex)
            {
                ex.PrintStackTrace();
            }
            ReadDB();
        }

        private void ExxportExternalClickEvent(object sender, EventArgs e)
        {
            try
            {
                sqLiteImporterExporter.ExportDataBase(path);
            }
            catch (Exception ex)
            {
                ex.PrintStackTrace();
            }
            ReadDB();
        }

        private void ImportAssetsClickEvent(object sender, EventArgs e)
        {
            try
            {
                sqLiteImporterExporter.ImportDataBaseFromAssets();
            }
            catch (Exception ex)
            {
                ex.PrintStackTrace();
            }
            ReadDB();
        }

        private void CheckClickEvent(object sender, EventArgs e)
        {
            if (sqLiteImporterExporter.IsDataBaseExists())
            {
                Toast.MakeText(ApplicationContext, "DB Exists", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(ApplicationContext, "DB Doesn't Exists", ToastLength.Short).Show();
            }
        }

        private void ReadDB()
        {
            if (sqLiteImporterExporter.IsDataBaseExists())
            {
                dbQueries.Open();
                adapter = new ArrayAdapter<String>(ApplicationContext, Resource.Layout.list_item, dbQueries.GetDetail());
                listView.Adapter = adapter;
                dbQueries.close();
            }
            else
            {
                Toast.MakeText(ApplicationContext, "DB Doesn't Exists", ToastLength.Short).Show();
            }
        }

        class ImportExportListener : SQLiteImporterExporter.Listener
        {
            private string v;

            public ImportExportListener(string v)
            {
                this.v = v;
            }

            public void OnFailure(System.Exception exception)
            {
                Log.Error("mode : " + v, exception.Message);
            }

            public void OnSuccess(string message)
            {
                Log.Verbose("mode : " + v, message);
            }
        }

    }
}

