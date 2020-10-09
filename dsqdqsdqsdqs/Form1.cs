using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dsqdqsdqsdqs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitPiece();
            Plateau();
            
        }

        private void Plateau()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Plateau pi = ((j + i) % 2 == 0) ? new Plateau("1") : new Plateau("2");
                    pi.Name = (7 - j).ToString() + (i).ToString();
                    pi.BorderStyle = BorderStyle.FixedSingle;
                    pi.Location = new Point(i * 50, j * 50);
                    this.Controls.Add(pi);
                }
            }
        }

        private void InitPiece()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece pi = new Piece("1");
                    pi.Location = new Point(i * 50+10, j * 50+10);
                    Controls.Add(pi);
                }
            }
            for (int i = 6; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece pi = new Piece("2");
                    pi.Location = new Point(i * 50 + 10, j * 50 + 10);
                    Controls.Add(pi);
                }
            }
        }
    }

    public class Piece : PictureBox, IEnumerable<Piece>
    {
        #region Def
        private string _type;

        internal static class Xio
        {
            public static Piece xopi;
        }
        public Piece(string type)
        {
            Type = type;
            BackColor = Type == "1" ? Color.Red : Color.Blue;
            //Click += (obj, eArgs) => { Location = MoveIt(); };
            Click += (obj, eArgs) => { MoveThat(); };
            Height = 30;
            Width = 30;         
        }
        public string Type { get => _type; set => _type = value; }

        public IEnumerator<Piece> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
        private Point MoveIt()
        {
            foreach (Piece pie in Parent.Controls.OfType<Piece>())
            {
                if (((Location.X == pie.Location.X + 50  && Type == "2") || (Location.X == pie.Location.X - 50 && Type == "1")) && Location.Y == pie.Location.Y)
                {
                    foreach (Piece pie2 in Parent.Controls.OfType<Piece>())
                    {
                        if ((Type == "2" && Location.X == pie2.Location.X + (2 * 50)) && pie2.Location.Y == Location.Y)
                        {
                            return new Point(Location.X, Location.Y);
                        }
                        else if ((Type == "1" && Location.X == pie2.Location.X - (2 * 50)) && pie2.Location.Y == Location.Y)
                        {
                            return new Point(Location.X, Location.Y);
                        }                                                        
                    }
                    if (Type == "1") return new Point(Location.X + (2*50), Location.Y);
                    else return new Point(Location.X - (2*50), Location.Y);
                }               
            }
            if (Type == "1") return new Point(Location.X + 50, Location.Y);
            else return new Point(Location.X - 50, Location.Y );
        }

        private void MoveThat()
        {
            Button it = new Button();
            it.Location = new Point(Location.X - 60, Location.Y-10);
            Button it2 = new Button();
            it2.Location = new Point(Location.X-10 , Location.Y - 60);
            it.BackColor = Color.Black;
            it2.BackColor = Color.Gold;
            it.Height = it2.Height = it.Width = it2.Width = 50;
            it.Click += new EventHandler(btn_Click);
            it2.Click += new EventHandler(btn_Click);
            
            Parent.Controls.Add(it);
            Parent.Controls.Add(it2);
            it.BringToFront();
            it2.BringToFront();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button it = sender as Button;
            
            it.Click -= new EventHandler(btn_Click);
            this.Parent.Controls.Remove(it);
        }

    }
    public class Plateau : Label
    {
        #region Attributs
        private string _type;
        #endregion
        public Plateau(string type)
        {
            Type = type;
            BackColor = Type == "1" ? Color.White : Color.Black;
            Height = 50;
            Width = 50;
        }

        #region Property
        public string Type { get => _type; set => _type = value; }
        #endregion
        

    }
}

