using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace LiLa
{
    public partial class Form2 : Form
    {
        private WebClient WC = new WebClient();
        /// <summary>
        /// Base of links
        /// </summary>
        public string[,] Cell = new string[100, 2];
        public int tempIndex;
        private string UpdLink = "http://geozemly.ru/DataBase";
        private int[] cellNum = new int[6];
        public string[] defCellName = new string[6] { "Facebook", "ВКонтакте", "Windows Live", "Twitter", "Одноклассники", "Мой Мир" };
        public string[] defCellLink = new string[6] { "facebook.com", "vk.com", "live.com", "twitter.com", "odnoklassniki.ru", "my.mail.ru" };

        public void AddNewItem(string name, string link, bool EditMode, int ind)
        {
            int i = 0;
            while (Cell[i, 0] != null)
                i++;
            if (EditMode)
                i = ind;
            Cell[i, 0] = name;
            Cell[i, 1] = link;
            BuildListBox();
        }

        public void updateBase()
        {
            StreamReader SR = new StreamReader("DBase");
            int i = 0;
            string str = SR.ReadLine();
            if (str == "[#index]")
            {
                str = SR.ReadLine();
                while (!SR.EndOfStream && str != "[#cell]")
                {
                    Cell[i, 0] = str;
                    str = SR.ReadLine();
                    i++;
                }
            }
            if (i < 6)
                while (i != 6)
                {
                    Cell[i, 0] = defCellName[i];
                    i++;
                }

            i = 0;
            if (str == "[#cell]")
            {
                str = SR.ReadLine();
                while (!SR.EndOfStream && str != "[#source]" && i != 6)
                {
                    cellNum[i] = Convert.ToInt32(str);
                    str = SR.ReadLine();
                    i++;
                }
            }
            if (i < 6)
                while (i != 6)
                {
                    cellNum[i] = i;
                    i++;
                }

            i = 0;
            if (str == "[#source]")
            {
                while (!SR.EndOfStream)
                {
                    str = SR.ReadLine();
                    Cell[i, 1] = str;
                    i++;
                }
            }
            if (i < 6)
                while (i != 6)
                {
                    Cell[i, 1] = defCellLink[i];
                    i++;
                }
            SR.Close();
        }

        private void changeCell(int numb)
        {
            cellNum[numb] = listBox1.SelectedIndex;
            setCell();
        }

        private void setCell()
        {
            radioButton1.Text = Cell[cellNum[0], 0];
            radioButton1.Font = new Font("Segoe Print", 22 - Cell[cellNum[0], 0].Length);
            radioButton2.Text = Cell[cellNum[1], 0];
            radioButton2.Font = new Font("Segoe Print", 22 - Cell[cellNum[1], 0].Length);
            radioButton3.Text = Cell[cellNum[2], 0];
            radioButton3.Font = new Font("Segoe Print", 22 - Cell[cellNum[2], 0].Length);
            radioButton4.Text = Cell[cellNum[3], 0];
            radioButton4.Font = new Font("Segoe Print", 22 - Cell[cellNum[3], 0].Length);
            radioButton5.Text = Cell[cellNum[4], 0];
            radioButton5.Font = new Font("Segoe Print", 22 - Cell[cellNum[4], 0].Length);
            radioButton6.Text = Cell[cellNum[5], 0];
            radioButton6.Font = new Font("Segoe Print", 22 - Cell[cellNum[5], 0].Length);
        }

        public Form2()
        {
            InitializeComponent();
            FileStream fCh = new FileStream("DBase", FileMode.OpenOrCreate);
            fCh.Close();
            updateBase();
        }

        private void BuildListBox()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < 100; i++)
            {
                if (Cell[i, 0] == null) break;
                listBox1.Items.Add(Cell[i, 0]);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            BuildListBox();
            listBox1.SelectedIndex = 0;
            setCell();
        }
        // Save Button
        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("DBase");
            sw.WriteLine("[#index]");
            for (int i = 0; i < 100; i++)
            {
                if (Cell[i, 0] == null) break;
                sw.WriteLine(Cell[i, 0]);
            }
            sw.WriteLine("[#cell]");
            for (int i = 0; i < 6; i++)
                sw.WriteLine(cellNum[i]);
            sw.WriteLine("[#source]");
            for (int i = 0; i < 100; i++)
            {
                if (Cell[i, 1] == null) break;
                sw.WriteLine(Cell[i, 1]);
            }
            sw.Close();
            this.Hide();
        }

        public void SetCell()
        {
            if (radioButton1.Checked)
            {
                changeCell(0);
                radioButton1.Checked = false;
                radioButton2.Checked = true;
            }
            else if (radioButton2.Checked)
            {
                changeCell(1);
                radioButton2.Checked = false;
                radioButton3.Checked = true;

            }
            else if (radioButton3.Checked)
            {
                changeCell(2);
                radioButton3.Checked = false;
                radioButton4.Checked = true;

            }
            else if (radioButton4.Checked)
            {
                changeCell(3);
                radioButton4.Checked = false;
                radioButton5.Checked = true;

            }
            else if (radioButton5.Checked)
            {
                changeCell(4);
                radioButton5.Checked = false;
                radioButton6.Checked = true;

            }
            else if (radioButton6.Checked)
            {
                changeCell(5);
                radioButton6.Checked = false;
                radioButton1.Checked = true;
            }
            try
            {
                listBox1.SelectedIndex += 1;
            }
            catch
            {
                listBox1.SelectedIndex = 0;
            }
        }
        // Set Button
        private void button1_Click(object sender, EventArgs e)
        {
            SetCell();
        }
        // Update Button
        private void button4_Click(object sender, EventArgs e)
        {
            FileInfo fs = new FileInfo("DataBase");
            try
            {
                WC.DownloadFile(UpdLink, "DataBase");
            }
            catch
            {
                MessageBox.Show("Error with connecting to update center.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (fs.Exists)
            {
                StreamReader SR = new StreamReader("DataBase");
                int i, j, len, n = 0, jump = 1;
                int[] pos = new int[Cell.Length / 2];
                for (i = 0; i < Cell.Length / 2; i++)
                    if (Cell[i, 0] == null) break;
                pos[0] = i;
                j = i;
                string str = SR.ReadLine();
                bool check = true;
                if (str == "[#index]")
                {
                    str = SR.ReadLine();
                    while (!SR.EndOfStream && str != "[#source]")
                    {
                        for (int k = 0; k < i; k++)
                            if (Cell[k, 0].ToLower() == str.ToLower())
                            {
                                check = false;
                                break;
                            }
                        if (check)
                        {
                            Cell[i, 0] = str;
                            i++;
                            pos[n] = jump;
                            n++;
                        }
                        str = SR.ReadLine();
                        check = true;
                        jump++;
                    }
                }
                i = j;
                len = jump - 1;
                jump = 0;
                n = 0;
                for (int count = 0; count < len; count++)
                {
                    str = SR.ReadLine();
                    jump++;
                    if (jump == pos[n])
                    {
                        Cell[i, 1] = str;
                        i++;
                        n++;
                    }
                }
                SR.Close();
            }
            BuildListBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
            this.Hide();
        }
        // Edit Button
        private void button5_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
            this.Hide();
            frm3.textBox1.Text = Cell[listBox1.SelectedIndex, 0];
            frm3.textBox2.Text = Cell[listBox1.SelectedIndex, 1];
            frm3.label5.Text = "E";
            frm3.tempIndex = listBox1.SelectedIndex;
        }
        // Delete Button
        private void button6_Click(object sender, EventArgs e)
        {
            int i = 0;
            while (Cell[i, 0] != null)
                i++;
            i--;
            Cell[listBox1.SelectedIndex, 0] = Cell[i, 0];
            Cell[listBox1.SelectedIndex, 1] = Cell[i, 1];
            Cell[i, 0] = null;
            Cell[i, 1] = null;
            BuildListBox();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SetCell();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = "http://" + Cell[listBox1.SelectedIndex, 1];
        }
    }
}
