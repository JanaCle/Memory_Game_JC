using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using NBitcoin;
using System;
using System.Xml;

namespace Memory_Game_JC
{
    public partial class Memory_game : Form
    {
        PictureBox firstGuess;
        Random rnd = new Random();
        int numberClicked = 0;
        int correctCards = 0;

        public Memory_game()
        {
            InitializeComponent();
            removePreviousImages();
            System.Drawing.Image photo = new Bitmap(@"C:\Users\cleja\OneDrive\Bureaublad\2022-2023\Devops\Memory_Game_JC\Memory_Game_JC\SQL_images\Cards.png");
            byte[] pic = ImageToByte(photo, System.Drawing.Imaging.ImageFormat.Png);
            SaveImage(pic,1);
            LoadImage(1);
            System.Drawing.Image photo2 = new Bitmap(@"C:\Users\cleja\OneDrive\Bureaublad\2022-2023\Devops\Memory_Game_JC\Memory_Game_JC\SQL_images\Bulbasaur.png");
            byte[] pic2 = ImageToByte(photo2, System.Drawing.Imaging.ImageFormat.Png);
            SaveImage(pic2,2);
            System.Drawing.Image photo3 = new Bitmap(@"C:\Users\cleja\OneDrive\Bureaublad\2022-2023\Devops\Memory_Game_JC\Memory_Game_JC\SQL_images\Chansey.png");
            byte[] pic3 = ImageToByte(photo3, System.Drawing.Imaging.ImageFormat.Png);
            SaveImage(pic3,3);
            System.Drawing.Image photo4 = new Bitmap(@"C:\Users\cleja\OneDrive\Bureaublad\2022-2023\Devops\Memory_Game_JC\Memory_Game_JC\SQL_images\Charmander.png");
            byte[] pic4 = ImageToByte(photo4, System.Drawing.Imaging.ImageFormat.Png);
            SaveImage(pic4,4);
            System.Drawing.Image photo5 = new Bitmap(@"C:\Users\cleja\OneDrive\Bureaublad\2022-2023\Devops\Memory_Game_JC\Memory_Game_JC\SQL_images\Eevee.png");
            byte[] pic5 = ImageToByte(photo5, System.Drawing.Imaging.ImageFormat.Png);
            SaveImage(pic5,5);
            System.Drawing.Image photo6 = new Bitmap(@"C:\Users\cleja\OneDrive\Bureaublad\2022-2023\Devops\Memory_Game_JC\Memory_Game_JC\SQL_images\Gengar.png");
            byte[] pic6 = ImageToByte(photo6, System.Drawing.Imaging.ImageFormat.Png);
            SaveImage(pic6,6);
            System.Drawing.Image photo7 = new Bitmap(@"C:\Users\cleja\OneDrive\Bureaublad\2022-2023\Devops\Memory_Game_JC\Memory_Game_JC\SQL_images\Jigglypuff.png");
            byte[] pic7 = ImageToByte(photo7, System.Drawing.Imaging.ImageFormat.Png);
            SaveImage(pic7,7);
            System.Drawing.Image photo8 = new Bitmap(@"C:\Users\cleja\OneDrive\Bureaublad\2022-2023\Devops\Memory_Game_JC\Memory_Game_JC\SQL_images\Pikachu.png");
            byte[] pic8 = ImageToByte(photo8, System.Drawing.Imaging.ImageFormat.Png);
            SaveImage(pic8,8);
            System.Drawing.Image photo9 = new Bitmap(@"C:\Users\cleja\OneDrive\Bureaublad\2022-2023\Devops\Memory_Game_JC\Memory_Game_JC\SQL_images\Poliwag.png");
            byte[] pic9 = ImageToByte(photo9, System.Drawing.Imaging.ImageFormat.Png);
            SaveImage(pic9,9);
            System.Drawing.Image photo10 = new Bitmap(@"C:\Users\cleja\OneDrive\Bureaublad\2022-2023\Devops\Memory_Game_JC\Memory_Game_JC\SQL_images\Psyduck.png");
            byte[] pic10 = ImageToByte(photo10, System.Drawing.Imaging.ImageFormat.Png);
            SaveImage(pic10,10);
            System.Drawing.Image photo11 = new Bitmap(@"C:\Users\cleja\OneDrive\Bureaublad\2022-2023\Devops\Memory_Game_JC\Memory_Game_JC\SQL_images\Sentret.png");
            byte[] pic11 = ImageToByte(photo11, System.Drawing.Imaging.ImageFormat.Png);
            SaveImage(pic11,11) ;
            System.Drawing.Image photo12 = new Bitmap(@"C:\Users\cleja\OneDrive\Bureaublad\2022-2023\Devops\Memory_Game_JC\Memory_Game_JC\SQL_images\Squirtle.png");
            byte[] pic12 = ImageToByte(photo12, System.Drawing.Imaging.ImageFormat.Png);
            SaveImage(pic12,12);
            System.Drawing.Image photo13 = new Bitmap(@"C:\Users\cleja\OneDrive\Bureaublad\2022-2023\Devops\Memory_Game_JC\Memory_Game_JC\SQL_images\Topegi.png");
            byte[] pic13 = ImageToByte(photo13, System.Drawing.Imaging.ImageFormat.Png);
            SaveImage(pic13,13);
            LoadImage(0);

        }

