using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PassGenLib;

namespace PassGenCli
{
    class Program
    {
        static void ShowHelp()
        {
            Console.WriteLine("Usage:\n        PassGenCli [-uUsSlLdD] [OPTIONS] password_length\n" +
                              "        PassGenCli [--help|-h]\n");
            Console.WriteLine("    Small letter means to use character class, BIG means not to use\n");
            Console.WriteLine("--lowercase | --no-lowercase\n         -l | -L           " + 
                              new string(CharacterRange.LowercaseEnglishLetters.Characters.ToArray()) + "\n");
            Console.WriteLine("--uppercase | --no-uppercase\n         -u | -U           " + 
                              new string(CharacterRange.UppercaseEnglishLetters.Characters.ToArray()) + "\n");
            Console.WriteLine("--digits | --no-digits\n      -d | -D              " + 
                              new string(CharacterRange.Digits.Characters.ToArray()) + "\n");
            Console.WriteLine("--special | --no-special\n       -s | -S             " + 
                              new string(CharacterRange.SpecialCharacters.Characters.ToArray()) + "\n");
        }
        
        static int Main(string[] args)
        {
            // Ugh, this is ugly. Should use some library for that.
            
            var flags = new List<string>();
            var arguments = new List<string>();

            foreach (var s in args)
            {
                if (s.StartsWith("--"))
                    flags.Add(s);
                else if (s.StartsWith("-"))
                    foreach (var flag in s.Substring(1))
                        flags.Add("-" + flag);
                else
                    arguments.Add(s);
            }

            if (flags.Contains("-h") || flags.Contains("--help"))
            {
                ShowHelp();
                return 0;
            }

            var useUppercaseFlag = flags.AsEnumerable().Reverse().FirstOrDefault(_ => _ == "-u" || _ == "-U" ||
                                                                                      _ == "--uppercase" || _ == "--no-uppercase");
            var useUppercase = useUppercaseFlag != "-U" && useUppercaseFlag != "--no-uppercase";
            flags.Remove("-u");
            flags.Remove("-U");
            flags.Remove("--uppercase");
            flags.Remove("--no-uppercase");
            
            
            var useSpecialCharactersFlag = flags.AsEnumerable().Reverse().FirstOrDefault(_ => _ == "-s" || _ == "-S" ||
                                                                                      _ == "--special" || _ == "--no-special");
            var useSpecialCharacters = useSpecialCharactersFlag == "-s" || useSpecialCharactersFlag == "--special";
            flags.Remove("-s");
            flags.Remove("-S");
            flags.Remove("--special");
            flags.Remove("--no-special");
            
            
            var useLowercaseFlag = flags.AsEnumerable().Reverse().FirstOrDefault(_ => _ == "-l" || _ == "-L" ||
                                                                                      _ == "--lowercase" || _ == "--no-lowercase");
            var useLowercase = useLowercaseFlag != "-L" && useLowercaseFlag != "--no-lowercase";
            flags.Remove("-l");
            flags.Remove("-L");
            flags.Remove("--lowercase");
            flags.Remove("--no-lowercase");
            
            
            var useDigitsFlag = flags.AsEnumerable().Reverse().FirstOrDefault(_ => _ == "-d" || _ == "-D" ||
                                                                                      _ == "--digits" || _ == "--no-digits");
            var useDigits = useDigitsFlag == "-D" || useDigitsFlag == "--digits";
            flags.Remove("-d");
            flags.Remove("-D");
            flags.Remove("--digits");
            flags.Remove("--no-digits");

            if (flags.Count > 0)
            {
                Console.WriteLine($"Unknown flags: {string.Join(", ", flags.ToArray())}");
                ShowHelp();
                return 1;
            }

            if (arguments.Count == 0)
            {
                Console.WriteLine("No password length provided");
                ShowHelp();
                return 1;
            }

            if (arguments.Count > 1)
            {
                Console.WriteLine("Too much arguments provided");
                ShowHelp();
                return 1;
            }

            if (!int.TryParse(arguments[0], out var length) || length < 0 || length > 512)
            {
                Console.WriteLine("Password length is invalid");
                return 1;
            }

            var range = CharacterRange.Empty;
            
            if (useLowercase)
                range += CharacterRange.LowercaseEnglishLetters;
            if (useUppercase)
                range += CharacterRange.UppercaseEnglishLetters;
            if (useSpecialCharacters)
                range += CharacterRange.SpecialCharacters;
            if (useDigits)
                range += CharacterRange.Digits;

            if (range.Characters.Count == 0)
            {
                Console.WriteLine("No password characters selected");
                return 1;
            }
            
            var pwgen = new PasswordGenerator(range);
            
            Console.WriteLine(pwgen.MakePassword(length));
            return 0;
        }
    }
}