using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HanoiTowers
{
    public partial class Form1 : Form
    {
        private object currObj = null;

        static ControlCollection control;
        /// <summary>
        /// Массив стержней для возможности их индексации и обращения к конкретному объекту
        /// </summary>
        Rod[] rod = new Rod[3];

        List<Disk> disks = new List<Disk>();
        Disk disk;
        Background background;

        public Form1()
        {
            InitializeComponent();
            control = (ControlCollection)this.Controls;
            CreateMap();
            this.MouseMove += new MouseEventHandler(MoveDisk);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        public void CreateMap()
        {
            this.Width = 910;
            this.Height = 600;

            for (int i = 0; i < 3; i++)
            {
                rod[i] = new Rod(ref control);
                rod[i].Add(i);
            }

            background = new Background(ref control);
            background.AddTable();
            background.AddTextBox();
            background.AddButton();
            background.apply.Click += new EventHandler(AddDisks);
        }

        public void AddDisks(object sender, EventArgs e)
        {
            
            int size = Convert.ToInt32(background.sumOfDisk.SelectedItem);
            for (int i = 0; i < size; i++)
            {
                disks.Add(disk = new Disk(ref control));
            }

            for (int i = 0; i < size; i++)
            {
                disks[i].AddDisk(i);
            }

            background.apply.Enabled = false;
            background.apply.BackColor = Color.White;
            background.sumOfDisk.Enabled = false;
        }

        public void MoveDisk(object sender, MouseEventArgs e)
        {
            if (currObj != null)
                currObj.GetType().GetProperty("Location").SetValue(currObj, new Point(Cursor.Position.X, Cursor.Position.Y));
                //disk.disk.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
        }

        public void PanelMouseClick(object sender,EventArgs e)
        {
            currObj = sender;
        }
    }
}
