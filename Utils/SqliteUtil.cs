using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using GenericCefSharp.Setting;
using Dapper;
using System.Data;
using System.Windows.Forms;

namespace GenericCefSharp.Utils
{
    
    class SqliteUtil
    {
        public enum eSetting
        {
            js,
            html, api
        }
        public static Dictionary<String, String> config = new Dictionary<String, String>();

        static string SQLITE_DB = "setting.db";
        static string SQL_CreateSettingTable = @"
            CREATE TABLE IF NOT EXISTS tbSetting (
	            id int PRIMARY KEY,
   	            name Text NOT NULL,
	            val Text DEFAULT ''
            )";
        static string SQL_CreateSettingTableIndex = @"
                    CREATE UNIQUE INDEX IF NOT EXISTS idx_tbSetting  ON tbSetting (name);
            ";
        
        static public void initSetting()
        {
            string cs = String.Format("URI=file:{0}", SQLITE_DB); 

            var con = new SQLiteConnection(cs);
            try
            {
                con.Open();

                using (SQLiteCommand command = con.CreateCommand())
                {
                    command.CommandText = SQL_CreateSettingTable;
                    command.ExecuteNonQuery();

                    command.CommandText = SQL_CreateSettingTableIndex;
                    command.ExecuteNonQuery();

                    defaultValue(command);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally{
                con.Close();
            }
        }
        static private void defaultValue(SQLiteCommand command)
        {
            String []sql = new String[]{ "INSERT OR IGNORE INTO tbSetting(id, name, val) VALUES(1, 'url', 'http://localhost:8080/api')",
                                         "INSERT OR IGNORE INTO tbSetting(id, name, val) VALUES(2, 'printerName', 'pdfPrint')",
                                         "INSERT OR IGNORE INTO tbSetting(id, name, val) VALUES(3, 'html', 'D:\\Projects\\temp\\my-project\\build\\index.html')",
                                         "INSERT OR IGNORE INTO tbSetting(id, name, val) VALUES(4, 'js', 'D:\\Projects\\GenericCefSharp\\HTMLResources\\dotnet.js')",
                                        };

            for (int i = 0; i < sql.Length; i++)
            {
                command.CommandText = sql[i];
                command.ExecuteNonQuery();
            }
        }
        public static Dictionary<String, String> getSettings()
        {
            string cs = String.Format("URI=file:{0}", SQLITE_DB);

            var con = new SQLiteConnection(cs);
            config.Clear();

            try
            {
                con.Open();
                var a = con.Query<clsSetting>("Select * From tbSetting").ToList();
                a.ForEach(
                    r =>
                    {
                        config.Add(r.Name, r.val);
                    }
                    );
            }
            finally
            {
                con.Close();
            }
            return config;
        }
        public static void getSettings(System.Windows.Forms.DataGridView grdData)
        {
            string cs = String.Format("URI=file:{0}", SQLITE_DB);

            var con = new SQLiteConnection(cs);
            try
            {
                con.Open();
                var a = con.Query<clsSetting>("Select * From tbSetting").ToList();
                a.ForEach(
                    r =>
                    {
                        grdData.Rows.Add(r.Id, r.Name, r.val);
                    }
                    );
            }
            finally
            {
                con.Close();
            }
            
            
        }
        public static void saveSettings(DataGridView grdData)
        {
            string SQL_UpdateSetting = @"
                Update tbSetting set val = @value where id = @id
            ";
            string cs = String.Format("URI=file:{0}", SQLITE_DB);

            var con = new SQLiteConnection(cs);
            try
            {
                con.Open();

                using (SQLiteCommand command = con.CreateCommand())
                {
                    command.CommandText = SQL_UpdateSetting;
                    command.Parameters.Add("@id", DbType.Int32);
                    command.Parameters.Add("@value", DbType.String);

                    foreach (DataGridViewRow row in grdData.Rows) {

                        command.Parameters["@id"].Value = row.Cells["id"].Value;
                        command.Parameters["@value"].Value = row.Cells["value"].Value;

                        command.ExecuteNonQuery();

                    }
                }
            }
            finally
            {
                con.Close();
            }
        }
    }
}
