using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeviceManager.Model;
using System.IO;

namespace DeviceManager.CustomControl
{
    public partial class CustomAlarmSetControl : UserControl
    {     
        public CustomAlarmSetControl()
        {
            InitializeComponent();
        }


        public AlarmConfig AlarmConfig { get; set; }

        public CustomAlarmSetControl(string name,AlarmConfig ac)
        {
            InitializeComponent();
            groupBox1.Text = name;
            AlarmConfig = ac;
            AlarmConfig.Name = name;
            Init();
        }

        public CustomAlarmSetControl(AlarmConfig ac)
        {
            InitializeComponent();
            groupBox1.Text = ac.Name;
            AlarmConfig = ac;
            Init();
        }       

        private void Init()
        {
            listBox1.DoubleClick += ListBox1_DoubleClick;
            foreach (var item in ConfigData.SensorModelRoot.SensorModels)
            {
                if (AlarmConfig.AlarmField.Exists(af => af.Model == item.Name))
                {
                    List<AlarmField> afList = AlarmConfig.AlarmField.FindAll(af=>af.Model == item.Name);
                    foreach (var af in afList)
                    {
                        SetControlGroup scg = new SetControlGroup(af);
                        AddControls(scg);
                    }
                }
                else
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void ListBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                SensorModel smodel = (SensorModel)listBox1.SelectedItem;
                AddModel(smodel);
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        List<SetControlGroup> scgList = new List<SetControlGroup>();
        void AddModel(SensorModel sm)
        {
            foreach (Field fd in sm.Fields)
            {
                SetControlGroup scg = new SetControlGroup(fd);
                AddControls(scg);
            }
        }

        void AddControls(SetControlGroup scg)
        {
            tableLayoutPanel1.Controls.Add(scg.Model);
            tableLayoutPanel1.Controls.Add(scg.Name);
            tableLayoutPanel1.Controls.Add(scg.UpLb);
            tableLayoutPanel1.Controls.Add(scg.Up);
            tableLayoutPanel1.Controls.Add(scg.LowLb);
            tableLayoutPanel1.Controls.Add(scg.Low);
            tableLayoutPanel1.Controls.Add(scg.AroundLb);
            tableLayoutPanel1.Controls.Add(scg.Around);
        }
    }

    class SetControlGroup
    {
        string uptext = "上限值";
        string lowtext = "下限值";
        string aroundtext = "允许误差(+-)";
        public SetControlGroup(Field fd)
        {
            Field = fd;
            Model = new Label();
            Name = new Label();
            UpLb = new Label();
            LowLb = new Label();
            AroundLb = new Label();
            Up = new TextBox();
            Low = new TextBox();
            Around = new TextBox();
            Model.Text = fd.CurrentSensor.Model.Title;
            Name.Text = fd.Alias;
            UpLb.Text = uptext;
            LowLb.Text = lowtext;
            AroundLb.Text = aroundtext;
        }
        public SetControlGroup(AlarmField fd)
        {
            AField = fd;
            Model = new Label();
            Name = new Label();
            UpLb = new Label();
            LowLb = new Label();
            AroundLb = new Label();
            Up = new TextBox();
            Low = new TextBox();
            Around = new TextBox();
            Up.Text = fd.Up.ToString();
            Around.Text = fd.Around.ToString();
            Low.Text = fd.Low.ToString();
            Model.Text = fd.Model;
            Name.Text = fd.Name;
            UpLb.Text = uptext;
            LowLb.Text = lowtext;
            AroundLb.Text = aroundtext;
        }

        public AlarmField AField { get; set; }
        public Field Field { get; set; }
        public Label Model { get; set; }
        public Label Name { get; set; }
        public Label UpLb { get; set; }
        public Label LowLb { get; set; }
        public Label AroundLb { get; set; }
        public TextBox Up { get; set; }
        public TextBox Low { get; set; }
        public TextBox Around { get; set; }
    }
}
