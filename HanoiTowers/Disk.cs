using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Form;

namespace HanoiTowers
{
    class Disk
    {
        public int sizeOfDisk = 21; // Height
        public int initialWidth = 170;
        public Panel disk;

        ControlCollection control;

        public Disk(ref ControlCollection control)
        {
            this.control = control;
        }

        public void AddDisk(int i)
        {
            Random random = new Random(0);

            disk = new Panel();
            
            disk.Location = new Point(130+(i*10), 329 - (i*sizeOfDisk));
            disk.BorderStyle = BorderStyle.FixedSingle;
            disk.Size = new Size(initialWidth - (i*20), sizeOfDisk);
            disk.BackColor = Color.FromArgb(200-i*15,255-i*20,255-i*25);
            this.control.Add(disk);
        }

        Point current;
        private void DiskMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                current = new Point(e.X, e.Y);
            }
        }

        public void DiskMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //MessageBox.Show("d");
                Point newlocation = disk.Location;
                newlocation.X += e.X - current.X;
                newlocation.Y += e.Y - current.Y;
                disk.Location = newlocation;
            }
        }

        //private void DiskMouseUp(object sender, MouseEventArgs e)
        //{
        //    MessageBox.Show("f");
        //}

        public void MoveDisk()
        {
            disk.MouseClick += new MouseEventHandler(DiskMouseDown);
            disk.MouseMove += new MouseEventHandler(DiskMouseMove);
            //disk.MouseUp += new MouseEventHandler(DiskMouseUp);
        }
    }
}
