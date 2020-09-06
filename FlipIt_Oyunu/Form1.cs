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

namespace FlipIt_Oyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int tiklamasayisi = 0;
        int puan = 10000;
        int p1 = 0;
        int p2 = 0;
        string isim;
        private void button1_Click(object sender, EventArgs e)
        {
                grpGame.Controls.Clear();
                lblMoves.Text = "0";
                lblScore.Text = "10000";
                tiklamasayisi = 0;
                puan = 10000;
                if (txtisim.Text != "")
                    isim = txtisim.Text;
                else
                {
                    MessageBox.Show("Lütfen bir isim giriniz.");
                    return;

                }
                   
                if (cmbHorizontal.SelectedItem != null && cmbVertical.SelectedItem != null)
                {
                    p1 = Convert.ToInt32(cmbHorizontal.SelectedItem);
                    p2 = Convert.ToInt32(cmbVertical.SelectedItem);
                    ButonOlustur(p1, p2);
                }
                else
                    MessageBox.Show("Lütfen boyut seçimlerini yapınız");
                
      
        }

        private void DosyayaYaz(string isim, int tiklamasayisi, int puan)
        {

            
                string dosyayolu = @"D:\" + isim + ".txt";
                string[] yazilacakveriler = new string[1];
                yazilacakveriler[0] = "Ad Soyad:" + isim + "---Hamle Sayısı:" + tiklamasayisi.ToString() + "---Puan:" + puan.ToString();
                File.WriteAllLines(dosyayolu, yazilacakveriler);
           
      
        }

        private void ButonOlustur(int p1,int p2)
        {
            for (int i = 0; i < p2; i++)
            {

                for (int j = 0; j < p1; j++)
                {
                    Button b = new Button();
                    b.Width = 50;
                    b.Height = 50;
                    b.Left = (j * 50) + 128;
                    b.Top = (i * 50) + 100;
                    b.BackColor = Color.Black;
                    b.Click += b_Click;
                    grpGame.Controls.Add(b); 
                } 
            }

        }

        void b_Click(object sender, EventArgs e)
        {
            tiklamasayisi++;
            puan -= 50;
            lblMoves.Text = tiklamasayisi.ToString();
            lblScore.Text = puan.ToString();

            Button tiklananbuton = sender as Button;
            tiklananbuton.BackColor = tiklananbuton.BackColor == Color.Black ? Color.White : Color.Black;
            Point btnkordinat = tiklananbuton.Location;
            int Xkordinat = btnkordinat.X;
            int Ykordinat = btnkordinat.Y;
            int yenibtnkordinatX = Xkordinat - 50;
            int yenibtnkordinatY = Ykordinat - 50;
            int yenibtnkordinatX2 = Xkordinat + 50;
            int yenibtnkordinatY2 = Ykordinat + 50;
            int kontrol = 1;
            int sonuc=0;

            for (int i = 0; i < grpGame.Controls.Count; i++)
            {
                if (grpGame.Controls[i] is Button)
                {
                    if (grpGame.Controls[i].Location.X == yenibtnkordinatX && grpGame.Controls[i].Location.Y==Ykordinat)                    
                        grpGame.Controls[i].BackColor = grpGame.Controls[i].BackColor == Color.Black ? Color.White : Color.Black;
                    
                    if (grpGame.Controls[i].Location.Y == yenibtnkordinatY && grpGame.Controls[i].Location.X==Xkordinat)                   
                        grpGame.Controls[i].BackColor = grpGame.Controls[i].BackColor == Color.Black ? Color.White : Color.Black;
                    
                    if (grpGame.Controls[i].Location.X == yenibtnkordinatX2 && grpGame.Controls[i].Location.Y == Ykordinat)                    
                        grpGame.Controls[i].BackColor = grpGame.Controls[i].BackColor == Color.Black ? Color.White : Color.Black;
                    
                    if (grpGame.Controls[i].Location.Y == yenibtnkordinatY2 && grpGame.Controls[i].Location.X == Xkordinat)                   
                        grpGame.Controls[i].BackColor = grpGame.Controls[i].BackColor == Color.Black ? Color.White : Color.Black;
                    
                    foreach (Control b in grpGame.Controls)
                    {
                        if (b is Button)
                            sonuc = b.BackColor==Color.White ? kontrol++ : 0;
                        
                    }
                    if (sonuc==grpGame.Controls.Count)
                    {
                        MessageBox.Show("Tebrikler!");
                        foreach (Control b in grpGame.Controls)
                        {
                            if (b is Button) b.Enabled = false;
                        }
                        btnStart.Text = "Play Again";
                        DosyayaYaz(isim, tiklamasayisi, puan);
                        break;
                    }
                    else
                        if(puan==0)
                        {
                            MessageBox.Show("Kaybettiniz.");
                            foreach (Control b in grpGame.Controls)
                            {
                                if (b is Button) b.Enabled = false;
                            }
                            btnStart.Text="Play Again";
                            break;
                        }
                        else
                        kontrol = 1;
                    
                }
                 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Flip It!";
        }
    }
}
