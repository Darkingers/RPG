using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RPG
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Width = 1200;
            Height = 800;
            GameTimer.Start(1);
            Database.Load();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
           
            base.OnPaint(e);
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
               
            }
            Invalidate();
        }
  
        
        private void Form1_Zoom(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            
        }
    }
   
}
