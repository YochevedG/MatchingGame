using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGameApp
{
    public partial class MatchingGame : Form
    {
        System.Windows.Forms.Timer tmr = new() { Interval = 1500 };
        Label firstclicked = null;
        Label secondclicked = null;
        Random rnd = new();
        List<string> numbers = new()

        {
            "1","1", "2","2", "3","3", "4","4","5","5", "6","6","7","7", "8", "8", "9", "9","10", "10", "11", "11",
            "12", "12", "13", "13", "14", "14", "15", "15", "16", "16", "17", "17", "18", "18", "19", "19", "20", "20", "21", "21"
        };

        private enum GameStatusEnum { NotStarted, Playing, Tie, Winner }
        GameStatusEnum gamestatus = GameStatusEnum.NotStarted;

        private enum TurnEnum { Player1, Player2 }
        TurnEnum currentturn = TurnEnum.Player2;

        public MatchingGame()
        {
            InitializeComponent();
            StartUp();
            tmr.Tick += Tmr_Tick;
            btnStart.Click += BtnStart_Click;
        }

        private void DoTurn()
        {
            if (gamestatus == GameStatusEnum.Playing)
            {
                if(currentturn == TurnEnum.Player1 && firstclicked == null && secondclicked == null)
                {
                    currentturn = TurnEnum.Player2;
                }
                else if (currentturn == TurnEnum.Player2 && firstclicked == null && secondclicked == null)
                {
                    currentturn = TurnEnum.Player1;
                }
            }
            lblCurrentTurn.Text = "Current Turn: " + currentturn.ToString();
        }

        private void CalculateScore()
        {
            if (firstclicked.Text == secondclicked.Text && currentturn == TurnEnum.Player1)
            {
                int n = 0;
                bool isint = int.TryParse(lblScrorePlayer1.Text, out n);

                lblScrorePlayer1.Text = decimal.Add(n, 1).ToString();
            }
            else if(firstclicked.Text == secondclicked.Text && currentturn == TurnEnum.Player2)
            {
                int n = 0;
                bool isint = int.TryParse(lblScrorePlayer1.Text, out n);
                lblScorePlayer2.Text = decimal.Add(n, 1).ToString();
            }
        }

        private void DetectWinner()
        {
            foreach (Control c in tblMain.Controls)
            {
                Label numlabel = c as Label;
                if (numlabel != null)
                {
                    if (numlabel.ForeColor == numlabel.BackColor)
                        return;
                }
            }
            
            int n = 0;
            bool b = int.TryParse(lblScrorePlayer1.Text, out n);
            int n2 = 0;
            bool b2 = int.TryParse(lblScorePlayer2.Text, out n2);
            if(n > n2)
            {
                MessageBox.Show("Winner is Player 1!");
            }
            else if (n2 > n)
            {
                MessageBox.Show("Winner is Player 2!");
            }
            else if (n2 == n)
            {
                MessageBox.Show("It's a Tie!");
            }
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            //currentturn = TurnEnum.Player2;
            gamestatus = GameStatusEnum.Playing;
            //DoTurn();
            AssignNumToLabel();
        }

        private void Tmr_Tick(object? sender, EventArgs e)
        {
            tmr.Stop();

            firstclicked.ForeColor = firstclicked.BackColor;
            secondclicked.ForeColor = secondclicked.BackColor;

            firstclicked = null;
            secondclicked = null;
        }

        private void StartUp()
        {
            foreach (Control c in tblMain.Controls)
            {
                Label numlabel = c as Label;
                if (numlabel != null)
                {
                    numlabel.ForeColor = Color.LightSkyBlue;
                    numlabel.Text = "";
                }

            }
        }

        private void AssignNumToLabel()
        {
            foreach (Control c in tblMain.Controls)
            {
                Label numlabel = c as Label;
                if (numlabel != null)
                {
                    int rndnum = rnd.Next(numbers.Count);
                    numlabel.Text = numbers[rndnum];
                    numlabel.ForeColor = numlabel.BackColor;
                    numbers.RemoveAt(rndnum);
                }

            }
        }

        private void lbl1_Click(object sender, EventArgs e)
        {
            DoTurn();
            if (tmr.Enabled == true)
                return;

            Label clickedlabel = sender as Label;
            if (clickedlabel != null)
            {
                if (clickedlabel.ForeColor == Color.Black)
                    return;
                
                if (firstclicked == null)
                {
                    firstclicked = clickedlabel;
                    firstclicked.ForeColor = Color.Black;
                    return;
                }

                secondclicked = clickedlabel;
                secondclicked.ForeColor = Color.Black;

                DetectWinner();

                if (firstclicked.Text == secondclicked.Text)
                {
                    CalculateScore();
                    firstclicked = null;
                    secondclicked = null;
                    return;
                }

                tmr.Start();
            }
        }
    }
}
