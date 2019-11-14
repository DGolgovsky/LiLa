using System;
using System.IO;
using System.Windows.Forms;

namespace LiLa
{
    public partial class Form1 : Form
    {
        private Form2 frm2 = new Form2();
        private int[] CellNum = new int[6];

        public void getCellNum()
        {
            StreamReader SR = new StreamReader("DBase");
            int i = 0;
            string str = "";
            while (!SR.EndOfStream && str != "[#cell]")
                str = SR.ReadLine();
            while (!SR.EndOfStream && i != 6)
            {
                CellNum[i] = Convert.ToInt32(SR.ReadLine());
                i++;
            }

            while (i != 6)
            {
                CellNum[i] = i;
                i++;
            }

            SR.Close();
            frm2.updateBase();
            linkLabel1.Text = frm2.Cell[CellNum[0], 0];
            linkLabel1.Font = new System.Drawing.Font("Segoe Print", 27 - linkLabel1.Text.Length);
            linkLabel2.Text = frm2.Cell[CellNum[1], 0];
            linkLabel2.Font = new System.Drawing.Font("Segoe Print", 27 - linkLabel2.Text.Length);
            linkLabel3.Text = frm2.Cell[CellNum[2], 0];
            linkLabel3.Font = new System.Drawing.Font("Segoe Print", 27 - linkLabel3.Text.Length);
            linkLabel4.Text = frm2.Cell[CellNum[3], 0];
            linkLabel4.Font = new System.Drawing.Font("Segoe Print", 27 - linkLabel4.Text.Length);
            linkLabel5.Text = frm2.Cell[CellNum[4], 0];
            linkLabel5.Font = new System.Drawing.Font("Segoe Print", 27 - linkLabel5.Text.Length);
            linkLabel6.Text = frm2.Cell[CellNum[5], 0];
            linkLabel6.Font = new System.Drawing.Font("Segoe Print", 27 - linkLabel6.Text.Length);
        }

        public Form1()
        {
            InitializeComponent();
            getCellNum();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.Links[0].LinkData = "http://" + frm2.Cell[CellNum[0], 1];
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel2.Links[0].LinkData = "http://" + frm2.Cell[CellNum[1], 1];
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel3.Links[0].LinkData = "http://" + frm2.Cell[CellNum[2], 1];
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel4.Links[0].LinkData = "http://" + frm2.Cell[CellNum[3], 1];
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel5.Links[0].LinkData = "http://" + frm2.Cell[CellNum[4], 1];
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel6.Links[0].LinkData = "http://" + frm2.Cell[CellNum[5], 1];
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getCellNum();
        }
    }
}
