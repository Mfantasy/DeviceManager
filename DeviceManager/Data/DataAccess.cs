using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager
{
    public static class DataAccess
    {
        /// <summary>
        /// 验证用户名密码(返回 0:认证失败,1:管理员[库中写死],2:普通用户)
        /// </summary>                        
        public static int Logon(string acc, string pwd)
        {
            //首先查询数据库中账户acc的用户权限,如果存在查询密码对不对,如果不存在返回错
            if (acc == "admin" && pwd == "admin")
                return 1;
            else
                return 0;
        }
    }

    public static class SqlLiteHelper
    {
        //public static string conStr = "Data Source=" + Environment.CurrentDirectory + "\\test.db;Initial Catalog=sqlite;";
        public static string conStr = "Data Source=" + ConfigurationManager.AppSettings["dbPath"] + ";Initial Catalog=sqlite;";
        static SQLiteConnection con = new SQLiteConnection(conStr);
        public static DataTable ExecuteReader(string cmdText)
        {
            try
            {
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = cmdText;
                SQLiteDataAdapter mAdapter = new SQLiteDataAdapter(cmd);
                DataTable result = new DataTable();
                mAdapter.Fill(result);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }
   
        }

        public static void ExecuteReader0(string cmdText)
        {          
            SQLiteCommand cmd = con.CreateCommand();            
            cmd.CommandText = cmdText;
            con.Open();
            SQLiteDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                Console.WriteLine(reader[0]);
            }

        }
    }

    public static class MySqlHelper
    {
        static string conStr = ConfigurationManager.AppSettings["conStrMySql"];
        static MySqlConnection con = new MySqlConnection(conStr);
        //增删改
        public static void ExecNonQuery(string cmdText)
        {
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = cmdText;
            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        //用来查用户名权限
        public static object ExecuteScalar(string cmdText, bool close)
        {
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = cmdText;
            if (con.State != ConnectionState.Open)
                con.Open();
            object obj = new object();
            try
            {
                obj = cmd.ExecuteScalar();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (close)
                    con.Close();
            }
        }

        public static void CloseCon()
        {
            con.Close();
        }
        //查询数据表
        public static DataTable ExecuteReader(string cmdText)
        {
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = cmdText;
            MySqlDataAdapter mAdapter = new MySqlDataAdapter(cmd);
            DataTable result = new DataTable();
            mAdapter.Fill(result);
            return result;
        }

        public static List<string[]> ExecuteReader(string cmdText, string[] colnames, bool close)
        {
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = cmdText;
            if (con.State != ConnectionState.Open)
                con.Open();
            List<string[]> list = new List<string[]>();
            MySqlDataReader reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    string[] row = new string[colnames.Length];
                    for (int i = 0; i < colnames.Length; i++)
                    {
                        row[i] = reader[colnames[i]].ToString();
                    }
                    list.Add(row);
                }
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (close)
                    con.Close();
            }
        }


    }
}
