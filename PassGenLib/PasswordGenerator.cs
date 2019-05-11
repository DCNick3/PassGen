using System;
using System.Security.Cryptography;
using System.Text;

namespace PassGenLib
{
    public class PasswordGenerator : IDisposable
    {
        private readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();
        private readonly CharacterRange _range;
        
        public PasswordGenerator(CharacterRange range)
        {
            _range = range;
        }

        public string MakePassword(int length)
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(NextCharacter());
            }
            
            return stringBuilder.ToString();
        }

        private char NextCharacter()
        {
            return _range.Characters[Math.Abs(NextInteger()) % _range.Characters.Count];
        }

        private int NextInteger()
        {
            var data = new byte[4];
            _rng.GetBytes(data);
            return BitConverter.ToInt32(data, 0);
        }

        public void Dispose()
        {
            _rng?.Dispose();
        }
    }
}