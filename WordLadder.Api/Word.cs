using System;
using System.Collections.Generic;
using System.Linq;

namespace WordLadder.Api
{
    public class Word : IWord
    {
        public string Text { get; private set; }

        public Word(string value)
        {
            Text = value;
        }

        public bool IsValidWord()
        {
            return Text.All(Char.IsLetter);
        }

        public bool IsValidLength(int length)
        {
            return Text.Length == length;
        }

        public bool HasSameLength(IWord secondWord)
        {
            return Text.Length == secondWord.Text.Length;
        }

        public bool IsNotNullOrEmptyOrWhiteSpace()
        {
            return !string.IsNullOrWhiteSpace(Text) &&
                Text != string.Empty;
        }

        public bool IsOnWordsList(IEnumerable<IWord> wordsList)
        {
            return wordsList.Any(x => x.Text.ToLower().Equals(this.Text.ToLower()));
        }

        public bool IsOneLetterDifferent(IWord first, IWord second)
        {
            int differences = 0;
            if (first.Text.Length == second.Text.Length)
            {
                for (int i = 0; i < first.Text.Length; i++)
                {
                    if (first.Text[i] != second.Text[i])
                    {
                        differences++;
                    }
                }
            }

            return differences == 1;
        }
    }
}
