using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiplayerSnakeGame
{
    public partial class Form1 : Form
    {
        #region diceGlobals
        int DiceCurrVal = 0;
        Random r = new Random();
        #endregion 



        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// this method get a random dice number and renders its relative image on the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DiceCurrVal = r.Next(1,6);

            var obj = (Bitmap)Properties.Resources.ResourceManager.GetObject(string.Format("dice{0}", DiceCurrVal));
            ((Button)sender).BackgroundImage = obj;
        }




    }
}
