# SQLiteImporterExporter
A light weight library for exporting and importing sqlite database in android

## How to Download
You can download the library using Nuget
```csharp
Install-Package SQLiteImporterExporter -Version 1.0.0
```
If the nuget is not compatible to your project, then you can download and include the directly from [here](https://github.com/androidmads/XASQLiteImporterExporter/tree/master/dll)
## How to use this Library:
This Library is used to import SQLite Database from Assets or External path and Export/Backup SQLite Database to external path.

```csharp
SQLiteImporterExporter sqLiteImporterExporter = new SQLiteImporterExporter(ApplicationContext, db);

// Listeners for Import and Export DB
sqLiteImporterExporter.ImportListener = new ImportExportListener("I");
sqLiteImporterExporter.ExportListener = new ImportExportListener("E");

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
```

#### To Import SQLite from Assets 
```csharp
try
{
	sqLiteImporterExporter.ImportDataBaseFromAssets();
}
catch (Exception ex)
{
	ex.PrintStackTrace();
}
```

#### To import from external storage
```csharp
try
{
	sqLiteImporterExporter.ImportDataBase(path);
}
catch (Exception ex)
{
	ex.PrintStackTrace();
}
```

#### To export to external storage
```csharp
try
{
	sqLiteImporterExporter.ExportDataBase(path);
}
catch (Exception ex)
{
	ex.PrintStackTrace();
}
```
#### License
```
MIT License

Copyright (c) 2017 AndroidMad / Mushtaq M A

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
