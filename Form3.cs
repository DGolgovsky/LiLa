using System;
using System.Windows.Forms;

namespace LiLa
{
    public partial class Form3 : Form
    {
        public int tempIndex;
        public Form3()
        {
            InitializeComponent();
            textBox1.Text = null;
            textBox2.Text = null;
            label5.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool check = true;
            if (textBox1.Text.Length < 2)
            {
                MessageBox.Show("Wrong Name. Please retype.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                check = false;
            }
            else if (textBox2.Text.Length < 2 && check)
            {
                MessageBox.Show("Wrong Link path. Please retype.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                check = false;
            }
            if (check)
            {
                bool EditMode = false;
                if (label5.Text == "E")
                {
                    EditMode = true;
                }

                Form2 frm2 = new Form2();
                frm2.AddNewItem(textBox1.Text, textBox2.Text, EditMode, tempIndex);
                frm2.Show();
                Close();
            }
        }
    }
}
