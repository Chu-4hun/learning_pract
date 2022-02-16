using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace learning_pract.Models
{
    public class Crypt
    {
        public static string Encrypt (string password)
        {
            string cryptPass = String.Empty;

            foreach (char c in password)
            {
                int cp = Convert.ToInt32 (c);
                cp++;
                cryptPass += Convert.ToChar (cp);
            }

            return cryptPass;
        }
        public static string Decrypt (string password)
        {
            string decryptPass = String.Empty;

            foreach (char c in password)
            {
                int cp = Convert.ToInt32 (c);
                cp--;
                decryptPass += Convert.ToChar (cp);
            }

            return decryptPass;
        }
    }
}