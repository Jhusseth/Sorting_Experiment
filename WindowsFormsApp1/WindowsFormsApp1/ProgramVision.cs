using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.modelo;

namespace WindowsFormsApp1
{
    public partial class ProgramVision : Form
    {

        private PrincipalProcess principal;





        public ProgramVision()
        {
            InitializeComponent();
            principal = new PrincipalProcess();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void Button1_Click(object sender, EventArgs e)
        {
            principal.generateData();
        }


        public void Button2_Click(object sender, EventArgs e)
        {
            principal.experimentEvent();


        }
    }
       
}
