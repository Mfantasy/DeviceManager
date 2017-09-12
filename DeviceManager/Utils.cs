using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
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
        public static void SetDoubleBuffer(Control ctrl)
        {
            ctrl.GetType().GetProperty("DoubleBuffered",
  System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(ctrl,
  true, null);
        }

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

        public static void ExcelTest()
        {
            HSSFWorkbook workBook = new HSSFWorkbook();
            ISheet st = workBook.CreateSheet("yyyy.M.d");
            IRow r0 = st.CreateRow(0);
            r0.Height = 200;
            IRow r1 = st.CreateRow(1);
            st.SetColumnBreak(1);
            st.SetColumnBreak(0);
            ICell c1 = r1.CreateCell(0);
            ICell c2 = r1.CreateCell(1);
            ICell c3 = r1.CreateCell(2);
            c1.SetCellValue ( "XXXSB泰斯特斯特斯特斯特" );         
            IRow r2 = st.CreateRow(2);
            ICell c21 = r2.CreateCell(0);
            ICell c22 = r2.CreateCell(1);
            ICell c23 = r2.CreateCell(2);
            c21.SetCellValue("c21");
            c22.SetCellValue("c22");
            c23.SetCellValue("c23");

            CellRangeAddress cra = new CellRangeAddress(1, 1, 0, 1);
            st.AddMergedRegion(cra);

            using (FileStream fs = File.OpenWrite("xxx.xls"))
            {
                workBook.Write(fs);
            }
        }

        public static string FmtExcel(DateTime dt,string fileName)
        {
            HSSFWorkbook workBook = new HSSFWorkbook();
            ISheet st = workBook.CreateSheet(dt.ToString("yyyy.M.d"));
            //先创建cellstyle
            
            ICellStyle r1s = workBook.CreateCellStyle();
            r1s.FillForegroundColor = HSSFColor.Grey40Percent.Index;
            r1s.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            r1s.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            r1s.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            r1s.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            IFont font = workBook.CreateFont();
            font.IsBold = true;
            font.FontName = "宋体";
            font.FontHeightInPoints = 14;
            r1s.SetFont(font);
            r1s.FillPattern = FillPattern.SolidForeground;

            ICellStyle r2s = workBook.CreateCellStyle();
            r2s.FillForegroundColor = HSSFColor.LemonChiffon.Index;
            r2s.FillPattern = FillPattern.SolidForeground;
            r2s.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            r2s.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            r2s.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            r2s.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            IFont font2 = workBook.CreateFont();
            font2.IsBold = true;
            font2.FontHeightInPoints = 12;
            font2.FontName = "宋体";
            r2s.SetFont(font2);

            ICellStyle r3s = workBook.CreateCellStyle();
            r3s.FillForegroundColor = HSSFColor.Grey25Percent.Index;
            r3s.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            r3s.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            r3s.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            r3s.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            r3s.FillPattern = FillPattern.SolidForeground;
            IFont font3 = workBook.CreateFont();
            font3.FontName = "宋体";
            font3.IsBold = true;
            font3.FontHeightInPoints = 10;
            r3s.SetFont(font3);

            ICellStyle rfs = workBook.CreateCellStyle();
            //rfs.FillForegroundColor = HSSFColor.LemonChiffon.Index;
            //rfs.FillPattern = FillPattern.SolidForeground;
            rfs.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            rfs.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            rfs.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            rfs.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            ICellStyle rf0s = workBook.CreateCellStyle();
            rf0s.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            rf0s.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            rf0s.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            rf0s.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            IFont frfs = workBook.CreateFont();
            frfs.FontName = "宋体";
            IFont frf0s = workBook.CreateFont();
            frf0s.FontName = "宋体";
            frfs.FontHeightInPoints = 10;
            frf0s.FontHeightInPoints = 10;
            frf0s.IsBold = true;
            rf0s.SetFont(frf0s);
            rfs.SetFont(frfs);

            //榆社化石博物馆传感监测典型数据统计(2017 / 8 / 6)
            IRow r0 = st.CreateRow(0);
            ICellStyle r0s = workBook.CreateCellStyle();
            IFont font0 = workBook.CreateFont();
            font0.IsBold = true;
            font0.FontHeightInPoints = 18;
            font0.FontName = "黑体";
            r0s.SetFont(font0);
            ICell cr0 = r0.CreateCell(0);
            cr0.SetCellValue("榆社化石博物馆传感监测典型数据统计("+dt.ToString("yyyy/M/d")+")");
            cr0.CellStyle = r0s;

            CellRangeAddress crar0 = new CellRangeAddress(0, 0, 0, 5);
            st.AddMergedRegion(crar0);

            st.CreateRow(1).Height = 200;

            foreach (var g1 in ConfigData.GroupConfigRoot.GroupConfig1s)
            {               
                IRow row = st.CreateRow(st.LastRowNum+1);                
                ICell c0 = row.CreateCell(0);                
                c0.SetCellValue(g1.Name);
                ICell c0sp = row.CreateCell(1);
                c0sp.SetCellValue("");
                ICell c1 = row.CreateCell(2);
                c1.SetCellValue("数值");
                ICell c2 = row.CreateCell(3);
                c2.SetCellValue("节点号");
                ICell c3 = row.CreateCell(4);
                c3.SetCellValue("时间");
                ICell c4 = row.CreateCell(5);                                
                c4.SetCellValue("备注");

                CellRangeAddress cra = new CellRangeAddress(c0.RowIndex, c0.RowIndex, c0.ColumnIndex, c0.ColumnIndex+1);
                st.AddMergedRegion(cra);
                c0sp.CellStyle = r1s;
                c0.CellStyle = r1s;
                c1.CellStyle = r1s;
                c2.CellStyle = r1s;
                c3.CellStyle = r1s;
                c4.CellStyle = r1s;
                foreach (var g2 in g1.GroupConfigs)
                {
                    IRow row2 = st.CreateRow(st.LastRowNum+1);                                  
                    ICell c20 = row2.CreateCell(0);
                    c20.CellStyle = r2s;
                    c20.SetCellValue("  "+g2.Name);
                    ICell c21 = row2.CreateCell(1);
                    c21.CellStyle = r2s;
                    c21.SetCellValue("");
                    ICell c22 = row2.CreateCell(2);
                    c22.CellStyle = r2s;
                    c22.SetCellValue("");
                    ICell c23 = row2.CreateCell(3);
                    c23.CellStyle = r2s;
                    c23.SetCellValue("");
                    ICell c24 = row2.CreateCell(4);
                    c24.CellStyle = r2s;
                    c24.SetCellValue("");
                    ICell c25 = row2.CreateCell(5);
                    c25.CellStyle = r2s;
                    c25.SetCellValue("");

                    CellRangeAddress cra20 = new CellRangeAddress(c20.RowIndex, c20.RowIndex, 0, 1);
                    st.AddMergedRegion(cra20);

                    foreach (var g3 in g2.GroupConfigs)
                    {
                        IRow row3 = st.CreateRow(st.LastRowNum+1);                       
                        ICell c30 = row3.CreateCell(0);
                        c30.SetCellValue("     "+g3.Name);
                        c30.CellStyle = r3s;
                        ICell c31 = row3.CreateCell(1);
                        c31.SetCellValue("");
                        c31.CellStyle = r3s;
                        ICell c32 = row3.CreateCell(2);
                        c32.SetCellValue("");
                        c32.CellStyle = r3s;
                        ICell c33 = row3.CreateCell(3);
                        c33.SetCellValue("");
                        c33.CellStyle = r3s;
                        ICell c34 = row3.CreateCell(4);
                        c34.SetCellValue("");
                        c34.CellStyle = r3s;
                        ICell c35 = row3.CreateCell(5);
                        c35.SetCellValue("");
                        c35.CellStyle = r3s;                                                

                        bool ir1 = true;
                        foreach (var s in g3.Sensors)
                        {                        
                            foreach (var f in s.Model.Fields)
                            {
                                if (!f.Realtime) continue;
                                IRow rf = st.CreateRow(st.LastRowNum+1);
                                ICell cf0 = rf.CreateCell(0);
                                cf0.CellStyle = rf0s;
                                if (ir1)
                                {
                                    ir1 = false;                                    
                                    cf0.SetCellValue("包括:");
                                }
                                else
                                {
                                    cf0.SetCellValue("");
                                }
                                ICell cf1 = rf.CreateCell(1);
                                cf1.SetCellValue(f.Name + "(" + f.Unit + ")");
                                cf1.CellStyle = rfs;
                                ICell cf2 = rf.CreateCell(2);
                                cf2.SetCellValue(f.GetValue(dt));
                                cf2.CellStyle = rfs;
                                ICell cf3 = rf.CreateCell(3);
                                cf3.SetCellValue(s.NodeId.ToString());
                                cf3.CellStyle = rfs;
                                ICell cf4 = rf.CreateCell(4);
                                cf4.SetCellValue(dt.ToString("H:mm"));
                                cf4.CellStyle = rfs;
                                ICell cf5 = rf.CreateCell(5);
                                cf5.SetCellValue(s.Comment);
                                cf5.CellStyle = rfs;
                            }                          
                        }
                    }
                }
            }
            //自动宽度
            
            st.AutoSizeColumn(1);
            st.AutoSizeColumn(2);
            st.AutoSizeColumn(3);
            st.AutoSizeColumn(4);
            st.SetColumnWidth(0, 4000);
            st.SetColumnWidth(5, 9000);
            //16_00榆社化石博物馆传感监测典型数据统计表2017.8.6.xls            
            using (FileStream fs = File.OpenWrite(fileName))
            {
                workBook.Write(fs);
            }
            return fileName;
        }
    }
}
