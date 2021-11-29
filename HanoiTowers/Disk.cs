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
        Rod[] rodsFromDisk = new Rod[3];


        public Disk(ref ControlCollection control,ref Rod[] rods)
        {
            this.control = control;
            this.rodsFromDisk = rods;
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
        //Бесконечно ставиться вверх 
        //Нужно удалять из листа из которого убрал диск
        //Добавить проверку на постановку диска на штырь с которого блы взять диск
        //На начальном штыре нет кол-ва дисков
        private void DiskMouseUp(object sender, MouseEventArgs e)
        {
            int delta = 50;
            Panel disk = sender as Panel;
            for (int i = 0; i < 3; i++)
            {
                if (disk.Location.X <= rodsFromDisk[i].rod.Location.X + delta && disk.Location.X >= rodsFromDisk[i].rod.Location.X - delta)
                {
                    if (rodsFromDisk[i].disksOnRod.Count == 0)
                    {
                        disk.Location = new Point(rodsFromDisk[i].rod.Location.X - disk.Width / 2 + 3, rodsFromDisk[i].rod.Location.Y + rodsFromDisk[i].rod.Height - disk.Height);
                        rodsFromDisk[i].disksOnRod.Add(disk);
                    }

                    else
                    {
                        disk.Location = new Point(rodsFromDisk[i].rod.Location.X - disk.Width / 2 + 3, rodsFromDisk[i].rod.Location.Y + rodsFromDisk[i].rod.Height - disk.Height* (rodsFromDisk[i].disksOnRod.Count+1));
                        rodsFromDisk[i].disksOnRod.Add(disk);
                    }
                    MessageBox.Show(disk.Location.ToString()+"\n" + rodsFromDisk[i].disksOnRod.Count + '\n' + rodsFromDisk[i].rod.Location);
                }
            }
        }

        public void MoveDisk()
        {
            disk.MouseClick += new MouseEventHandler(DiskMouseDown);
            disk.MouseMove += new MouseEventHandler(DiskMouseMove);
            disk.MouseUp += new MouseEventHandler(DiskMouseUp);
        }
    }
}
