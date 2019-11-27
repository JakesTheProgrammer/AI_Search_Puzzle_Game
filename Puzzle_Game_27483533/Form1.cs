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
using System.Resources;
using System.Drawing.Imaging;
using System.IO;

namespace Puzzle_Game_27483533
{
    public partial class frm_Display : Form
    {
        private int MOVE_COUNT = 0;
        private string state = " ";
        private Boolean useState = false;
        private Boolean begin = false;

        private ArrayList arrPictureButtons = new ArrayList();  //ArrayLists for the buttons and pictures 
        private ArrayList arrPics = new ArrayList();

        private int[] arrState = new int[9];        //Place the state form a csv file 
        private int[] arrShuffleState = new int[9]; //Place the state of the shuffled buttons in the array

        private Point blankPoint = new Point();     //Blank point 

        private Bitmap imageBitMap;     //Use bitmap to make the picture into smaller sizes 
        

        public frm_Display()
        {
            InitializeComponent();
            
            foreach (Button b in pnlBackGround.Controls)        //Adds all buttons in a button arraylist
            {
                arrPictureButtons.Add(b);
            }
            Point point = new Point(200, 200);      //Make a blank point

            lblWinner.Hide();
        }

        private void frm_Display_Load(object sender, EventArgs e)
        {
            buttonActivation();
        }

        private void shuffleImage()
        {
            Random random = new Random();

            Button tempButton = new Button();

            for (int k = 0; k < 100; k++)       //Shuffles button in the correct paramater positions 
            {
                tempButton = (Button)arrPictureButtons[random.Next(arrPictureButtons.Count)];
                if ((tempButton.Location.X >= 0 && tempButton.Location.X <= 200) && (tempButton.Location.Y <= 200 && tempButton.Location.Y >= 0))
                {
                    moveButton(tempButton);     //Calls the move method 
                }
            }

            useState = true;
        }

        private void buttonActivation()     //Sends button click objects to the correct buttons 
        {
            btn1.Click += Button_Click;
            btn2.Click += Button_Click;
            btn3.Click += Button_Click;
            btn4.Click += Button_Click;
            btn5.Click += Button_Click;
            btn6.Click += Button_Click;
            btn7.Click += Button_Click;
            btn8.Click += Button_Click;
            btn9.Click += Button_Click;
            blankPoint = btn9.Location;
        }

        private void moveButton(Button clicked)
        {
            int blankPosX = blankPoint.X;
            int blankPosY = blankPoint.Y;

            Point tempPoint = new Point();      //Makes temp point for where the blank button must move 

            int direction = getDirection(clicked.Location, blankPoint);

            if (blankPosX == clicked.Location.X || blankPosY == clicked.Location.Y)
            {
                tempPoint = clicked.Location;

                if (blankPosX == clicked.Location.X)
                {
                    foreach (Button buttonX in pnlBackGround.Controls)
                    {
                        int xPosButton = buttonX.Location.X;
                        int xPosClicked = clicked.Location.X;
                        int yPosButton = buttonX.Location.Y;
                        int yPosClicked = clicked.Location.Y;

                        //Check that the postion of the button can move in the defined paramater arguments 
                        if (((yPosButton >= yPosClicked) && (yPosButton < blankPosY) && (xPosButton == blankPosX)
                            || ((yPosButton <= yPosClicked) && (yPosButton > blankPosY) && (xPosButton == blankPosX))))
                        {
                            if (yPosButton <= 200 && yPosButton >= 0)
                            {
                                if (direction == 2)     //Check which direction to move the button 
                                {
                                    if (yPosClicked - 100 == blankPosY)
                                    {
                                        moveUp(buttonX);
                                        blankPoint = tempPoint;
                                    }

                                }
                                else
                                if (direction == 3)     //Check which direction to move the button 
                                {
                                    if (yPosClicked + 100 == blankPosY)
                                    {
                                        moveDown(buttonX);
                                        blankPoint = tempPoint;
                                    }

                                }
                            }
                            
                        }
                    }
                }

                if (blankPosY == clicked.Location.Y)
                {
                    foreach (Button buttonY in pnlBackGround.Controls)  
                    {
                        int xPosButton = buttonY.Location.X;
                        int xPosClicked = clicked.Location.X;
                        int yPosButton = buttonY.Location.Y;
                        int yPosClicked = clicked.Location.Y;

                        //Check that the postion of the button can move in the defined paramater arguments 
                        if (((xPosButton >= xPosClicked) && (xPosButton < blankPosX) && (yPosButton == blankPosY)
                            || ((xPosButton <= xPosClicked) && (xPosButton > blankPosX) && (yPosButton == blankPosY))))
                        {
                            if (xPosButton <= 200 && xPosButton >= 0)
                            {
                                if (direction == 0)     //Check which direction to move the button 
                                {
                                    if (xPosClicked + 100 == blankPosX)
                                    {
                                        moveRight(buttonY);
                                        blankPoint = tempPoint;
                                    }

                                }
                                else
                                if (direction == 1)     //Check which direction to move the button 
                                {
                                    if (xPosClicked - 100 == blankPosX)
                                    {
                                        moveLeft(buttonY);
                                        blankPoint = tempPoint;
                                    }

                                }
                            }
                        }
                    }
                }
           }

            
        }

