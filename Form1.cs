using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KelimeCumleAyırma
{
    public partial class Form1 : Form
    {
        //Proje Sahibi: Zeyneb Eda Yılmaz
        //https://github.com/Zeynebb
        public Form1()
        {InitializeComponent();}
        private void Form1_Load(object sender, EventArgs e)
        {}
        Char[] sesliHarfler = { 'a', 'e', 'ı', 'i', 'o', 'ö', 'u', 'ü','A',
        'E','I','İ','O','Ö','U','Ü'};
        Char[] sessizHarfler = { 'b', 'c', 'ç', 'd', 'f', 'g', 'ğ', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'r', 's', 'ş',
            't', 'v', 'y', 'z', 'B', 'C', 'Ç', 'D', 'F', 'G', 'Ğ', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R',
            'S', 'Ş', 'T', 'V', 'Y', 'Z' };
        Char[] karakter = { ' ', '!', '?', '/', '.', ',', ':', ';'};
        int heceSayisi = 0;
        String hece = "";
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            heceSayisi = 0;
            String metin = textBox1.Text;
            String[] cumleler = metin.Split('.', '!', '?');
            String[] kelimeler = metin.Split(' ', ',', ':', ';','?', '.', '!');
            for (int i = 0; i < kelimeler.Length; i++)
            {
                listBox2.Items.Add("Kelime " + (i + 1) + ": " + kelimeler[i]);
                char[] harfler = kelimeler[i].ToCharArray();
                heceAyir(harfler, kelimeler[i]);
            }
            for (int i = 0; i < cumleler.Length - 1; i++)
            {
                listBox1.Items.Add("Cümle " + (i + 1) + ": " + cumleler[i]);
            }
        }
        public bool SesliHarfMi(char karakter)
        {
            bool sonuc = false;
            for (int i = 0; i < sesliHarfler.Length; i++)
            {
                if (karakter == sesliHarfler[i])
                {
                    sonuc = true;
                    break;
                }
                else
                {
                    sonuc = false;
                }
            }
            return sonuc;
        }
        public bool SessizHarfMi(char karakter)
        {
            bool sonuc = false;
            for (int i = 0; i < sessizHarfler.Length; i++)
            {
                if (karakter == sessizHarfler[i])
                {
                    sonuc = true;
                }
                else
                {
                    sonuc = false;
                }
            }
            return sonuc;
        }
        public int SozKacHeceli(String soz)
        {
            char[] karakterler = soz.ToCharArray();
            for (int s = 0; s < karakterler.Length; s++)
            {
                SesliHarfMi(karakterler[s]);
            }
            listBox3.Items.Add("hece sayısı: " + heceSayisi);
            return heceSayisi;
        }
        public bool boslukMu(char karakterler)
        {
            for (int i = 0; i < karakter.Length; i++)
            {
                if (karakter[i] == karakterler) ;
            }
            return true;
        }
        public void heceAyir(char[] harfler, String kelime)
        {
            for (int i = 0; i < harfler.Length; i++)
            {
                if (SesliHarfMi(harfler[i]) == true)//ilk harf sesli
                {
                    if (i + 1 >= harfler.Length)
                    {
                        listBox3.Items.Add("Hecelenmiş Kelime: " + hece+ kelime.Substring(i));
                        hece = "";
                        break;
                    }
                    else
                    {
                        if (SesliHarfMi(harfler[i + 1]) == false)//ikinci harf sessiz
                        {
                            if (i + 2 >= harfler.Length)
                            {
                                listBox3.Items.Add("Hecelenmiş Kelime: " + hece+kelime.Substring(i));
                                hece = "";
                                break;
                            }
                            else
                            {
                                if (SesliHarfMi(harfler[i + 2]) == false)//3.harf sessiz
                                {
                                    if (i + 3 >= harfler.Length)
                                    {
                                        listBox3.Items.Add("Hecelenmiş Kelime: " + hece+kelime.Substring(i));
                                        hece = "";
                                        break;
                                    }
                                    else
                                    {
                                        if (SesliHarfMi(harfler[i + 3]) == false)//4.harf sessiz
                                        {
                                            hece=hece+kelime.Substring(i, 3) + "-";//ilk 3 harf hece
                                            kelime = kelime.Substring(2);
                                            harfler = kelime.ToCharArray();
                                        }
                                        if (SesliHarfMi(harfler[i + 3]) == true)//4.harf sesliyse 
                                        {
                                            hece=hece+kelime.Substring(i, 2) + "-";//ilk 2 harf hece
                                            kelime = kelime.Substring(1);
                                            harfler = kelime.ToCharArray();
                                        }
                                    }
                                }
                                else if (SesliHarfMi(harfler[i + 2]) == true)//3.harf sesliyse
                                {
                                    hece = hece + kelime.Substring(i, 1) + "-";//ilk harf hece
                                    kelime = kelime.Substring(0);
                                    harfler = kelime.ToCharArray();
                                }
                            }
                        }
                    }
                }
                else if (SesliHarfMi(harfler[i]) == false)//ilk harf sessiz
                {
                    if ((i + 1) >= harfler.Length)
                    {
                        listBox3.Items.Add("Hecelenmiş Kelime: " + hece+ kelime.Substring(i));
                        hece = "";
                        break;
                    }
                    else
                    {
                        if (SesliHarfMi(harfler[i + 1]) == true)//ikinci sesli
                        {
                            if (i + 2 >= harfler.Length)
                            {
                                listBox3.Items.Add("Hecelenmiş Kelime: " + hece+ kelime.Substring(i));
                                hece = "";
                                break;
                            }
                            else
                            {
                                if (SesliHarfMi(harfler[i + 2]) == false)//3.harf sessiz
                                {
                                    if (i + 3 >= harfler.Length)
                                    {
                                        listBox3.Items.Add("Hecelenmiş Kelime: " + hece+ kelime.Substring(i));
                                        hece = "";
                                        break;
                                    }
                                    else
                                    {
                                        if (SesliHarfMi(harfler[i + 3]) == true)//4.harf sesli
                                        {
                                            hece = hece + kelime.Substring(i, 2) + "-";//ilk 2 harf hece**
                                            kelime = kelime.Substring(1);
                                            harfler = kelime.ToCharArray();
                                        }
                                        else if (SesliHarfMi(harfler[i + 3]) == false)//4.sessiz
                                        {
                                            if (i + 4 >= harfler.Length)
                                            {
                                                listBox3.Items.Add("Hecelenmiş Kelime: " +hece+  kelime.Substring(i));
                                                hece = "";
                                                break;
                                            }
                                            else
                                            {
                                                if (SesliHarfMi(harfler[i + 4]) == true)//5.harf sesli
                                                {
                                                    hece = hece + kelime.Substring(i, 3) + "-";//ilk 3 harf hece
                                                    kelime = kelime.Substring(2);
                                                    harfler = kelime.ToCharArray();
                                                }
                                                else if (SesliHarfMi(harfler[i + 4]) == false)//5.harf sessiz
                                                {
                                                    hece = hece + kelime.Substring(i, 4) + "-";//ilk 4 harf hece
                                                    kelime = kelime.Substring(3);
                                                    harfler = kelime.ToCharArray();
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (SesliHarfMi(harfler[i + 2]) == true)//3.harf sesli
                                {
                                    hece=hece+kelime.Substring(i, 2) + "-";//ilk 2 harf hece
                                    kelime = kelime.Substring(1);
                                    harfler = kelime.ToCharArray();
                                }
                            }
                        }
                        else if (SesliHarfMi(harfler[i + 1]) == false)//2.harf sessiz
                        {
                            hece = hece + kelime.Substring(i, 4) + "-";//ilk 4 harf hece
                            kelime = kelime.Substring(3);
                            harfler = kelime.ToCharArray();
                        }
                    }
                }
            }
        }
    }
}


