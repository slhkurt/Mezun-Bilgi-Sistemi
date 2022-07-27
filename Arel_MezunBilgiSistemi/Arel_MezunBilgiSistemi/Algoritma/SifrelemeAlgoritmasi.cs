
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class SifrelemeAgoritmasi
    {
        public static byte[] ByteDonustur(string deger)
        {

            UnicodeEncoding byteConverter = new UnicodeEncoding();
            return byteConverter.GetBytes(deger);

        }

        public static byte[] Byte8(string deger)
        {
            char[] arrayChar = deger.ToCharArray();
            byte[] arrayByte = new byte[arrayChar.Length];
            for (int i = 0; i < arrayByte.Length; i++)
            {
                arrayByte[i] = Convert.ToByte(arrayChar[i]);
            }
            return arrayByte;
        }
        public string Md5(string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException(@"Şifrelenecek veri yok");
            }
            else
            {
                MD5CryptoServiceProvider sifre = new MD5CryptoServiceProvider();
                byte[] sifrebytes = sifre.ComputeHash(Encoding.UTF8.GetBytes(strGiris));
                var sb = new StringBuilder();
                foreach (byte b in sifrebytes)
                    sb.Append(b.ToString("x2").ToLower());

                return sb.ToString();
            }
        }
        public string Sha1(string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException(@"Şifrelenecek veri yok.");
            }
            else
            {
                SHA1CryptoServiceProvider sifre = new SHA1CryptoServiceProvider();
                 byte[] sifrebytes = sifre.ComputeHash(Encoding.UTF8.GetBytes(strGiris));
                 var sb = new StringBuilder();
                 foreach (byte b in sifrebytes)
                     sb.Append(b.ToString("x2").ToLower());

                 return sb.ToString();
            }
        }
        public string Sha256(string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException(@"Şifrelenecek Veri Yok");
            }
            else
            {
                SHA256Managed sifre = new SHA256Managed();
                byte[] arySifre = ByteDonustur(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }
        public string Sha384(string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException(@"Şifrelenecek veri bulunamadı.");
            }
            else
            {
                SHA384Managed sifre = new SHA384Managed();
                byte[] arySifre = ByteDonustur(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }
        public string Sha512(string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
            {
                throw new ArgumentNullException(@"Şifrelenecek veri yok.");
            }
            else
            {
                SHA512Managed sifre = new SHA512Managed();
                byte[] arySifre = ByteDonustur(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }
    }
