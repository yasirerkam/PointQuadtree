using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace YasirErkam
{
    /// <summary>
    /// dortlu agac üzerinde ekleme, silme, sorgu, ekrana cizdirme gibi işlemleri yapan siniftir.
    /// </summary>
    class Noktalar
    {
        int noktaCapi;
        public DortluAgac baslangic;
        public bool sorguAktif;
        public float sorguCapi;

        /// <summary>
        /// yapıcı metotdur. varsayılan ilk deger atamalarini yapar
        /// </summary>
        public Noktalar()
        {
            noktaCapi = 6;
            sorguAktif = false;
            sorguCapi = 100;
        }

        /// <summary>
        /// agaca null değeri atar ve ekrandaki noktalari silip ekrani temizler
        /// </summary>
        /// <param name="graphics">ekrandaki temizlemek için kullanılan fonksiyonun nesnesini tutar</param>
        public void SilAgaciVeEkraniTemizle(Graphics graphics)
        {
            baslangic = null;
            graphics.Clear(Color.White);
        }

        /// <summary>
        /// mause ile tıklama yapıldığında ekleme mi yoksa sorgu mu yapacağını belirler, sonucu verilen labele yazar
        /// </summary>
        /// <param name="label">sorgunun aktif olup olmadığının yazıldığı labeli tutar</param>
        public void AcKapaSorguyu(System.Windows.Forms.Label label)
        {
            if (sorguAktif)
            {
                sorguAktif = false;
                label.Text = "Sorgu İnaktif";
            }
            else
            {
                sorguAktif = true;
                label.Text = "Sorgu Aktif";
            }
        }
        /// <summary>
        /// düğümün agac üzerinde bulunup bulunmadığını sorgular ve sonucu ListBox a yazdırır.
        /// </summary>
        /// <param name="lB"> sonucun yazdırılacağı listbox</param>
        /// <param name="ilk">üzerinde sorgunun yapılacağı ağaç</param>
        /// <param name="x">sorgunun yapılacağı dairenin x parametresi</param>
        /// <param name="y">sorgunun yapılacağı dairenin y parametresi</param>
        /// <param name="yariCap">sorgunun yapılacağı dairenin capı</param>
        public void YapSorgu(System.Windows.Forms.ListBox lB, DortluAgac ilk, float x, float y, float yariCap)
        {
            if (ilk == null) // düğüm null ise
            {
                return;
            }
            else
            {
                /// düğüm null değilse sorgu için 4 drum vardır:

                ///1. drum : düğüm sorgu dairesinin içindeyse düğümün tüm alt düğümleri recursive olarak sorguya gönderilir
                if (Math.Pow((x - ilk.x), 2) + Math.Pow((y - ilk.y), 2) <= Math.Pow(yariCap, 2))
                {
                    lB.Items.Add(ilk.x.ToString("  x : 000") + ilk.y.ToString("  y : 000"));
                    ilk.sorgudaBulundu = true;

                    YapSorgu(lB, ilk.bolge1, x, y, yariCap);
                    YapSorgu(lB, ilk.bolge2, x, y, yariCap);
                    YapSorgu(lB, ilk.bolge3, x, y, yariCap);
                    YapSorgu(lB, ilk.bolge4, x, y, yariCap);
                }
                ///2. drum : sorgu dairesi düğümün 4 quadrantından ikisini kesiyorsa,
                ///sorgu dairesinin çaprazındaki bölge hariç 3 alt bölgesindeki düğümleri recursive olarak sorguya gönderilir
                else if (Math.Abs(x - ilk.x) < yariCap && Math.Abs(y - ilk.y) < yariCap) // sorgu ve düğümün hem x hem de y degerlerinin farki yaricaptan küçükse
                {
                    if (x > ilk.x && y > ilk.y) // sorgu merkezi 4. bölgedeyse
                    {
                        YapSorgu(lB, ilk.bolge1, x, y, yariCap);
                        YapSorgu(lB, ilk.bolge3, x, y, yariCap);
                        YapSorgu(lB, ilk.bolge4, x, y, yariCap);
                    }
                    if (x < ilk.x && y > ilk.y) // sorgu merkezi 3. bölgedeyse
                    {
                        YapSorgu(lB, ilk.bolge2, x, y, yariCap);
                        YapSorgu(lB, ilk.bolge3, x, y, yariCap);
                        YapSorgu(lB, ilk.bolge4, x, y, yariCap);
                    }
                    if (x < ilk.x && y < ilk.y) // sorgu merkezi 2. bölgedeyse
                    {
                        YapSorgu(lB, ilk.bolge1, x, y, yariCap);
                        YapSorgu(lB, ilk.bolge2, x, y, yariCap);
                        YapSorgu(lB, ilk.bolge3, x, y, yariCap);
                    }
                    if (x > ilk.x && y < ilk.y) // sorgu merkezi 1. bölgedeyse
                    {
                        YapSorgu(lB, ilk.bolge1, x, y, yariCap);
                        YapSorgu(lB, ilk.bolge2, x, y, yariCap);
                        YapSorgu(lB, ilk.bolge4, x, y, yariCap);
                    }
                }
                ///3. drum : sorgu dairesi düğümün 4 quadrantından sadece birini kesiyorsa,
                ///sorgu dairesinin kestiği bölge ve kendi bulunduğu bölge recursive olarak sorguya gönderilir
                else if (Math.Abs(x - ilk.x) > yariCap && Math.Abs(y - ilk.y) < yariCap) // sorgu ve düğümün y degerlerinin farki yaricaptan küçükse
                {
                    if (x > ilk.x) // sagindaki çizgiyi kesiyorsa
                    {
                        YapSorgu(lB, ilk.bolge1, x, y, yariCap);
                        YapSorgu(lB, ilk.bolge4, x, y, yariCap);
                    }
                    else// solundaki çizgiyi kesiyorsa
                    {
                        YapSorgu(lB, ilk.bolge2, x, y, yariCap);
                        YapSorgu(lB, ilk.bolge3, x, y, yariCap);
                    }
                }
                else if (Math.Abs(y - ilk.y) > yariCap && Math.Abs(x - ilk.x) < yariCap) // sorgu ve düğümün x degerlerinin farki yaricaptan küçükse
                {
                    if (y > ilk.y)// altindaki çizgiyi kesiyorsa
                    {
                        YapSorgu(lB, ilk.bolge3, x, y, yariCap);
                        YapSorgu(lB, ilk.bolge4, x, y, yariCap);
                    }
                    else// üstündeki çizgiyi kesiyorsa
                    {
                        YapSorgu(lB, ilk.bolge1, x, y, yariCap);
                        YapSorgu(lB, ilk.bolge2, x, y, yariCap);
                    }
                }
                ///4. drum : sorgu dairesi düğümün 4 quadrantından hiçbirini kesmiyorsa 
                ///düğümün tüm alt düğümleri recursive olarak sorguya gönderilir
                else
                {
                    YapSorgu(lB, ilk.bolge1, x, y, yariCap);
                    YapSorgu(lB, ilk.bolge2, x, y, yariCap);
                    YapSorgu(lB, ilk.bolge3, x, y, yariCap);
                    YapSorgu(lB, ilk.bolge4, x, y, yariCap);
                }


            }
        }

        /// <summary>
        /// verilen grafiğe, verilen agacin noktalarını ve quadrantlarını çizdirir
        /// </summary>
        /// <param name="graphics">noktalari ve quadrantlari cizdirecek nesneyi tutar</param>
        /// <param name="bas">noktalari cizdirilecek dortlu agac yapisini tutar</param>
        /// <param name="cizgiBasX">cizdirilecek x cizgisinin baslangic konumunu tutar</param>
        /// <param name="cizgiBitisX">cizdirilecek x cizgisinin bitis konumunu tutar</param>
        /// <param name="cizgiBasY">cizdirilecek y cizgisinin baslangic konumunu tutar</param>
        /// <param name="cizgiBitisY">cizdirilecek y cizgisinin bitis konumunu tutar</param>
        public void CizdirEkranaNoktalari(Graphics graphics, DortluAgac bas, float cizgiBasX, float cizgiBitisX, float cizgiBasY, float cizgiBitisY)
        {
            if (bas != null)
            {
                if (bas.derinlik > 100)
                    bas.derinlik %= 100;
                Color renk = Color.FromKnownColor(KnownColor.DarkBlue + bas.derinlik);

                if (bas.sorgudaBulundu)
                    renk = Color.Red;
                graphics.FillEllipse(new SolidBrush(renk), bas.x - noktaCapi / 2, bas.y - noktaCapi / 2, noktaCapi, noktaCapi);
                graphics.DrawLine(new Pen(renk), cizgiBasY, bas.y, cizgiBitisY, bas.y);
                graphics.DrawLine(new Pen(renk), bas.x, cizgiBasX, bas.x, cizgiBitisX);

                CizdirEkranaNoktalari(graphics, bas.bolge1, cizgiBasX, bas.y, bas.x, cizgiBitisY);
                CizdirEkranaNoktalari(graphics, bas.bolge2, cizgiBasX, bas.y, cizgiBasY, bas.x);
                CizdirEkranaNoktalari(graphics, bas.bolge3, bas.y, cizgiBitisX, cizgiBasY, bas.x);
                CizdirEkranaNoktalari(graphics, bas.bolge4, bas.y, cizgiBitisX, bas.x, cizgiBitisY);
            }

        }

    }
}
