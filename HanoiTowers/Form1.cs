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

        public Button tmp = new Button();

        static ControlCollection control;
        /// <summary>
        /// Массив стержней для возможности их индексации и обращения к конкретному объекту
        /// </summary>
        Rod[] rods = new Rod[3];

        List<Disk> disks = new List<Disk>();
        Disk disk;
        Background background;

        public Form1()
        {
            InitializeComponent();
            control = (ControlCollection)this.Controls;
            CreateMap();
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
                rods[i] = new Rod(ref control);
                rods[i].Add(i);
            }

            background = new Background(ref control);
            background.AddTable();
            background.AddTextBox();
            background.AddButton();
            Background.apply.Click += new EventHandler(AddDisks);

        }

        public void AddDisks(object sender, EventArgs e)
        {
            
            int size = Convert.ToInt32(Background.sumOfDisk.SelectedItem);
            for (int i = 0; i < size; i++)
            {
                disks.Add(disk = new Disk(ref control,ref rods));
            }

            for (int i = 0; i < size; i++)
            {
                disks[i].AddDisk(i);
                disks[i].MoveDisk(disks[i].isAnime);
            }
            if (size > 0)
            {
                Background.apply.Enabled = false;
                Background.apply.BackColor = Color.White;
                Background.sumOfDisk.Enabled = false;
            }
            else
                MessageBox.Show("Выберите кол-во дисков");

        }

         
    }
}