        public byte[] ImageToByte(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                return imageBytes;
            }
        }
        //public Image Base64ToImage(string base64String)
        public System.Drawing.Image ByteToImage(byte[] imageBytes)
        {
            // Convert byte[] to Image
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = new Bitmap(ms);
            return image;
        }
        /***************** SQLite **************************/
        void SaveImage(byte[] imagen, int number)
        {
            string conStringDbPokemon = @" Data Source = c:\sqlite\db_pokemon.db";
            SQLiteConnection con = new SQLiteConnection(conStringDbPokemon);
            con.Open();
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandText = String.Format("INSERT INTO pokemon (id,image) VALUES ("+ number +",@0);");
            SQLiteParameter param = new SQLiteParameter("@0", System.Data.DbType.Binary);
            param.Value = imagen;
            cmd.Parameters.Add(param);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc1)
            {
                MessageBox.Show(exc1.Message);
            }
            con.Close();
        }

        void removePreviousImages()
        {
            string conString = @" Data Source = c:\sqlite\db_pokemon.db";
            SQLiteConnection con = new SQLiteConnection(conString);
            con.Open();
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandText = String.Format("DELETE FROM pokemon;");
            cmd.ExecuteNonQuery();
        }
        void LoadImage(int number)
        {
            //random images
            if (number == 0)
            {
                Random random = new Random();
                int count = 0;
                List<int> numbers = new List<int>();
                List<int> listNumbers = new List<int>();
                int minRange = 2;
                int maxRange = 13;
                for (int i = 0; i < 8; i++)
                {
                    listNumbers.Add(i);
                }
                while (listNumbers.Count > 0)
                {
                    int rdnumber = random.Next(minRange, maxRange + 1);
                    if (!numbers.Contains(rdnumber) && listNumbers.Count > 0)
                    {
                        numbers.Add(rdnumber);
                        numbers.Add(rdnumber);
                        listNumbers.Remove(count);
                        count++;
                    }
                }
                foreach(var pictureBox in Controls.OfType<PictureBox>())
                {
                    int randomSelection = random.Next(numbers.Count);
                    string query = "SELECT image FROM pokemon WHERE id='" + numbers[randomSelection] + "';";
                    pictureBox.Tag = numbers[randomSelection];
                    numbers.RemoveAt(randomSelection);
                    string conString = @" Data Source = c:\sqlite\db_pokemon.db";
                    SQLiteConnection con = new SQLiteConnection(conString);
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, con);

                    try
                    {
                        IDataReader rdr = cmd.ExecuteReader();
                        try
                        {
                            while (rdr.Read())
                            {
                                if (!Convert.IsDBNull(rdr["image"]))
                                {
                                    //show cards
                                    //byte[] a = (byte[])rdr["image"];
                                    //pictureBox.Image = ByteToImage(a);
                                    pictureBox.Visible = false;
                                    pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;


                                }

                            }
                        }
                        catch (Exception exc) { MessageBox.Show(exc.Message); }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    con.Close();
                }
            }
            else
            {
                string query = "SELECT image FROM pokemon WHERE id='" + number + "';";
                string conString = @" Data Source = c:\sqlite\db_pokemon.db";
                SQLiteConnection con = new SQLiteConnection(conString);
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, con);

                try
                {
                    IDataReader rdr = cmd.ExecuteReader();
                    try
                    {
                        while (rdr.Read())
                        {
                            if (!Convert.IsDBNull(rdr["image"]))
                            {
                                foreach (var pictureBox in Controls.OfType<PictureBox>())
                                {
                                    byte[] a = (byte[])rdr["image"];
                                    pictureBox.Image = ByteToImage(a);
                                    pictureBox.Visible = false;
                                    pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
                                }

                            }
                        }
                    }
                    catch (Exception exc) { MessageBox.Show(exc.Message); }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                con.Close();
            }
            
        }
            

    private void Memory_game_Load(object sender, EventArgs e)
        {
        }

        private async void clickImage(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            if (firstGuess == null)
            {
                string query = "SELECT image FROM pokemon WHERE id='" + pic.Tag + "';";
                string conString = @" Data Source = c:\sqlite\db_pokemon.db";
                SQLiteConnection con = new SQLiteConnection(conString);
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, con);

                try
                {
                    IDataReader rdr = cmd.ExecuteReader();
                    try
                    {
                        while (rdr.Read())
                        {
                            if (!Convert.IsDBNull(rdr["image"]))
                            {
                                byte[] a = (byte[])rdr["image"];
                                pic.Image = ByteToImage(a);
                            }

                        }
                    }
                    catch (Exception exc) { MessageBox.Show(exc.Message); }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                con.Close();
                numberClicked++;
                firstGuess = pic;
            }
            else
            {
                if(firstGuess.Tag.ToString() == pic.Tag.ToString())
                {
                    //show card
                    string query = "SELECT image FROM pokemon WHERE id='" + pic.Tag + "';";
                    string conString = @" Data Source = c:\sqlite\db_pokemon.db";
                    SQLiteConnection con = new SQLiteConnection(conString);
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, con);
                    IDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (!Convert.IsDBNull(rdr["image"]))
                        {
                            byte[] a = (byte[])rdr["image"];
                            pic.Image = ByteToImage(a);
                            
                        }
                    }
                    numberClicked = 0;
                    rdr.Dispose();
                    rdr.Close();
                    con.Close();
                    pic.Enabled = false;
                    firstGuess.Enabled = false;
                    firstGuess = null;
                    correctCards++;
                    //all cards have been found
                    if (correctCards == 8)
                    {
                        foreach (var picture in Controls.OfType<PictureBox>())
                        {
                            picture.Visible = false;
                            

                        }
                        buttonReset.Visible = false;
                        titlememorygame.Visible = true;
                        titlememorygame.Text = "You have won!";
                        buttonPlay.Visible = true;
                        buttonPlay.Text = "Play again";

                    }
                }
                else
                {
                    //show card
                    string query = "SELECT image FROM pokemon WHERE id='" + pic.Tag + "';";
                    string conString = @" Data Source = c:\sqlite\db_pokemon.db";
                    SQLiteConnection con = new SQLiteConnection(conString);
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, con);
                    IDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            if (!Convert.IsDBNull(rdr["image"]))
                            {
                                byte[] a = (byte[])rdr["image"];
                                pic.Image = ByteToImage(a);
                            }
                        }
                    numberClicked = 0;
                    rdr.Dispose();
                    rdr.Close();
                    con.Close();
                    await Task.Delay(1000);
                    //hide card
                    string query2 = "SELECT image FROM pokemon WHERE id='1';";
                    string conString2 = @" Data Source = c:\sqlite\db_pokemon.db";
                    SQLiteConnection con2 = new SQLiteConnection(conString2);
                    con2.Open();
                    SQLiteCommand cmd2 = new SQLiteCommand(query2, con2);
                    IDataReader rdr2 = cmd2.ExecuteReader();
                    while (rdr2.Read())
                    {
                        if (!Convert.IsDBNull(rdr2["image"]))
                        {
                            byte[] a = (byte[])rdr2["image"];
                            pic.Image = ByteToImage(a);
                            firstGuess.Image = ByteToImage(a);
                        }
                    }
                    numberClicked = 0;

                    rdr.Dispose();
                    rdr.Close();
                    con.Close();
                    firstGuess = null;

                }

            }
            
        }
        private void buttonReset_Click(object sender, EventArgs e)
        {
            LoadImage(0);
            LoadImage(1);
            foreach (var picture in Controls.OfType<PictureBox>())
            {
                picture.Enabled = true;

            }
            buttonPlay.Text = "Try again";
            buttonPlay.Visible = true;
            titlememorygame.Visible = false;
            buttonReset.Visible = false;

        }

        private void Memory_game_Load_1(object sender, EventArgs e)
        {

        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            buttonPlay.Visible = false;
            buttonPlay.Text = "Play now";
            titlememorygame.Visible = false;
            buttonReset.Visible = true;
            LoadImage(0);
            LoadImage(1);
            foreach (var picture in Controls.OfType<PictureBox>())
            {
                picture.Visible = true;
                picture.Enabled = true;

            }

        }
    }
}
