using DeviceManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DeviceManagerO
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = "tt.xlsx";
            List<Manager> mstt = new List<Manager>();
            DataTable ttTable = Utils.ExcelToTable(fileName);
            for (int i = 0; i < ttTable.Rows.Count; i++)
            {
                Manager m = new Manager();
                m.Name = ttTable.Rows[i][0].ToString();
                m.Insititution = ttTable.Rows[i][1].ToString();
                string[] ids = ttTable.Rows[i][2].ToString().Split(',');
                List<string> fids = new List<string>();
                foreach (string str in ids)
                {
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        fids.Add(str);
                    }
                }
                m.FundIds = fids;
                string[] names = ttTable.Rows[i][3].ToString().Split(',');
                List<string> fnames = new List<string>();
                foreach (string str in names)
                {
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        fnames.Add(str);
                    }
                }
                m.FundNames = fnames;
                m.FundCount = ttTable.Rows[i][4].ToString();
                m.Time = ttTable.Rows[i][5].ToString();
                m.Days = ttTable.Rows[i][6].ToString();
                m.Capital = ttTable.Rows[i][7].ToString();
                m.Rate = ttTable.Rows[i][8].ToString();
                mstt.Add(m);
            }
            //天相数据
            //string fileName2 = "tx.xls";
            //List<Manager> mstx = new List<Manager>();
            //DataTable txTable = Utils.ExcelToTable(fileName2);
            //for (int i = 0; i < txTable.Rows.Count; i++)
            //{
            //    Manager m = new Manager();
            //    m.Name = txTable.Rows[i][0].ToString();
            //    m.Insititution = txTable.Rows[i][1].ToString();
            //    string[] ids = txTable.Rows[i][2].ToString().Split(',');
            //    List<string> fids = new List<string>();
            //    foreach (string str in ids)
            //    {
            //        if (!string.IsNullOrWhiteSpace(str))
            //        {
            //            fids.Add(str);
            //        }
            //    }
            //    m.FundIds = fids;
            //    string[] names = txTable.Rows[i][3].ToString().Split(',');
            //    List<string> fnames = new List<string>();
            //    foreach (string str in names)
            //    {
            //        if (!string.IsNullOrWhiteSpace(str))
            //        {
            //            fnames.Add(str);
            //        }
            //    }
            //    m.FundNames = fnames;
            //    m.FundCount = txTable.Rows[i][4].ToString();
            //    m.Time = txTable.Rows[i][5].ToString();                
            //    m.Capital = txTable.Rows[i][6].ToString();
            //    m.Rate = txTable.Rows[i][7].ToString();
            //    mstx.Add(m);
            //}    
            DataTable txDataTable = MySqlHelper.ExecuteReader(sql1);
            List<Manager> mstx = new List<Manager>();
            int rCount = txDataTable.Rows.Count;
            for (int i = 0; i < rCount; i++)
            {
                Manager m = new Manager();
                string mid = txDataTable.Rows[i][0].ToString();
                m.TXID = mid;
                List<string[]> list = MySqlHelper.ExecuteReader(string.Format(sql2, mid), new string[] { "f_fund_code", "f_fund_name" }, false);
                List<string> ids = new List<string>();
                List<string> names = new List<string>();
                for (int j = 0; j < list.Count; j++)
                {
                    string code = list[j][0];
                    while (code.Length < 6)
                    {
                        code = "0" + code;
                    }
                    string name = list[j][1];
                    ids.Add(code);
                    names.Add(name);
                }
                m.FundIds = ids;
                m.FundNames = names;
                m.Name = txDataTable.Rows[i][1].ToString();
                m.Time = txDataTable.Rows[i][2].ToString();
                m.FundCount = txDataTable.Rows[i][3].ToString();
                m.Insititution = txDataTable.Rows[i][4].ToString();
                string oldC = txDataTable.Rows[i][5].ToString();
                string newC = string.IsNullOrWhiteSpace(oldC) ? "" : (double.Parse(oldC) / 100000000).ToString("0.00") + "亿元";
                m.Capital = newC;
                m.Rate = txDataTable.Rows[i][6].ToString();
                mstx.Add(m);
                Console.WriteLine(i);
            }
            DataTable txt = LToTXTable(mstx);
            Utils.ExportToXls("天相原始数据.xls", txt);

            //数据核查
            List<Manager> txonly = new List<Manager>();
            List<Manager> ttonly = new List<Manager>();

            List<Manager> byzs = new List<Manager>();


            foreach (var item in mstt)
            {
                if (!mstx.Exists(m => m.Name == item.Name && (m.Insititution.Contains(item.Insititution) || item.Insititution.Contains(m.Insititution))))
                    ttonly.Add(item);
            }

            foreach (var item in mstx)
            {
                if (!mstt.Exists(m => m.Name == item.Name && (m.Insititution.Contains(item.Insititution) || item.Insititution.Contains(m.Insititution))))
                {
                    txonly.Add(item);
                }
                else
                {
                    var mm = mstt.Find(m => m.Name == item.Name && (m.Insititution.Contains(item.Insititution) || item.Insititution.Contains(m.Insititution)));
                    bool yz = true;
                    item.BYZ = "";
                    item.CTYZ = "是";

                    if (mm.FundCount.Trim() != item.FundCount.Trim())
                    {
                        yz = false;
                        item.CTYZ = string.Format("天天数量:{0}  ", mm.FundCount);
                    }
                    var idxtt = mm.FundIds.Except(item.FundIds);
                    var idxtx = item.FundIds.Except(mm.FundIds);
                    var namett = mm.FundNames.Except(item.FundNames);
                    var nametx = item.FundNames.Except(mm.FundNames);
                    if (idxtt.Count() > 0)
                    {
                        yz = false;
                        item.BYZ += "天天多:";
                        foreach (string s in idxtt)
                        {
                            item.BYZ += s + " ";
                        }

                    }
                    if (idxtx.Count() > 0)
                    {
                        yz = false;
                        item.BYZ += "天相多:";
                        foreach (string s in idxtx)
                        {
                            item.BYZ += s + " ";
                        }
                    }
                    if (!yz)
                    {
                        byzs.Add(item);
                    }
                }
            }

            MySqlHelper.CloseCon();
            DataTable byzTb = LTT(byzs);
            DataTable onlyTx = LToTable(txonly);
            DataTable onlyTt = LToTable(ttonly);

            Utils.ExportToXls("只有天相有.xls", onlyTx);
            Utils.ExportToXls("只有天天有.xls", onlyTt);
            Utils.ExportToXls("不一致.xls", byzTb);
        }

        DataTable LTT(List<Manager> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("天相ID");
            dt.Columns.Add("姓名");
            dt.Columns.Add("所属公司");
            dt.Columns.Add("现任基金Id");
            dt.Columns.Add("现任基金数量");
            dt.Columns.Add("不一致项");
            dt.Columns.Add("基金数量是否一致");
            foreach (var item in list)
            {
                dt.Rows.Add(item.TXID, item.Name, item.Insititution, LToString(item.FundIds), item.FundCount, item.BYZ, item.CTYZ);
            }
            return dt;
        }

        DataTable LToTXTable(List<Manager> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TXID");
            dt.Columns.Add("姓名");
            dt.Columns.Add("所属公司");
            dt.Columns.Add("现任基金Id");
            dt.Columns.Add("现任基金名称");
            dt.Columns.Add("现任基金数量");
            dt.Columns.Add("累计从业时间");
            dt.Columns.Add("现任基金资产总规模");
            dt.Columns.Add("现任基金最佳回报");
            foreach (var item in list)
            {
                dt.Rows.Add(item.TXID, item.Name, item.Insititution, LToString(item.FundIds), LToString(item.FundNames), item.FundCount, item.Time, item.Capital, item.Rate);
            }
            return dt;
        }

        DataTable LToTable(List<Manager> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("姓名");
            dt.Columns.Add("所属公司");
            dt.Columns.Add("现任基金Id");
            dt.Columns.Add("现任基金名称");
            dt.Columns.Add("现任基金数量");
            dt.Columns.Add("累计从业时间");
            dt.Columns.Add("现任基金资产总规模");
            dt.Columns.Add("现任基金最佳回报");
            foreach (var item in list)
            {
                dt.Rows.Add(item.Name, item.Insititution, LToString(item.FundIds), LToString(item.FundNames), item.FundCount, item.Time, item.Capital, item.Rate);
            }
            return dt;
        }
        class Manager
        {
            public string Name { get; set; }
            public string Insititution { get; set; }
            public List<string> FundIds { get; set; }
            public List<string> FundNames { get; set; }
            public string FundCount { get; set; }
            public string Time { get; set; }
            public string Days { get; set; }
            public string Capital { get; set; }
            public string Rate { get; set; }
            public string TXID { get; set; }

            public string BYZ { get; set; }
            public string CTYZ { get; set; }

        }

        string LToString(List<string> list)
        {
            string x = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (i != list.Count - 1)
                {
                    x += list[i] + ",";
                }
                else
                {
                    x += list[i];
                }
            }
            return x;
        }
        string sql1 = @"SELECT a.f_manager_id,a.f_manager_name,a.f_manage_day,f_manage_num,a.f_institution_name,b.f_capital,c.rate FROM txnfresearch.V_Manager_Info a 
