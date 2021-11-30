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
        public bool isAnime = true;

        public Rod prev;

        ControlCollection control;
        Rod[] rodsFromDisk = new Rod[3];


        public Disk(ref ControlCollection control,ref Rod[] rods)
        {
            this.control = control;
            this.rodsFromDisk = rods;
            this.prev = rods[0];
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

            rodsFromDisk[0].disksOnRod.Push(disk);
        }

        Point current;
        private void DiskMouseDown(object sender, MouseEventArgs e)
        {
            if (prev.disksOnRod.Peek().Size == disk.Size)
            {

                if (e.Button == MouseButtons.Left)
                {
                    current = new Point(e.X, e.Y);
                }
            }
            else
            {
                isAnime = isAnime && false;
                //MessageBox.Show("Нельзя");
            }
        }

        public void DiskMouseMove(object sender, MouseEventArgs e)
        {

            if (prev.disksOnRod.Peek().Size == disk.Size)
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
            else
                isAnime = isAnime && false;
        }

        //Бесконечно ставиться вверх done
        //Нужно удалять из листа из которого убрал диск done
        //На начальном штыре нет кол-ва дисков done 
        //Можно брать только самый маленький done
        //Добавить проверку на постановку диска на штырь с которого был взят диск done
        //Маленький можно ставить на пустой штырь или на больший диск done
        private void DiskMouseUp(object sender, MouseEventArgs e)
        {
            int delta = 50;
            Panel disk = sender as Panel;
            
            for (int i = 0; i < 3; i++)
            {
                if (prev.disksOnRod.Peek().Size == disk.Size) 
                {
                    if (disk.Location.X <= rodsFromDisk[i].rod.Location.X + delta && disk.Location.X >= rodsFromDisk[i].rod.Location.X - delta)
                    {
                        if (rodsFromDisk[i].disksOnRod.Count == 0 || rodsFromDisk[i].disksOnRod.Peek().Width > disk.Width)
                        {
                            if (rodsFromDisk[i].disksOnRod.Count == 0)
                            {
                                prev.disksOnRod.Peek().Location = new Point(rodsFromDisk[i].rod.Location.X - disk.Width / 2 + 3, rodsFromDisk[i].rod.Location.Y + rodsFromDisk[i].rod.Height - disk.Height);
                                prev.disksOnRod.Pop();
                                rodsFromDisk[i].disksOnRod.Push(disk);
                                this.prev = rodsFromDisk[i];
                            }

                            else
                            {
                                prev.disksOnRod.Peek().Location = new Point(rodsFromDisk[i].rod.Location.X - disk.Width / 2 + 3, rodsFromDisk[i].rod.Location.Y + rodsFromDisk[i].rod.Height - disk.Height * (rodsFromDisk[i].disksOnRod.Count + 1));
                                prev.disksOnRod.Pop();
                                rodsFromDisk[i].disksOnRod.Push(disk);
                                this.prev = rodsFromDisk[i];
                            }
                            MessageBox.Show(disk.Location.ToString() + "\n" + rodsFromDisk[i].disksOnRod.Count + '\n' + rodsFromDisk[i].rod.Location);
                        }
                        else
                        {
                            prev.disksOnRod.Peek().Location = new Point(prev.rod.Location.X - disk.Width / 2 + 3, prev.rod.Location.Y + rodsFromDisk[i].rod.Height - disk.Height*(prev.disksOnRod.Count));
                           // MessageBox.Show("Нельзя");
                        }
                    } 
                }
                else
                {
                    //MessageBox.Show("Нельзя");
                }
            }
        }

        public void MoveDisk(bool isAnime)
        {
            if (isAnime)
            {
                disk.MouseClick += new MouseEventHandler(DiskMouseDown);
                disk.MouseMove += new MouseEventHandler(DiskMouseMove);
                disk.MouseUp += new MouseEventHandler(DiskMouseUp);
            }
            else
                MessageBox.Show("Нельзя");
        }
    }
}
