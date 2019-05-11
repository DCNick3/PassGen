using System.Collections.Generic;
using System.Linq;

namespace PassGenLib
{
    public class CharacterRange
    {
        /// <summary>
        /// Creates a range from half interval [start;end)
        /// </summary>
        public CharacterRange(char start, char end)
            : this(Enumerable.Range(start, end - start).Select(_ => (char)_))
        {
        }
        
        /// <summary>
        /// Creates a range from half interval [start;end)
        /// </summary>
        public CharacterRange(int start, int end)
            : this(Enumerable.Range(start, end - start).Select(_ => (char)_))
        {
        }

        public CharacterRange(IEnumerable<char> characters)
        {
            Characters = characters.ToArray();
        }
        
        public IReadOnlyList<char> Characters { get; }

        public static CharacterRange operator +(CharacterRange a, CharacterRange b)
        {
            return new CharacterRange(a.Characters.Concat(b.Characters));
        }
        
        public static readonly CharacterRange Empty = new CharacterRange(new char[0]);
        public static readonly CharacterRange LowercaseEnglishLetters = new CharacterRange('a', 'z' + 1);
        public static readonly CharacterRange UppercaseEnglishLetters = new CharacterRange('A', 'Z' + 1);
        public static readonly CharacterRange Digits = new CharacterRange('0', '9' + 1);
        public static readonly CharacterRange Whitespace = new CharacterRange(" \t\n");
        public static readonly CharacterRange SpecialCharacters = 
            new CharacterRange("!\"#$%&'()*+`-./:;<=>?@[\\]^_`{|}~");
    }
}