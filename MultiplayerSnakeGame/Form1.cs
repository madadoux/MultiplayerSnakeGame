using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace MultiplayerSnakeGame
{

    public partial class Form1 : Form
    {
        #region diceGlobals
        int DiceCurrVal = 0;
        Random r = new Random();
        #endregion
        #region matrixinfo
        int numberOfrows = 10;
        int numberOfColumns = 10;
        float cellWidth = 40.0f;
        float cellHeight = 40.0f;
        Point Origin = new Point(25, 25);
        #endregion

        #region playersData
        List<player> OtherPlayers;
        player me;
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
            DiceCurrVal = r.Next(1, 6);

            var obj = (Bitmap)Properties.Resources.ResourceManager.GetObject(string.Format("dice{0}", DiceCurrVal));
            ((Button)sender).BackgroundImage = obj;

            movePlayer(me, DiceCurrVal);
        }



        Pen p = new Pen(Brushes.Black, 7);

        void UpdateGame()
        {
            var g = panel1.CreateGraphics();
            drawMatrix(g, p);
            updateList();
        }


        ListViewItem playerToListItem(player p)
        {
            ListViewItem lvt = new ListViewItem();
            lvt.UseItemStyleForSubItems = false;
            lvt.SubItems[0].BackColor = ((SolidBrush)p.mCol).Color; ;
            lvt.Text = string.Format("{0} , position : {1}", p.name, p.getPositionAsCell(numberOfrows).ToString());
            lvt.ForeColor = ((SolidBrush)p.mCol).Color;

            return lvt;
        }


        //updatePlayerList
        void updateList()
        {

            listBox1.Items.Clear();
            listBox1.Items.Add(playerToListItem(me));
            foreach (player p in OtherPlayers)
            {

                var xxx = playerToListItem(p);
                listBox1.Items.Add(xxx);


            }
        }


        // drawMatrixWithPlayers
        void drawMatrix(Graphics g, Pen p)
        {

            panel1.Refresh();
            for (int i = 0; i < numberOfrows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    g.DrawRectangle(p, Origin.X + (cellWidth * j), Origin.Y + (cellHeight * i), cellWidth, cellHeight);
                }
            }

            var myPos = me.getPositionAsCell(numberOfrows);
            g.FillEllipse(me.mCol, Origin.X + ((int)myPos.Y * cellWidth) + cellHeight / 4, Origin.Y + ((int)myPos.X * cellHeight) + cellWidth / 4, cellWidth / 2, cellHeight / 2);

            foreach (var item in OtherPlayers)
            {
                var oPos = item.getPositionAsCell(numberOfrows);
                g.FillEllipse(item.mCol, Origin.X + ((int)oPos.Y * cellWidth) + cellHeight / 4, Origin.Y + ((int)oPos.X * cellHeight) + cellWidth / 4, cellWidth / 2, cellHeight / 2);
            }

        }


        // configuring
        private void Form1_Load(object sender, EventArgs e)
        {
             listBox1.DrawItem += listBox1_DrawItem;

            me = new player("Mada", Brushes.Green, 1);
            OtherPlayers = new List<player>();
            OtherPlayers.Add(new player("mancy", Brushes.Gray, 5));
            OtherPlayers.Add(new player("mona", Brushes.Red, 15));
            OtherPlayers.Add(new player("alaa", Brushes.Blue, 26));
            OtherPlayers.Add(new player("fadia", Brushes.Violet, 56));


        }


        #region DrawListItemWithCorrespondingColor
        private SolidBrush reportsForegroundBrushSelected = new SolidBrush(Color.White);
        private SolidBrush reportsForegroundBrush = new SolidBrush(Color.Black);
        private SolidBrush reportsBackgroundBrushSelected = new SolidBrush(Color.FromKnownColor(KnownColor.Highlight));
        private SolidBrush reportsBackgroundBrush1 = new SolidBrush(Color.White);
        private SolidBrush reportsBackgroundBrush2 = new SolidBrush(Color.White);
        void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {


            //custom method to draw the items, don't forget to set DrawMode of the ListBox to OwnerDrawFixed

            e.DrawBackground();
            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < listBox1.Items.Count)
            {
                string text = listBox1.Items[index].ToString();
                Graphics g = e.Graphics;

                //background:
                SolidBrush backgroundBrush;
                if (selected)
                    backgroundBrush = reportsBackgroundBrushSelected;
                else
                    backgroundBrush = reportsBackgroundBrush2;
                g.FillRectangle(backgroundBrush, e.Bounds);

                //text: 
                SolidBrush foregroundBrush;
                if (index == 0)
                    foregroundBrush = (SolidBrush)me.mCol; 
                else 
              foregroundBrush     = (selected) ? reportsForegroundBrushSelected :(SolidBrush) OtherPlayers[index-1].mCol;
                g.DrawString(text, e.Font, foregroundBrush, listBox1.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        #endregion
        private void button2_Click(object sender, EventArgs e)
        {
            UpdateGame();

        }



        #region GamePlay

        //return which cell he is there now 
        public void movePlayer(player player, int diceVal)
        {
            if (player.position + diceVal <= 100)
                player.position += diceVal;
            else player.position = 100;

            UpdateGame();

        }

        #endregion

    }

    public class player
    {
        public string name;
        public Brush mCol;
        public int position;

        public player(string _name, Brush c, int pos)
        {
            name = _name;
            mCol = c;
            position = pos;
        }

        // 1 >> 9,0  .. 100 >> 0,9
        public Point getPositionAsCell(int numberOfrows)
        {
            int X;
            if (position % numberOfrows == 0)
                X = (numberOfrows - 1) - ((position - 1) / numberOfrows);
            else
                X = (numberOfrows - 1) - (position / numberOfrows);
            return new Point(X, (position - 1) % numberOfrows);
        }
    }

}
