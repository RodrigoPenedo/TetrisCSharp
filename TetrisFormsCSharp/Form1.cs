using System;
using System.Windows.Forms;
using System.Drawing;

namespace TetrisFormsCSharp
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);
            Start();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = 150;
            timer.Start();
        }

        //Tetrominos will have a Name, a Shape,
        //Position on the screen and a Potential Position
        public struct Tetromino
        {
            public String Name;
            public int[,] Shape;
            public int[] Position;
            public int[] PotentialPosition;
            public bool move;
        }

        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Temp_piece.PotentialPosition = Temp_piece.Position;
            
            if (Temp_piece.Name != null) { 

                if (e.KeyChar == 97)
                {
                    MovePiece(0, -1);

                }
                else if (e.KeyChar == 100)
                {
                    MovePiece(0, 1);
                }
                else if (e.KeyChar == 101)
                {
                    Rotate();
                }
                else if (e.KeyChar == 113)
                {
                    Rotate();
                    Rotate();
                    Rotate();
                }


                DisplayBoard();
                DisplayPiece();

                e.Handled = true;
            }
        }

        private void Rotate()
        {
            if (Temp_piece.Name == "I Piece")
            {
                int[,] newArray = new int[4, 4];

                for (int i = 3; i >= 0; i--)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        newArray[j, 3 - i] = Temp_piece.Shape[i, j];
                    }
                }

                Temp_piece.Shape = newArray;
            }
            else if (Temp_piece.Name != "O Piece")
            {
                int[,] newArray = new int[3, 3];

                for (int i = 2; i >= 0; i--)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        newArray[j, 2 - i] = Temp_piece.Shape[i, j];
                    }
                }

                Temp_piece.Shape = newArray;
            }
        }

        private void MovePiece(int N, int M)
        {
            Boolean possiblemove = true;

            Temp_piece.PotentialPosition[0] = Temp_piece.Position[0] + N;
            Temp_piece.PotentialPosition[1] = Temp_piece.Position[1] + M;

            for (int x = 0; x < Temp_piece.Shape.GetLength(0); x++)
            {
                for (int y = 0; y < Temp_piece.Shape.GetLength(1); y++)
                {
                    if (Temp_piece.Shape[x, y] != 0)
                    {
                        try
                        {
                            if (Grid[x + Temp_piece.PotentialPosition[0], y + Temp_piece.PotentialPosition[1]] != 0
                                || Temp_piece.PotentialPosition[0] == 23)
                            {
                                possiblemove = false;
                            }
                            else if (y + Temp_piece.PotentialPosition[1] < 0 || y + Temp_piece.PotentialPosition[1] > 10)
                            {
                                possiblemove = false;
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            possiblemove = false;
                        }
                    }
                }
            }

            if (possiblemove)
            {
                Temp_piece.Position = Temp_piece.PotentialPosition;
            }
            else
            {
                LandPiece();
            }

        }

        //Buttons Array
        private Button[,] Buttons = new Button[10, 24];

        //Grid Array
        private int[,] Grid = {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                               {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};

        Tetromino I_piece = new Tetromino()
        {
            Name = "I Piece",
            Shape = new int[4, 4] {{0, 1, 0, 0},
                                   {0, 1, 0, 0},
                                   {0, 1, 0, 0},
                                   {0, 1, 0, 0}}
        };

        Tetromino O_piece = new Tetromino()
        {
            Name = "O Piece",
            Shape = new int[2, 2] {{2, 2},
                                   {2, 2}}
        };

        Tetromino T_piece = new Tetromino()
        {
            Name = "T Piece",
            Shape = new int[3, 3] {{0, 3, 0},
                                   {3, 3, 3},
                                   {0, 0, 0}}
        };

        Tetromino S_piece = new Tetromino()
        {
            Name = "S Piece",
            Shape = new int[3, 3] {{0, 4, 4},
                                   {4, 4, 0},
                                   {0, 0, 0}}
        };

        Tetromino Z_piece = new Tetromino()
        {
            Name = "Z Piece",
            Shape = new int[3, 3] {{5, 5, 0},
                                   {0, 5, 5},
                                   {0, 0, 0}}
        };

        Tetromino J_piece = new Tetromino()
        {
            Name = "J Piece",
            Shape = new int[3, 3] {{6, 0, 0},
                                   {6, 6, 6},
                                   {0, 0, 0}}
        };

        Tetromino L_piece = new Tetromino()
        {
            Name = "L Piece",
            Shape = new int[3, 3] {{0, 0, 7},
                                   {7, 7, 7},
                                   {0, 0, 0}}
        };

        Tetromino Temp_piece;

        private void Start()
        {
            SetUpButtons();
            DisplayBoard();

            SpawnPiece();
            DisplayPiece();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (Temp_piece.Name == null)
            {
                SpawnPiece();
            }
            else
            {
                MovePiece(1,0);
            }

            DisplayBoard();
            DisplayPiece();
        }

        private void SetUpButtons()
        {
            Buttons[0, 0] = btn00;
            Buttons[1, 0] = btn01;
            Buttons[2, 0] = btn02;
            Buttons[3, 0] = btn03;
            Buttons[4, 0] = btn04;
            Buttons[5, 0] = btn05;
            Buttons[6, 0] = btn06;
            Buttons[7, 0] = btn07;
            Buttons[8, 0] = btn08;
            Buttons[9, 0] = btn09;

            Buttons[0, 1] = btn10;
            Buttons[1, 1] = btn11;
            Buttons[2, 1] = btn12;
            Buttons[3, 1] = btn13;
            Buttons[4, 1] = btn14;
            Buttons[5, 1] = btn15;
            Buttons[6, 1] = btn16;
            Buttons[7, 1] = btn17;
            Buttons[8, 1] = btn18;
            Buttons[9, 1] = btn19;

            Buttons[0, 2] = btn20;
            Buttons[1, 2] = btn21;
            Buttons[2, 2] = btn22;
            Buttons[3, 2] = btn23;
            Buttons[4, 2] = btn24;
            Buttons[5, 2] = btn25;
            Buttons[6, 2] = btn26;
            Buttons[7, 2] = btn27;
            Buttons[8, 2] = btn28;
            Buttons[9, 2] = btn29;

            Buttons[0, 3] = btn30;
            Buttons[1, 3] = btn31;
            Buttons[2, 3] = btn32;
            Buttons[3, 3] = btn33;
            Buttons[4, 3] = btn34;
            Buttons[5, 3] = btn35;
            Buttons[6, 3] = btn36;
            Buttons[7, 3] = btn37;
            Buttons[8, 3] = btn38;
            Buttons[9, 3] = btn39;

            Buttons[0, 4] = btn40;
            Buttons[1, 4] = btn41;
            Buttons[2, 4] = btn42;
            Buttons[3, 4] = btn43;
            Buttons[4, 4] = btn44;
            Buttons[5, 4] = btn45;
            Buttons[6, 4] = btn46;
            Buttons[7, 4] = btn47;
            Buttons[8, 4] = btn48;
            Buttons[9, 4] = btn49;

            Buttons[0, 5] = btn50;
            Buttons[1, 5] = btn51;
            Buttons[2, 5] = btn52;
            Buttons[3, 5] = btn53;
            Buttons[4, 5] = btn54;
            Buttons[5, 5] = btn55;
            Buttons[6, 5] = btn56;
            Buttons[7, 5] = btn57;
            Buttons[8, 5] = btn58;
            Buttons[9, 5] = btn59;

            Buttons[0, 6] = btn60;
            Buttons[1, 6] = btn61;
            Buttons[2, 6] = btn62;
            Buttons[3, 6] = btn63;
            Buttons[4, 6] = btn64;
            Buttons[5, 6] = btn65;
            Buttons[6, 6] = btn66;
            Buttons[7, 6] = btn67;
            Buttons[8, 6] = btn68;
            Buttons[9, 6] = btn69;

            Buttons[0, 7] = btn70;
            Buttons[1, 7] = btn71;
            Buttons[2, 7] = btn72;
            Buttons[3, 7] = btn73;
            Buttons[4, 7] = btn74;
            Buttons[5, 7] = btn75;
            Buttons[6, 7] = btn76;
            Buttons[7, 7] = btn77;
            Buttons[8, 7] = btn78;
            Buttons[9, 7] = btn79;

            Buttons[0, 8] = btn80;
            Buttons[1, 8] = btn81;
            Buttons[2, 8] = btn82;
            Buttons[3, 8] = btn83;
            Buttons[4, 8] = btn84;
            Buttons[5, 8] = btn85;
            Buttons[6, 8] = btn86;
            Buttons[7, 8] = btn87;
            Buttons[8, 8] = btn88;
            Buttons[9, 8] = btn89;

            Buttons[0, 9] = btn90;
            Buttons[1, 9] = btn91;
            Buttons[2, 9] = btn92;
            Buttons[3, 9] = btn93;
            Buttons[4, 9] = btn94;
            Buttons[5, 9] = btn95;
            Buttons[6, 9] = btn96;
            Buttons[7, 9] = btn97;
            Buttons[8, 9] = btn98;
            Buttons[9, 9] = btn99;

            Buttons[0, 10] = btn100;
            Buttons[1, 10] = btn101;
            Buttons[2, 10] = btn102;
            Buttons[3, 10] = btn103;
            Buttons[4, 10] = btn104;
            Buttons[5, 10] = btn105;
            Buttons[6, 10] = btn106;
            Buttons[7, 10] = btn107;
            Buttons[8, 10] = btn108;
            Buttons[9, 10] = btn109;

            Buttons[0, 11] = btn110;
            Buttons[1, 11] = btn111;
            Buttons[2, 11] = btn112;
            Buttons[3, 11] = btn113;
            Buttons[4, 11] = btn114;
            Buttons[5, 11] = btn115;
            Buttons[6, 11] = btn116;
            Buttons[7, 11] = btn117;
            Buttons[8, 11] = btn118;
            Buttons[9, 11] = btn119;

            Buttons[0, 12] = btn120;
            Buttons[1, 12] = btn121;
            Buttons[2, 12] = btn122;
            Buttons[3, 12] = btn123;
            Buttons[4, 12] = btn124;
            Buttons[5, 12] = btn125;
            Buttons[6, 12] = btn126;
            Buttons[7, 12] = btn127;
            Buttons[8, 12] = btn128;
            Buttons[9, 12] = btn129;

            Buttons[0, 13] = btn130;
            Buttons[1, 13] = btn131;
            Buttons[2, 13] = btn132;
            Buttons[3, 13] = btn133;
            Buttons[4, 13] = btn134;
            Buttons[5, 13] = btn135;
            Buttons[6, 13] = btn136;
            Buttons[7, 13] = btn137;
            Buttons[8, 13] = btn138;
            Buttons[9, 13] = btn139;

            Buttons[0, 14] = btn140;
            Buttons[1, 14] = btn141;
            Buttons[2, 14] = btn142;
            Buttons[3, 14] = btn143;
            Buttons[4, 14] = btn144;
            Buttons[5, 14] = btn145;
            Buttons[6, 14] = btn146;
            Buttons[7, 14] = btn147;
            Buttons[8, 14] = btn148;
            Buttons[9, 14] = btn149;

            Buttons[0, 15] = btn150;
            Buttons[1, 15] = btn151;
            Buttons[2, 15] = btn152;
            Buttons[3, 15] = btn153;
            Buttons[4, 15] = btn154;
            Buttons[5, 15] = btn155;
            Buttons[6, 15] = btn156;
            Buttons[7, 15] = btn157;
            Buttons[8, 15] = btn158;
            Buttons[9, 15] = btn159;

            Buttons[0, 16] = btn160;
            Buttons[1, 16] = btn161;
            Buttons[2, 16] = btn162;
            Buttons[3, 16] = btn163;
            Buttons[4, 16] = btn164;
            Buttons[5, 16] = btn165;
            Buttons[6, 16] = btn166;
            Buttons[7, 16] = btn167;
            Buttons[8, 16] = btn168;
            Buttons[9, 16] = btn169;

            Buttons[0, 17] = btn170;
            Buttons[1, 17] = btn171;
            Buttons[2, 17] = btn172;
            Buttons[3, 17] = btn173;
            Buttons[4, 17] = btn174;
            Buttons[5, 17] = btn175;
            Buttons[6, 17] = btn176;
            Buttons[7, 17] = btn177;
            Buttons[8, 17] = btn178;
            Buttons[9, 17] = btn179;

            Buttons[0, 18] = btn180;
            Buttons[1, 18] = btn181;
            Buttons[2, 18] = btn182;
            Buttons[3, 18] = btn183;
            Buttons[4, 18] = btn184;
            Buttons[5, 18] = btn185;
            Buttons[6, 18] = btn186;
            Buttons[7, 18] = btn187;
            Buttons[8, 18] = btn188;
            Buttons[9, 18] = btn189;

            Buttons[0, 19] = btn190;
            Buttons[1, 19] = btn191;
            Buttons[2, 19] = btn192;
            Buttons[3, 19] = btn193;
            Buttons[4, 19] = btn194;
            Buttons[5, 19] = btn195;
            Buttons[6, 19] = btn196;
            Buttons[7, 19] = btn197;
            Buttons[8, 19] = btn198;
            Buttons[9, 19] = btn199;

            Buttons[0, 20] = btn200;
            Buttons[1, 20] = btn201;
            Buttons[2, 20] = btn202;
            Buttons[3, 20] = btn203;
            Buttons[4, 20] = btn204;
            Buttons[5, 20] = btn205;
            Buttons[6, 20] = btn206;
            Buttons[7, 20] = btn207;
            Buttons[8, 20] = btn208;
            Buttons[9, 20] = btn209;

            Buttons[0, 21] = btn210;
            Buttons[1, 21] = btn211;
            Buttons[2, 21] = btn212;
            Buttons[3, 21] = btn213;
            Buttons[4, 21] = btn214;
            Buttons[5, 21] = btn215;
            Buttons[6, 21] = btn216;
            Buttons[7, 21] = btn217;
            Buttons[8, 21] = btn218;
            Buttons[9, 21] = btn219;

            Buttons[0, 22] = btn220;
            Buttons[1, 22] = btn221;
            Buttons[2, 22] = btn222;
            Buttons[3, 22] = btn223;
            Buttons[4, 22] = btn224;
            Buttons[5, 22] = btn225;
            Buttons[6, 22] = btn226;
            Buttons[7, 22] = btn227;
            Buttons[8, 22] = btn228;
            Buttons[9, 22] = btn229;

            Buttons[0, 23] = btn230;
            Buttons[1, 23] = btn231;
            Buttons[2, 23] = btn232;
            Buttons[3, 23] = btn233;
            Buttons[4, 23] = btn234;
            Buttons[5, 23] = btn235;
            Buttons[6, 23] = btn236;
            Buttons[7, 23] = btn237;
            Buttons[8, 23] = btn238;
            Buttons[9, 23] = btn239;

            foreach (Button square in Buttons)
            {
                square.Enabled = false;
                square.BackColor = Color.White;
            }
        }

        //Picks a random number and Spanws a random Piece
        private void SpawnPiece()
        {
            Random n = new Random();
            int number = n.Next(1, 8);

            switch (number)
            {
                case 1: { Temp_piece = I_piece; break; }
                case 2: { Temp_piece = O_piece; break; }
                case 3: { Temp_piece = T_piece; break; }
                case 4: { Temp_piece = S_piece; break; }
                case 5: { Temp_piece = Z_piece; break; }
                case 6: { Temp_piece = J_piece; break; }
                case 7: { Temp_piece = L_piece; break; }
            }

            //set initial spawn position
            Temp_piece.Position = new int[2] { 0, 4 };
            Temp_piece.PotentialPosition = new int[2] { 0, 4 };
            Temp_piece.move = true;
        }

        private void LandPiece()
        {
            int newX, newY;

            for (int x = 0; x < Temp_piece.Shape.GetLength(0); x++)
            {
                for (int y = 0; y < Temp_piece.Shape.GetLength(1); y++)
                {
                    try
                    {
                        newX = x + Temp_piece.Position[0] - 1;
                        newY = y + Temp_piece.Position[1];

                        if (Temp_piece.Shape[x, y] != 0)
                        {
                            Grid[newX, newY] = Temp_piece.Shape[x, y];
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {

                    }
                }
            }

            Temp_piece = new Tetromino()
            {
                Name = null,
                Shape = new int[2, 2] {{0,0},
                                       {0,0}}
            };
        }

        private void DisplayPiece()
        {
            int[,] Shape = Temp_piece.Shape;
            int[] Position = Temp_piece.Position;
            int X, Y;


            for (int x = 0; x < Temp_piece.Shape.GetLength(0); x++)
            {
                for (int y = 0; y < Temp_piece.Shape.GetLength(1); y++)
                {
                    if (Shape[x, y] != 0)
                    {
                        X = x + Position[0];
                        Y = y + Position[1];

                        SetButtonColour(Shape[x, y], X, Y);
                    }
                }
            }
        }

        private void DisplayBoard()
        {
            int block;

            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {
                    block = Grid[x, y];

                    //depending on each specific block change the button colour
                    SetButtonColour(block, x, y);
                }
            }
        }

        private void SetButtonColour(int number, int y, int x)
        {
            switch (number)
            {
                case 0: { Buttons[x, y].BackColor = Color.Transparent; break; }
                case 1: { Buttons[x, y].BackColor = Color.Aqua; break; }
                case 2: { Buttons[x, y].BackColor = Color.Gold; break; }
                case 3: { Buttons[x, y].BackColor = Color.MediumOrchid; break; }
                case 4: { Buttons[x, y].BackColor = Color.LimeGreen; break; }
                case 5: { Buttons[x, y].BackColor = Color.Crimson; break; }
                case 6: { Buttons[x, y].BackColor = Color.MidnightBlue; break; }
                case 7: { Buttons[x, y].BackColor = Color.DarkOrange; break; }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
