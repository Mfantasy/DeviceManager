using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DeviceManager
{
    public static class Utils
    {
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
                    StringBuilder error = new StringBuilder();
                    error.AppendLine(e.Message);
                    error.AppendLine();
                    error.AppendFormat("{0}", path);
                    error.AppendLine();
                    error.AppendFormat("{0}", AppDomain.CurrentDomain.BaseDirectory);
                    //MessageBox.Show(error.ToString());
                }
                return default(T);
            }
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
