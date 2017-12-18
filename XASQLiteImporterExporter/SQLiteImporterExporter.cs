using Android.Content;
using Android.Database.Sqlite;
using Java.IO;
using System;
using System.IO;

namespace Com.Ajts.Androidmads.Sqliteimpex
{
    public class SQLiteImporterExporter : SQLiteOpenHelper
    {
        private Context context;
        private String DB_PATH;
        private String DB_NAME;
        public Listener ImportListener;
        public Listener ExportListener;

        public SQLiteImporterExporter(Context context, String DB_NAME) : base(context, DB_NAME, null, 1)
        {
            this.context = context;
            this.DB_NAME = DB_NAME;
            DB_PATH = Environment.GetFolderPath(Environment.SpecialFolder.Personal); //"/data/data/" + context.PackageName + "/databases/";
        }

        public override void OnCreate(SQLiteDatabase db)
        {
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
        }

        public bool IsDataBaseExists()
        {
            Java.IO.File dbFile = new Java.IO.File(DB_PATH + DB_NAME);
            return dbFile.Exists();
        }

        public void ImportDataBaseFromAssets()
        {
            Stream myInput = null;
            FileStream myOutput = null;
            try
            {
                myInput = context.Assets.Open(DB_NAME);
                String outFileName = DB_PATH + DB_NAME;
                myOutput = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
                byte[] buffer = new byte[1024];
                int length;
                while ((length = myInput.Read(buffer, 0, buffer.Length)) > 0)
                {
                    myOutput.Write(buffer, 0, length);
                }
                if (ImportListener != null)
                    ImportListener.OnSuccess("Successfully Imported");
            }
            catch (Exception e)
            {
                if (ImportListener != null)
                    ImportListener.OnFailure(e);
            }
            finally
            {
                // Close the streams
                try
                {
                    myOutput.Flush();
                    myOutput.Close();
                    myInput.Close();
                }
                catch (Exception e)
                {
                    if (ImportListener != null)
                        ImportListener.OnFailure(e);
                }
            }
        }

        public void ImportDataBase(String path)
        {
            FileStream myInput = null;
            FileStream myOutput = null;
            try
            {
                String inFileName = path + DB_NAME;
                myInput = new FileStream(inFileName, FileMode.OpenOrCreate, FileAccess.Read);
                String outFileName = DB_PATH + DB_NAME;
                myOutput = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write);

                byte[] buffer = new byte[1024];
                int length;
                while ((length = myInput.Read(buffer, 0, buffer.Length)) > 0)
                {
                    myOutput.Write(buffer, 0, length);
                }
                if (ImportListener != null)
                    ImportListener.OnSuccess("Successfully Imported");
            }
            catch (Exception e)
            {
                if (ImportListener != null)
                    ImportListener.OnFailure(e);
            }
            finally
            {
                try
                {
                    myOutput.Flush();
                    myOutput.Close();
                    myInput.Close();
                }
                catch (Java.IO.IOException ioe)
                {
                    if (ImportListener != null)
                        ImportListener.OnFailure(ioe);
                }
            }

        }

        public void ExportDataBase(String path)
        {
            FileStream myInput = null;
            FileStream myOutput = null;
            try
            {
                String inFileName = DB_PATH + DB_NAME;
                myInput = new FileStream(inFileName, FileMode.OpenOrCreate, FileAccess.Read);
                String outFileName = path + DB_NAME;
                myOutput = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
                byte[] buffer = new byte[1024];
                int length;
                while ((length = myInput.Read(buffer, 0, buffer.Length)) > 0)
                {
                    myOutput.Write(buffer, 0, length);
                }
                if (ExportListener != null)
                    ExportListener.OnSuccess("Successfully Exported");
            }
            catch (Exception e)
            {
                if (ExportListener != null)
                    ExportListener.OnFailure(e);
            }
            finally
            {
                try
                {
                    myOutput.Flush();
                    myOutput.Close();
                    myInput.Close();
                }
                catch (Exception ex)
                {
                    if (ExportListener != null)
                        ExportListener.OnFailure(ex);
                }
            }
        }

        public interface Listener
        {
            void OnSuccess(String message);

            void OnFailure(Exception exception);
        }

    }
}
