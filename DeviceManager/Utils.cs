using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace DeviceManager
{
    public static class Utils
    {
        public static void WriteEX(Exception ex)
        {
            lock (lockObj)
            {
                File.AppendAllText("error.txt",ex.Message);
            }
        }

        public static object lockObj = new object();
        /// <summary>
        /// 发送邮件(此方法占用网络时间,需用开线程调用)
        ///<add key = "邮件接收人" value="mengft@txsec.com,527049200@qq.com" />
        ///<add key = "发件人地址" value="mfantasy@mfant.com" />
        ///<add key = "发件人昵称" value="设备提醒" />
        ///<add key = "发件服务器" value="smtp.qq.com" />
        ///<add key = "发件用户名" value="mfantasy@mfant.com" />
        ///<add key = "发件密码" value="ryqmwpnrmoygcbdd" />
        /// </summary>                
        public static void SendMail(string title, string body)
        {
            MailAddress EmailFrom = new MailAddress(ConfigurationManager.AppSettings["发件人地址"], ConfigurationManager.AppSettings["发件人昵称"]);
            MailMessage mailMsg = new MailMessage();
            mailMsg.From = EmailFrom;
            string receivers = ConfigurationManager.AppSettings["邮件接收人"];
            mailMsg.To.Add(receivers);
            mailMsg.Subject = title;
            mailMsg.Body = body;
            SmtpClient spClient = new SmtpClient(ConfigurationManager.AppSettings["发件服务器"]);
            spClient.Timeout = 600 * 1000;
            spClient.EnableSsl = true;
            spClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["发件用户名"], ConfigurationManager.AppSettings["发件密码"]);

            try
            {
                spClient.Send(mailMsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                lock (lockObj)
                {
                    File.AppendAllText("error.txt", ex.Message + "\r\n发送邮件失败\r\n" + title + "\r\n" + body + "\r\n" + DateTime.Now.ToString());
                }
            }
        }

        public static DataTable ExcelToTable(string fileName)
        {
            ISheet sheet = null;
            IWorkbook workbook = null;
            DataTable table = new DataTable();
            using (FileStream fs = File.OpenRead(fileName))
            {
                if (fileName.Contains(".xlsx"))
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.Contains(".xls"))
                {
                    workbook = new HSSFWorkbook(fs);
                }
                else
                {
                    throw new Exception("不是有效的Excel类型");
                }
            }
            sheet = workbook.GetSheetAt(0);
            IRow firstRow = sheet.GetRow(0);
            int cellCount = firstRow.LastCellNum;//列的总数
            for (int i = 0; i < cellCount; i++)
            {
                ICell cell = firstRow.GetCell(i);
                if (cell != null)
                {
                    string cellValue = cell.StringCellValue;
                    DataColumn column = new DataColumn(cellValue);
                    table.Columns.Add(column);
                }
            }
            int rowCount = sheet.LastRowNum;
            for (int i = 1; i < rowCount; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null)
                    continue;
                DataRow dataRow = table.NewRow();
                for (int j = 0; j < cellCount; j++)
                {
                    string value = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
                    dataRow[j] = value;
                }
                table.Rows.Add(dataRow);
            }
            return table;
        }

        /// <summary>
        /// 表中内容均为string
        /// </summary>                
        public static void ExportToXls(string fileName, DataTable dataTable)
        {
            HSSFWorkbook workBook = new HSSFWorkbook();
            ISheet sheet = workBook.CreateSheet();
            int i = 1;
            int jL = dataTable.Columns.Count;

            IRow r0 = sheet.CreateRow(0);//列头            
            int jj = 0;
            foreach (DataColumn item in dataTable.Columns)
            {
                ICell cell = r0.CreateCell(jj);
                cell.CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                cell.SetCellValue(item.ColumnName);
                jj++;
            }

            foreach (DataRow item in dataTable.Rows)
            {
                IRow row = sheet.CreateRow(i);
                for (int j = 0; j < jL; j++)
                {
                    ICell cell = row.CreateCell(j);
                    cell.SetCellValue(item[j].ToString());
                }
                i++;
            }

            using (FileStream fs = File.OpenWrite(fileName))
            {
                workBook.Write(fs);
            }
        }

        //序列化
        public static void ToFile<T>(string path, T obj)
        {
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path)))
            {
                File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path)).Close();
            }

            XmlSerializerFactory factory = new XmlSerializerFactory();
            XmlSerializer serializer = factory.CreateSerializer(typeof(T));

            try
            {
                using (FileStream fs = File.Open(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path), FileMode.Create))
                {
                    serializer.Serialize(fs, obj);
                    fs.Flush();
                }
            }
            catch (Exception e)
            {
                StringBuilder error = new StringBuilder();
                error.AppendLine(e.Message);
                error.AppendLine();
                error.AppendFormat("{0}", path);
                error.AppendLine();
                error.AppendFormat("{0}", AppDomain.CurrentDomain.BaseDirectory);
                MessageBox.Show(error.ToString());
            }
        }

        internal static T FromXMLFile<T>(string path)
        {
            var x = new XmlDocument();
            x.Load(path);
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path)))
            {
                throw new Exception(string.Format("没有找到配置文件:{0}", Path.GetFullPath(path)));
            }
            else
            {
                try
                {
                    XmlSerializerFactory factory = new XmlSerializerFactory();
                    XmlSerializer serializer = factory.CreateSerializer(typeof(T));

                    if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path)))
                    {
                        using (FileStream fs = File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path)))
                        {
                            if (fs != null && fs.Length > 0)
                            {
                                object cacheData = serializer.Deserialize(fs);
                                return cacheData == null ? default(T) : (T)cacheData;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                return default(T);
            }
        }

        public static string GetUserPath()
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (!Directory.Exists(Path.Combine(dir, @"msoft\config\")))
            {
                Directory.CreateDirectory(Path.Combine(dir, @"msoft\config\"));
            }
            return Path.Combine(dir, @"msoft\config\");
        }
        /// <summary>
        /// 可逆加密
        /// </summary>                
        public static string Encode(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes("mfantasy");
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes("mfantasy");
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }
        /// <summary>
        /// 可逆解密
        /// </summary>        
        public static string Decode(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes("mfantasy");
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes("mfantasy");
            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }
    }
}
