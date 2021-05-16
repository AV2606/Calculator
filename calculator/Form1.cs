using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class Main : Form
    {       
        bool cleard = true;
        string[] clearBtexts = new string[] { "CLR", "All" };
        Size initialSize;
        public Main()
        {
            InitializeComponent();
            initialSize = this.Size;
            cleard = true;
            ClearB.Text = clearBtexts[1];
            HistoryFLP.ControlAdded += MaxScroll;
            InsertTB.Width = 198;//226 ;
            foreach (Control c in Controls)
            {
                CenterHorizontal(c);
            }
        }
        private void MaxScroll(object sender, EventArgs e)
        {
            HistoryFLP.AutoScrollPosition = new Point(0, HistoryFLP.VerticalScroll.Maximum);
        }

        private void InsertTB_TextChanged(object sender, EventArgs e)
        {
            if(InsertTB.Text.Length>0)
            if (InsertTB.Text[InsertTB.Text.Length - 1] == '=')
            {
                InsertTB.Text = InsertTB.Text.Substring(0, InsertTB.Text.Length - 1);
                EqualsB_Click(sender, e);

            }
        }

        //<onclicks>
        private void MethodL_Click(object sender, EventArgs e)
        {

        }
        private void button0_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "";
            InsertTB.Text += "0";
            cleard = false; ClearB.Text = clearBtexts[0];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "";
            InsertTB.Text += "1";
            cleard = false; ClearB.Text = clearBtexts[0];
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "";
            InsertTB.Text += "2";
            cleard = false; ClearB.Text = clearBtexts[0];
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "";
            InsertTB.Text += "3";
            cleard = false; ClearB.Text = clearBtexts[0];
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "";
            InsertTB.Text += "5";
            cleard = false; ClearB.Text = clearBtexts[0];
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "";
            InsertTB.Text += "5";
            cleard = false; ClearB.Text = clearBtexts[0];
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "";
            InsertTB.Text += "6";
            cleard = false; ClearB.Text = clearBtexts[0];
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "";
            InsertTB.Text += "7";
            cleard = false; ClearB.Text = clearBtexts[0];
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "";
            InsertTB.Text += "8";
            cleard = false; ClearB.Text = clearBtexts[0];
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "";
            InsertTB.Text += "9";
            cleard = false; ClearB.Text = clearBtexts[0];
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "";
            InsertTB.Text += "0.";
            cleard = false; ClearB.Text = clearBtexts[0];
        }
        private void ClearB_Click(object sender, EventArgs e)
        {
            if (cleard)
            {
                HistoryFLP.Controls.Clear();
                cleard = false;
                ClearB.Text = clearBtexts[0];
                return;
            }
            InsertTB.Text = "0.0";
            cleard = true;
            ClearB.Text = clearBtexts[1];
        }
        private void AddB_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "0";
            InsertTB.Text += "+";
            cleard = false; ClearB.Text = clearBtexts[0]; ClearB.Text = clearBtexts[0];
        }
        private void SubB_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "0";
            InsertTB.Text += "-";
            cleard = false; ClearB.Text = clearBtexts[0]; ClearB.Text = clearBtexts[0];
        }
        private void MultiB_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "1";
            InsertTB.Text += "*";
            cleard = false; ClearB.Text = clearBtexts[0]; ClearB.Text = clearBtexts[0];
        }
        private void DivB_Click(object sender, EventArgs e)
        {
            if (cleard)
                InsertTB.Text = "1";
            InsertTB.Text += "/";
            cleard = false; ClearB.Text = clearBtexts[0]; ClearB.Text = clearBtexts[0];
        }
        private void EqualsB_Click(object sender, EventArgs e)
        {
            var a = new Computer();
            HistoryFLP.Controls.Add(GetLabel(InsertTB.Text + "=", HLable.ForeColor, 11));
           // try
            //{
                decimal result = a.Compute(InsertTB.Text);
                InsertTB.Text = result+"";
            //}
            /*
            catch (FormatException fe)
            {
                InsertTB.Text = "Math Error";
            }*/
            HistoryFLP.Controls.Add(GetLabel(InsertTB.Text, Color.Green, 12));
            InsertTB.SelectionStart = InsertTB.Text.Length;
            InsertTB.SelectionLength = 0;
            cleard = false;
            ClearB.Text = clearBtexts[0];
        }
        private Label GetLabel(string text, Color forcolor, int fontsize)
        {
            Label a = new Label();
            a.Font = new Font("Arial", fontsize);
            a.ForeColor = forcolor;
            a.Text = text;
            return a;
        }
        private void InsertTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                EqualsB_Click(sender, e);
        }
        private void InsertTB_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void HistoryFLP_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (this.Size == initialSize)
                return;
            //this.Size = initialSize;
        }
        //</onclicks>

        private void CenterHorizontal(Control control)
        {
            int width = control.Width + 14;
            control.Location = new Point
            {
                X = (control.Parent.Width - width) / 2,
                Y = control.Location.Y
            };
        }

        private void HLable_Click(object sender, EventArgs e)
        {
            foreach (Control c in Controls)
                CenterHorizontal(c);
        }
    }
}
