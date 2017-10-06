using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YasirErkam
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Noktalar noktalar;
        Random rnd;

        public Form1()
        {
            InitializeComponent();
            graphics = panel1.CreateGraphics();
            noktalar = new Noktalar();
            rnd = new Random();
        }

        /// <summary>
        /// sorgu aktif değilse panel1 üzerinde tıklama yapıldığında agaca ekleme yapar
        /// <para>sorgu aktif ise tıklama yapılan noktada agac üzerinden dairesel sorgu yapar</para>
        /// </summary>
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!noktalar.sorguAktif) // sorgu aktif değilse mause tıklamasıyla agaca duğum ekler
            {
                DortluAgac.EkleNoktaDortluAgaca(ref noktalar.baslangic, e.Location);

                listBox1.Items.Clear();
                listBox1.Items.Add("(" + e.X + ", " + e.Y + ") merkezli nokta ağaca eklendi.");

                noktalar.CizdirEkranaNoktalari(graphics, noktalar.baslangic, 0, 511, 0, 511);
            }
            else// sorgu aktif ise mause tıklamasıyla aranan bölgede sorgu yapar
            {
                Color renk = Color.FromKnownColor(KnownColor.MediumAquamarine + rnd.Next(7));

                listBox2.Items.Clear();
                listBox2.Items.Add("- " + renk.Name + " renginde " + "(" + e.X + ", " + e.Y + ") merkezli " + noktalar.sorguCapi + "br çapında dairesel sorgu sonucu : ");

                DortluAgac.FalseYapSrgdaBlndu(ref noktalar.baslangic);// yeni sorgudan önce daha önceki sorguda true yapılmış  düğümlerin sorgudaBulundu değerlerini false yapar
                noktalar.YapSorgu(listBox2, noktalar.baslangic, e.X, e.Y, noktalar.sorguCapi / 2);

                listBox2.Items.Add("  " + (listBox2.Items.Count - 1) + " adet nokta bulundu : ");
                listBox2.Sorted = true;

                graphics.Clear(Color.White);
                noktalar.CizdirEkranaNoktalari(graphics, noktalar.baslangic, 0, 511, 0, 511);
                graphics.DrawEllipse(new Pen(renk), e.X - (noktalar.sorguCapi / 2), e.Y - (noktalar.sorguCapi / 2), noktalar.sorguCapi, noktalar.sorguCapi);
            }
        }

        /// <summary>
        /// ağaca rasgele nokta ekler
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            Point nokta = new Point(rnd.Next(512), rnd.Next(512));
            DortluAgac.EkleNoktaDortluAgaca(ref noktalar.baslangic, nokta);

            listBox1.Items.Clear();
            listBox1.Items.Add("(" + nokta.X + ", " + nokta.Y + ") merkezli nokta ağaca eklendi.");

            noktalar.CizdirEkranaNoktalari(graphics, noktalar.baslangic, 0, 511, 0, 511);
        }

        /// <summary>
        /// agacı siler ve ekrani temizler
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Ağaç silindi."); /// listbox1 e ağaç silindi bilgisini yazdırır

            noktalar.SilAgaciVeEkraniTemizle(graphics);
        }

        /// <summary>
        /// basıldığında mause ile sorgu aktifleşir ya da mause ile ekleme aktifleşir
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            noktalar.AcKapaSorguyu(label3);
        }

        /// <summary>
        /// yapılan dairesel sorgunun çapını değiştirmeye yarar
        /// </summary>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            noktalar.sorguCapi = (float)numericUpDown1.Value;
        }

        /// <summary>
        /// son yapılan sorguyu listBox2 den ve ekrandan siler
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();

            graphics.Clear(Color.White);

            DortluAgac.FalseYapSrgdaBlndu(ref noktalar.baslangic);///  daha önceki sorguda true yapılmış  düğümlerin sorgudaBulundu değerlerini false yapar
            noktalar.CizdirEkranaNoktalari(graphics, noktalar.baslangic, 0, 511, 0, 511);
        }
    }
}