        private int getDirection(Point buttonClicked, Point blank)      //Change the direction to let the button know where it can move 
        {
            int direction = 0;

            if ((buttonClicked.X < blank.X) && (buttonClicked.Y == blank.Y))
            {
                direction = 0;
            }
            else 
                if ((buttonClicked.X > blank.X) && (buttonClicked.Y == blank.Y)) 
            {
                    direction = 1;
                }
                else
                    if ((buttonClicked.Y > blank.Y) && (buttonClicked.X == blank.X)) 
            {
                        direction = 2;
                    }
                    else
                        if ((buttonClicked.Y < blank.Y) && (buttonClicked.X == blank.X))  
                        {
                            direction = 3;
                        }

            return direction;
        }

        private void Button_Click(object sender, EventArgs e)   //Send that button has been clicked 
        {
            Button myButton = (Button)sender;
            moveButton(myButton);
            MOVE_COUNT++;
            lblNoMoves.Text = MOVE_COUNT.ToString();
            checkWin();
        }

        private void moveUp(Button newButton)       //Moves button up
        {
            int x = newButton.Location.X;
            int y = newButton.Location.Y;
            int hight = newButton.Size.Height;
            newButton.Location = new Point(x, y - hight); 
        }

        private void moveDown(Button newButton)     //Moves button down 
        {
            int x = newButton.Location.X;
            int y = newButton.Location.Y;
            int hight = newButton.Size.Height;
            newButton.Location = new Point(x, y + hight); 
        }

        private void moveRight(Button newButton)    //Move button to the right 
        {
            int x = newButton.Location.X;
            int y = newButton.Location.Y;
            int width = newButton.Size.Width;
            newButton.Location = new Point(x + width, y); 
        }

        private void moveLeft(Button newButton)     //Move button to the left 
        {
            int x = newButton.Location.X;
            int y = newButton.Location.Y;
            int width = newButton.Size.Width;
            newButton.Location = new Point(x - width, y); 
        }

        private void addImageOnButtons()
        {
            // Add the images from the image class ontop of the buttons 
            ImageClass myImageClass = new ImageClass();

            arrPics = myImageClass.ImageTrimArray(imageBitMap, 100, 100);
            btn1.Image = (Image)arrPics[0];
            btn2.Image = (Image)arrPics[1];
            btn3.Image = (Image)arrPics[2];
            btn4.Image = (Image)arrPics[3];
            btn5.Image = (Image)arrPics[4];
            btn6.Image = (Image)arrPics[5];
            btn7.Image = (Image)arrPics[6];
            btn8.Image = (Image)arrPics[7];
        }

        private void resetButtons()    // Place all buttons back to their original positions 
        {
            btn1.Location = new Point(0,0);
            btn2.Location = new Point(100, 0);
            btn3.Location = new Point(200, 0);
            btn4.Location = new Point(0, 100);
            btn5.Location = new Point(100, 100);
            btn6.Location = new Point(200, 100);
            btn7.Location = new Point(0, 200);
            btn8.Location = new Point(100, 200);
            btn9.Location = new Point(200, 200);
            
        }

        private void cheatButtons()
        {
            btn1.Location = new Point(0, 0);
            btn2.Location = new Point(100, 0);
            btn3.Location = new Point(200, 0);
            btn4.Location = new Point(0, 100);
            btn5.Location = new Point(200, 100);//  100, 100
            btn6.Location = new Point(0, 200);//  200, 100
            btn7.Location = new Point(100, 100);//  0, 200
            btn8.Location = new Point(100, 200);
            btn9.Location = new Point(200, 200);
        }

        private void readButtons()
        {
            for(int k = 0; k < arrPictureButtons.Count;k++)
            {
                arrPictureButtons.Remove(k);
            }

            foreach (Button b in pnlBackGround.Controls)
            { 
                arrPictureButtons.Add(b);
            }
            Point point = new Point(200, 200);
        }

