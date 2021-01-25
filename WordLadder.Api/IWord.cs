using System.Collections.Generic;

namespace WordLadder.Api
{
    public interface IWord
    {
        public string Text { get; }

        /// <summary>
        /// Check if the Word contains only Letters
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsValidWord();

        /// <summary>
        /// Check if the Length of the Word is the same as set through command prompt
        /// </summary>
        /// <param name="length">size of the word as set on command prompt</param>
        /// <returns>true or false</returns>
        public bool IsValidLength(int length);

        /// <summary>
        /// Check if the Start Word and End Word has the same Length
        /// </summary>
        /// <param name="secondWord">The end word</param>
        /// <returns>true or false</returns>
        public bool HasSameLength(IWord secondWord);

        /// <summary>
        /// Check if the Word is not null, empty or whitespace
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsNotNullOrEmptyOrWhiteSpace();

        /// <summary>
        /// Check if the provided word is on the words dictionary
        /// </summary>
        /// <param name="wordsList">Words Dictionary provided</param>
        /// <returns></returns>
        public bool IsOnWordsList(IEnumerable<IWord> wordsList);

        /// <summary>
        /// Check if two words has only one different letter
        /// </summary>
        /// <param name="first">First word</param>
        /// <param name="second">Second Word</param>
        /// <returns>true or false</returns>
        public bool IsOneLetterDifferent(IWord first, IWord second);
    }
}
