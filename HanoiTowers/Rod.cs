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
    class Rod
    {
        public ControlCollection control;
        public Rod(ref ControlCollection control)
        {
            this.control = control;
        }

        public void Add(int order)
        {
            Panel rod = new Panel();
            int delta = 180+30;
            rod.Size = new Size(10, 200);
            rod.Location = new Point(delta * (order + 1), 150);
            rod.BackColor = Color.Black;
            this.control.Add(rod);
        }

    }
}
