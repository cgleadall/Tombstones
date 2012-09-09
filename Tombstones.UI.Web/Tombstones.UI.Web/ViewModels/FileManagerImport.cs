using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using Tombstones.UI.Web.Models;

namespace Tombstones.UI.Web.ViewModels
{
    public class FileManagerImport
    {
        public string UploadFileId { get; set; }
        public string FullPath { get; set; }
        public string FileName { get; set; }
        public string Category { get; set; }
        public ICollection<string> Headers { get; set; }
        public ICollection<object[]> SampleData { get; set; }
        
        public int ImportFromRow { get; set; }

        protected FileManagerImport()
        {
            Headers = new List<string>();
            ImportFromRow = 1;
        }

        public void ReadHeaders()
        {
            Headers = GetFileHeaders(FullPath);
        }

        public IEnumerable<object[]> ReadRowData(string fullPath)
        {
            DataColumnCollection headers;
            DataRowCollection rows;

            ReadFileData(fullPath, out headers, out rows);
            foreach (DataRow row in rows)
            {
                yield return row.ItemArray;
            }
            yield break;
        }

        public static FileManagerImport Create(Models.UploadedFile uploadFile)
        {
            var result = new FileManagerImport();

            if (uploadFile != null)
            {
                result.UploadFileId = uploadFile.Id;
                result.Category = uploadFile.Category;
                result.FileName = uploadFile.FileName;
                result.FullPath = uploadFile.FullPath;

                DataRowCollection rows;
                DataColumnCollection headers;
                ReadFileData(result.FullPath, out headers, out rows);
                result.Headers = GetHeaderStrings(headers);
                result.SampleData = GetSampleImportRecords(rows, 5);
            }
            return result;
        }

        protected static void ReadFileData(string fullFilePath, out DataColumnCollection headers, out DataRowCollection rows)
        {
            rows = ReadDataFromXLSFile(fullFilePath, "Sheet1", out headers);

        }

        protected static ICollection<string> GetFileHeaders(string fullFilePath)
        {
            var result = new List<string>();
            DataColumnCollection headers;
            var fileData = ReadDataFromXLSFile(fullFilePath, "Sheet1", out headers);

            if (headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    result.Add(header.ToString());
                }

            }
            return result;
        }

        protected static ICollection<string> GetHeaderStrings(DataColumnCollection headers)
        {
            var result = new List<string>(headers.Count);
            foreach (var header in headers)
            {
                result.Add(header.ToString());
            }
            return result;
        }

        protected static ICollection<object[]> GetSampleImportRecords(DataRowCollection rows, int howManyRows)
        {
            var result = new List<object[]>(howManyRows);
            for (int i = 0; i < howManyRows; i++)
            {
                var row = rows[i];
                result.Add(row.ItemArray);
            }
            return result;
        }
        protected static DataRowCollection ReadDataFromXLSFile(string filename, string sheetname, out DataColumnCollection headers)
        {
            var con =
                new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename +
                                    ";Extended Properties=Excel 8.0");
            var myDataSet = new DataSet();
            con.Open();
            //Create Dataset and fill with imformation from the Excel Spreadsheet for easier reference
            var myCommand =
                new OleDbDataAdapter(" SELECT * FROM [" + sheetname + "$] ", con);
            myCommand.Fill(myDataSet);
            con.Close();


            headers = myDataSet.Tables[0].Columns;

            return myDataSet.Tables[0].Rows;
        }

    }
}