LEFT JOIN txnfresearch.t_fund_manager_capital b on a.f_manager_id = b.f_manager_id
LEFT JOIN (
SELECT f_manager_id,F_END_DATE,MAX(f_yield_rate) rate from txnfresearch.T_Manager_Perform 
WHERE F_END_DATE = 29991231
GROUP BY f_manager_id,F_END_DATE 
) c on a.f_manager_id = c.f_manager_id";

        string sql2 = @"select t3.f_fund_code,t3.f_fund_name
-- t3.* 
from txnfdb.T_FUND_MANAGER as t1
inner join txnfdb.T_FUND as t2 on t2.F_FUND_ID = t1.F_FUND_ID
inner join txnfdb.T_FUND as t3 on IFNULL(t2.F_FUND_PID, t2.F_FUND_ID) = IFNULL(t3.F_FUND_PID, t3.F_FUND_ID)
inner join (
-- 判断代码的个数，超过1的去掉母基金
 select F_Fund_Code, COUNT(F_Fund_Code) as F_Code_Count
 from txnfdb.T_FUND group by F_Fund_Code
) as t4 on t4.F_Fund_Code = t3.F_Fund_Code and ((t4.F_Code_Count = 1) or (t4.F_Code_Count > 1 and t3.F_CLASS_ID not in (20,30)))
where t1.F1 = {0} and LEAST(t1.f_end_date,t3.f56)>20170410 "; //FID29991231
    }

    }

   

