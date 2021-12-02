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
    class Background
    {
        public ControlCollection control;
        public static ComboBox sumOfDisk;
        public static Button apply;
        public static Button solve;
        public static Button restart;
        public static bool restartClicked = false;
        public static bool solveClicked = false;
        public Background(ref ControlCollection control)
        {
            this.control = control;
        }

        public void AddTable()
        {
            Panel table = new Panel();
            table.Size = new Size(650, 10);
            table.Location = new Point(100, 350);
            table.BackColor = Color.Black;
            this.control.Add(table);
        }

        public void AddTextBox()
        {
            Label text = new Label();
            //TextBox text = new TextBox();
            sumOfDisk = new ComboBox();
            text.Location = new Point(10, 10);
            text.Text = "Введите количество дисков:";
            text.Size = new Size(200, 15);
            text.Font = new Font("Arial", 10);

            int[] tmp = new int[6] { 3, 4, 5, 6, 7, 8 };
            for (int i = 0; i < 6; i++)
            {
                sumOfDisk.Items.Add(tmp[i]);
            }

            //sumOfDisk.SelectedItem = 1;
            sumOfDisk.TabStop = false;


            sumOfDisk.Location = new Point(210, 10);
            sumOfDisk.Size = new Size(45,23);
            this.control.Add(text);
            this.control.Add(sumOfDisk);
        }

        public void AddButton()
        {
            apply = new Button();
            apply.Location = new Point(260, 8);
            apply.Size = new Size(70, 23);
            apply.Text = "Применить";

            solve = new Button();
            solve.Location = new Point(380,450);
            solve.Size = new Size(120, 50);
            solve.Text = "Решить";

            restart = new Button();
            restart.Location = new Point(30, 30);
            restart.Size = new Size(150, 50);
            restart.Text = "Рестарт";

            this.control.Add(solve);
            this.control.Add(apply);
            this.control.Add(restart);


            solve.Click += new EventHandler(Solve);
        }

        public static int GetSumOfDisk()
        {
            //MessageBox.Show(sumOfDisk.Text);
            return Convert.ToInt32(sumOfDisk.SelectedItem);
        }

        public void Solve(object sender, EventArgs e)
        {
            Disk.Solver(GetSumOfDisk(), 0, 2, 1);
            solve.Enabled = false;
            MessageBox.Show("Игра закончена");
           
        }
    }
}
