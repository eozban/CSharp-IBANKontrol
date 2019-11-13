using System;
using System.Collections.Generic;
using System.Text;

namespace ESinTiBiLiSiM
{
    class MyIBAN
    {
        private string[] DonusturmeTablosu_String = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", };
        private string[] DonusturmeTablosu_Number = { "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35" };

        private string  _IBAN_Number = null;
        private bool    _IBAN_Status = false;

        public bool IBAN_Status
        {
            get { return _IBAN_Status; }
            set { _IBAN_Status = value; }
        }

        public string IBAN_Number
        {
            get { return _IBAN_Number; }
            set { _IBAN_Number = value; }
        }

        public bool IsAlpha(string strAlpha)
        {
            System.Text.RegularExpressions.Regex MyObj = new System.Text.RegularExpressions.Regex("[^A-Z]");
            return !MyObj.IsMatch(strAlpha);
        }

        public bool IsNumber(string strNumber)
        {
            System.Text.RegularExpressions.Regex MyObj = new System.Text.RegularExpressions.Regex("[^0-9]");
            return !MyObj.IsMatch(strNumber);
        }

        public bool IsAlphaNumeric(string strAlphaNumeric)
        {
            System.Text.RegularExpressions.Regex MyObj = new System.Text.RegularExpressions.Regex("[^A-Z0-9]");
            return !MyObj.IsMatch(strAlphaNumeric);
        }

        public MyIBAN(string GonderilenIBAN)
        {
            IBAN_Number = GonderilenIBAN.Trim().ToUpper();
            IBAN_Status = KontrolET(IBAN_Number);
        }

        public bool KontrolET(string strIBAN)
        {
        //------------------------------------------------------------------------------
        // Detaylı Bilgi http://www.tcmb.gov.tr/iban/teblig.htm
        //------------------------------------------------------------------------------
        //Karakter Sayısı		Karakter Şekli			        Açıklama
        //      2		        Alfabetik Karakter (A-Z)	    Ülke Kodu
        //      2		        Sayısal Karakter (0-9)		    IBAN Kontrol Basamakları
        //      5		        Sayısal Karakter (0-9)		    Banka Kodu
        //      1		        Sayısal Karakter (0-9)		    Rezerv Alan
        //      16		        Sayısal/Alfabetik Karakter	    Hesap Numarası
        //------------------------------------------------------------------------------

            //Boşsa Retunrn False
            if (String.IsNullOrEmpty(strIBAN.Trim())) return false;

            // uzunluk en fazla 34 karakter olabilir 
            if (strIBAN.Length > 34) return false;

            // ilk iki karakteri alalım
            string str1_2 = strIBAN.Substring(0, 2).Trim();

            // kontrol karakterlerini alalım 
            string str3_4 = strIBAN.Substring(2, 2).Trim();

            string strRezerv = strIBAN.Substring(10, 1).Trim();

            // ilk iki karakter yalnızca harf olabilir 
            if (!IsAlpha(str1_2)) return false;

            // kontrol karakterleri yalnızca sayı olabilir 
            if (!IsNumber(str3_4)) return false;

            // Rezerv Alanı Tüm hesaplarda 0 olmalıdır. 
            if (strRezerv != "0") return false;

            // IBAN numarası yalnızca harf ve rakam olabilir 
            if (!IsAlphaNumeric(strIBAN)) return false;

            // özel olan ilk 4 karakteri alalım 
            string temp1 = strIBAN.Substring(0, 4).Trim();

            // geri kalan karakterleri alalım 
            string temp2 = strIBAN.Substring(4).Trim();

            // ilk 4 karakteri sona atalım 
            string temp3 = temp2 + temp1;

            // harfleri sayı karşılıklarına çevirelim
            string temp4 = temp3;
            for (int i = 0; i < DonusturmeTablosu_String.Length; i++)
                temp4 = temp4.Replace(DonusturmeTablosu_String[i], DonusturmeTablosu_Number[i]);

            // sayıya çevirelim 
            decimal num = Convert.ToDecimal(temp4);

            // 97'ye göre mod alalım 
            decimal mod = num % 97;

            // eğer mod bölümünden kalan 1 ise uygun bir IBAN       
            // değilse uygun olmayan bir IBAN numarası demektir.    
            if (mod != 1) return false;

            return true;
        }


    }
}
