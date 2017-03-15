using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            return 0;
        }
    }

    public static class SqlHelper
    {
        static string conStr = ConfigurationManager.AppSettings["conStr"];
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
                con.Clone();
            }
        }
        //用来查用户名权限
        public static object ExecuteScalar(string cmdText)
        {
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = cmdText;
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
                con.Clone();
            }
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


    }
}
