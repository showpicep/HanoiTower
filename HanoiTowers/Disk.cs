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
        static Rod[] rodsFromDisk = new Rod[3];


        public Disk(ref ControlCollection control,ref Rod[] rods)
        {
            this.control = control;
            Disk.rodsFromDisk = rods;
            this.prev = rods[0];
        }

        public void AddDisk(int i)
        {
            Random random = new Random(0);
            disk = new Panel();
            
            disk.Location = new Point(130+(i*10), 350 - ((i+1)*sizeOfDisk));
            disk.BorderStyle = BorderStyle.FixedSingle;
            disk.Size = new Size(initialWidth - (i*20), sizeOfDisk);
            disk.BackColor = Color.FromArgb(200-i*15,255-i*20,255-i*25);
            this.control.Add(disk);

            rodsFromDisk[0].disksOnRod.Push(disk);
            Background.restartClicked = false;
        }

        Point current;
        private void DiskMouseDown(object sender, MouseEventArgs e)
        {
            if (Background.solve.Enabled != false)
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
                }
            }
        }

        public void DiskMouseMove(object sender, MouseEventArgs e)
        {
            if (Background.solve.Enabled != false /*&& Background.restartClicked == false*/)
            {
                if (prev.disksOnRod.Peek().Size == disk.Size)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        Point newlocation = disk.Location;
                        newlocation.X += e.X - current.X;
                        newlocation.Y += e.Y - current.Y;
                        disk.Location = newlocation;
                    }
                }
                else
                    isAnime = isAnime && false;
            }
        }

        private void DiskMouseUp(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(rodsFromDisk[2].disksOnRod.Count().ToString());
            int delta = 70;
            Panel disk = sender as Panel;
            if (Background.solve.Enabled != false)
            {
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
                                //MessageBox.Show(disk.Location.ToString() + "\n" + rodsFromDisk[i].disksOnRod.Count + '\n' + rodsFromDisk[i].rod.Location);
                            }
                            else
                            {
                                prev.disksOnRod.Peek().Location = new Point(prev.rod.Location.X - disk.Width / 2 + 3, prev.rod.Location.Y + rodsFromDisk[i].rod.Height - disk.Height * (prev.disksOnRod.Count));
                            }
                        }
                    }
                }
            }
        }

        public void MoveDisk(bool isAnime)
        {
            if (!(rodsFromDisk[1].disksOnRod.Count() == Background.GetSumOfDisk() || rodsFromDisk[2].disksOnRod.Count() == Background.GetSumOfDisk()))
            {
                if (isAnime || Background.restartClicked == false)
                {
                    disk.MouseClick += new MouseEventHandler(DiskMouseDown);
                    disk.MouseMove += new MouseEventHandler(DiskMouseMove);
                    disk.MouseUp += new MouseEventHandler(DiskMouseUp);

                }
                else
                    MessageBox.Show("Нельзя_1");
            }
            else
                MessageBox.Show("Игра закончена");
        }

        public static void Solver(int num_disc, int start, int end, int temp)
        {
            if (Background.apply.Enabled == false)
            {
                if (!gameOver(Background.GetSumOfDisk()))
                {
                    if (num_disc > 1)
                        Solver(num_disc - 1, start, temp, end);


                    Animation.moveUp(rodsFromDisk[start].disksOnRod.Peek(), 50);

                    if (rodsFromDisk[start].disksOnRod.Peek().Location.X < rodsFromDisk[end].rod.Location.X)
                        Animation.moveRight(rodsFromDisk[start].disksOnRod.Peek(), rodsFromDisk[end].rod.Location.X - (rodsFromDisk[start].disksOnRod.Peek().Width / 2) + 3); //+3
                    else
                        Animation.moveLeft(rodsFromDisk[start].disksOnRod.Peek(), rodsFromDisk[end].rod.Location.X - (rodsFromDisk[start].disksOnRod.Peek().Width / 2) + 6); // +3

                    Animation.moveDown(rodsFromDisk[start].disksOnRod.Peek(), 350 - (rodsFromDisk[end].disksOnRod.Count + 1) * 21);


                    rodsFromDisk[end].disksOnRod.Push(rodsFromDisk[start].disksOnRod.Pop());

                    if (num_disc > 1)
                        Solver(num_disc - 1, temp, end, start);

                }
                else
                    MessageBox.Show("Верните начальное положение дисков");
            }
            else
                MessageBox.Show("Задайте кол-во дисков!");
        }

        public static bool gameOver(int num_disc)
        {
            if (rodsFromDisk[1].disksOnRod.Count() == num_disc || rodsFromDisk[2].disksOnRod.Count() == num_disc)
            {
                MessageBox.Show("Игра закончена");
                return true;
            }
            else
            {
               MessageBox.Show(rodsFromDisk[1].disksOnRod.Count().ToString());
               return false;
            }
        }

        
    }
}
