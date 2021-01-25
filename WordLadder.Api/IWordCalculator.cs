using System.Collections.Generic;

namespace WordLadder.Api
{
    public interface IWordCalculator<IWord>
    {
        /// <summary>
        /// Calculates the Shortest path from Start Word to the End Word using a dictionary as words list
        /// </summary>
        /// <param name="wordStart">Start Word</param>
        /// <param name="wordEnd">End Word</param>
        /// <param name="allWordsInput">List of Words from dictionary</param>
        /// <returns></returns>
        public IEnumerable<IWord> CalculateShortestPath(IWord startWord, IWord endWord, int wordsLength, IEnumerable<IWord> allWordsInput);

        /// <summary>
        /// Iterate through each step identifying the possibilities and separating the lists to be calculated
        /// </summary>
        /// <param name="endWord">End word to finish the iteration when it's reached</param>
        /// <param name="wordLadder"></param>
        /// <param name="stopWhile">Field used to identify when the list is not being filled anymore to leave the loop While. It does not need to be manually setted</param>
        /// <returns>Returs the List of Words found</returns>
        public IEnumerable<IWord> IterateWordSteps(IWord endWord, IEnumerable<IWord> wordLadder, out bool stopWhile);
    }
}
