using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace PasswordGenerator
{
    public static class PasswordGenerator
    {
        private const string Lower = "abcdefghijklmnopqrstuvwxyz";
        private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Digits = "0123456789";
        private const string Specials = "!@#$%^&*()-_=+[]{}|;:,.<>?";

        // Generates strong password
        public static string GeneratePassword(int len, bool hasDigits = true, bool hasSpecials = true) 
            => GeneratePassword(len, ComposeAlphabet(hasDigits, hasSpecials));
        public static string GeneratePassword(int len, string alphabet)
        {
            string password = "";

            while (!IsStrong(password, alphabet))
                password = Next(len, alphabet);

            return password;
        }

        // Create a candidate for password
        public static string Next() => Next(ComposeAlphabet());
        public static string Next(int len) => Next(len, ComposeAlphabet());
        public static string Next(string alphabet) => Next(new Random().Next(15, 30), alphabet);
        public static string Next(int len, string alphabet)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            string candidate = "";
            
            for (int i = 0; i < len; i++)
                candidate += alphabet[rand.Next(alphabet.Length)];

            return candidate;
        }

        // Check if password is strong enough
        private static bool IsStrong(string candidate, string alphabet)
        {
            if (candidate == null) return false;
            if (candidate.Length < 15 || alphabet.Length < 0) return false;

            int border = (int)(candidate.Length * 0.3);

            if (NumOfChars(candidate, Lower) == 0 ||
                NumOfChars(candidate, Upper) == 0)
                return true;

            if (alphabet.Contains(Digits[0]))
            {
                var digitNum = NumOfChars(candidate, Digits);
                if (digitNum == 0 || digitNum > border)
                    return true;
            }

            if (alphabet.Contains(Specials[0]))
            {
                var specNum = NumOfChars(candidate, Specials);
                if (specNum == 0 || specNum > border)
                    return true;
            }

            if (NumberOfReps(candidate, 3) > 0 ||
                NumberOfReps(candidate, 2) > 1)
                return true;

            return false;
        }

        // Check if sequence has size identical symbols in a row
        // Reps - length of subsequence to check
        private static int NumberOfReps(string password, int reps)
        {
            if (reps < 2) return 0;

            var counter = 0;

            for (int i = 0; i < password.Length; i++)
            {
                var allAreSame = true;

                for (int j = i + 1; j < i + reps && j < password.Length; j++) 
                    if (password[i] != password[j])
                    {
                        allAreSame = false;
                        break;
                    }
                if (allAreSame) counter++;
            }

            return counter;
        }

        // Return amount of elements in password
        private static int NumOfChars(string password, string elements) => elements.Count(password.Contains);

        // Create string of chosen characters
        public static string ComposeAlphabet(bool hasDigits = true, bool hasSpecials = true) =>
            Lower + Upper + (hasDigits ? Digits : "") + (hasSpecials ? Specials : "");
    }
}
