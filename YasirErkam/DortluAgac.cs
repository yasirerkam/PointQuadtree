using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace YasirErkam
{
    /// <summary>
    /// dortlu agac yapisi olusturan, agaca node ekleyen ve agacla ilgili işlemler yapan sınıftır
    /// </summary>
    class DortluAgac
    {
        public bool sorgudaBulundu;
        public static int seviyeDgskeni;
        public int x, y, derinlik;
        public DortluAgac bolge1, bolge2, bolge3, bolge4;

        /// <summary>
        /// dortlu ağaç sınıfının yapıcı metodudur
        /// </summary>
        /// <param name="x">dortlu ağaca eklenecek node un x değerini tutar</param>
        /// <param name="y">dortlu ağaca eklenecek node un y değerini tutar</param>
        /// <param name="derinlik">dortlu ağaca eklenecek node un kacinci derinlikte olduğunu tutar.
        /// <para>bu bilgi dörtlü ağacı ekrana çizdiriken çizgilerin renklerini belirlemek için kullaniliyor</para> </param>
        public DortluAgac(int x, int y, int derinlik)
        {
            this.x = x;
            this.y = y;

            this.derinlik = derinlik;

            sorgudaBulundu = false;
            seviyeDgskeni = 0;
        }

        /// <summary>
        /// ilkAgac parametresiyle verilen bir dörtlü ağaca düğüm ekler
        /// </summary>
        /// <param name="ilkAgac">düğümün ekleneceği ağaçtır</param>
        /// <param name="nokta">eklenecek düğümün verilerini tutar</param>
        public static void EkleNoktaDortluAgaca(ref DortluAgac ilkAgac, Point nokta)
        {
            if (ilkAgac == null)
            {
                ilkAgac = new DortluAgac(nokta.X, nokta.Y, seviyeDgskeni);
            }
            else
            {
                seviyeDgskeni++;

                if (nokta.X >= ilkAgac.x && nokta.Y <= ilkAgac.y)
                {
                    EkleNoktaDortluAgaca(ref ilkAgac.bolge1, nokta);
                }
                else if (nokta.X < ilkAgac.x && nokta.Y <= ilkAgac.y)
                {
                    EkleNoktaDortluAgaca(ref ilkAgac.bolge2, nokta);
                }
                else if (nokta.X < ilkAgac.x && nokta.Y > ilkAgac.y)
                {
                    EkleNoktaDortluAgaca(ref ilkAgac.bolge3, nokta);
                }
                else if (nokta.X >= ilkAgac.x && nokta.Y > ilkAgac.y)
                {
                    EkleNoktaDortluAgaca(ref ilkAgac.bolge4, nokta);
                }
            }
        }

        /// <summary>
        /// agacin düğümlerinin sorgudaBulundu üyelerinin değerini "false" yapar.
        /// </summary>
        /// <param name="ilkAgac">sorgudaBulundu üyeleri false yapilacak agaci tutar</param>
        public static void FalseYapSrgdaBlndu(ref DortluAgac ilkAgac)
        {
            if (ilkAgac != null)
            {
                ilkAgac.sorgudaBulundu = false;

                FalseYapSrgdaBlndu(ref ilkAgac.bolge1);
                FalseYapSrgdaBlndu(ref ilkAgac.bolge2);
                FalseYapSrgdaBlndu(ref ilkAgac.bolge3);
                FalseYapSrgdaBlndu(ref ilkAgac.bolge4);
            }
        }
    }
}