        private void checkWin()
        {
            if ((btn1.Location.X == 0) && (btn1.Location.Y == 0))
            {
                if ((btn2.Location.X == 100) && (btn2.Location.Y == 0))
                {
                    if ((btn3.Location.X == 200) && (btn3.Location.Y == 0))
                    {
                        if ((btn4.Location.X == 0) && (btn4.Location.Y == 100))
                        {
                            if ((btn5.Location.X == 100) && (btn5.Location.Y == 100))
                            {
                                if ((btn6.Location.X == 200) && (btn6.Location.Y == 100))
                                {
                                    if ((btn7.Location.X == 0) && (btn7.Location.Y == 200))
                                    {
                                        if ((btn8.Location.X == 100) && (btn8.Location.Y == 200))
                                        {
                                            timer1.Start();
                                            timer1.Enabled = true; /// Display win message 
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private String getComboBoxInfo()
        {
            return cmbImage.Text;  // Get the picture name from the combobox 
        }

        private void selectImageToDisplay(string image)
        {
            // Select coresponding picture to what the user has chosen 
            if(image.Equals("Dog"))
            {
                imageBitMap = new Bitmap(Properties.Resources._330px_Female_German_Shepherd);
                pictureBox1.BackgroundImage = Properties.Resources._330px_Female_German_Shepherd;
            }

            if(image.Equals("My Little Pony"))
            {
                imageBitMap = new Bitmap(Properties.Resources.LittlePony);
                pictureBox1.BackgroundImage = Properties.Resources.LittlePony;
            }

            if (image.Equals("SpaceX Starman"))
            {
                imageBitMap = new Bitmap(Properties.Resources.SpaceX);
                pictureBox1.BackgroundImage = Properties.Resources.SpaceX;
            }

            if (image.Equals("Volcano"))
            {
                imageBitMap = new Bitmap(Properties.Resources.Volcano);
                pictureBox1.BackgroundImage = Properties.Resources.Volcano;
            }

        }

        private void btn6_Click(object sender, EventArgs e)
        {
            /// no code 
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            if (begin == true)
            {
                selectImageToDisplay(getComboBoxInfo());
                addImageOnButtons();
                if (useState == false)
                {
                    shuffleImage();
                }
                else
                {
                    //readButtons();
                    //shuffleImage();
                    //cheatButtons();
                   

                    shuffleImage();
                }
            }
            else
            {
                MessageBox.Show("Please select a image first", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            timer1.Stop();
            lblWinner.Hide();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Giving up there buddy?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                shuffleImage();
                MOVE_COUNT = 0;
                lblNoMoves.Text = MOVE_COUNT.ToString();
                timer1.Stop();
                lblWinner.Hide();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit this great game?", "Exit",MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                Application.Exit();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string message = "These are some tips to help you play the game and help you to interact with the game";
            message += "\n\nFirst select an image from the dropdown box in the information panel";
            message += "\nIf you want to use your own board state please select yes, and choose the file";
            message += "\nPress the start game button to load the image";
            message += "\n\nYou can choose to either, let the AI solve it with different search methods, or you can move the buttons self";
            message += "\nIf you want to restart the game or play again, please press the Play Again button";

            MessageBox.Show(message, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBreadth_Click(object sender, EventArgs e)
        {
            BFS bfs = new BFS(state);
            bfs.bfsSearch();

            if (bfs.getFound())
            {
                resetButtons();
                MessageBox.Show("Solution was Found\nNumber of moves:" + "\nCan you Beat the AI?", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Media.SystemSounds.Beep.Play();
                timer1.Start();
                timer1.Enabled = true;
                
            }

            lblNoMoves.Text = bfs.getCounter().ToString();
        }

        private void btnDepth_Click(object sender, EventArgs e)
        {
            DFS dfs = new DFS(state);//284603517
            dfs.dfsSearch();

            if (dfs.getFound())
            {
                resetButtons();
                MessageBox.Show("Solution was Found" + "\nCan you Beat the AI?", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Media.SystemSounds.Beep.Play();
                timer1.Start();
                timer1.Enabled = true;
            }

            lblNoMoves.Text = dfs.getCounter().ToString();
        }

        private void cmbImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string initialState = "";

            if (MessageBox.Show("Do you want to load a specific board state?", "Board State", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OpenFileDialog open = new OpenFileDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader reader = new StreamReader(open.FileName))
                    {
                        var line = reader.ReadLine();
                        string[] values = line.Split(',');

                        for (int i = 0; i < arrState.Length; i++)
                        {
                            arrState[i] = Convert.ToInt32(values[i]);
                        }

                        reader.Close();
                    }
                }
                for (int k = 0; k < arrState.Length; k++)
                {
                    initialState += arrState[k];
                }

                state = initialState.Substring(0, 9);
                //MessageBox.Show(state.Length.ToString() + "\n" + state);

                MessageBox.Show("Press the Start Game button", "Play", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //useState = true; // let the system know not to make its own shuffle

                begin = true;
            }
            else
            {
                useState = false;
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)    //Spescial RGB effect for label
        {
            Random rand = new Random();
            int A = rand.Next(0, 255);
            int R = rand.Next(0, 255);
            int G = rand.Next(0, 255);
            int B = rand.Next(0, 255);
            lblWinner.Show();
            lblWinner.ForeColor = Color.FromArgb(A, R, G, B);
        }
    }
}
