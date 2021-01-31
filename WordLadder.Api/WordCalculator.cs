using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordLadder.Infrastructure.Exceptions;
using WordLadder.Infrastructure.Extensions;
using WordLadder.Infrastructure.Resources;

namespace WordLadder.Api
{
    public class WordCalculator : IWordCalculator<IWord>
    {
        private List<List<IWord>> allWordSteps;
        private List<IWord> allWords;
        private List<IWord> intermediateWords;

        public IEnumerable<IWord> CalculateShortestPath(IWord startWord, IWord endWord, int wordsLength, IEnumerable<IWord> allWordsInput)
        {
            var wordLadder = new List<IWord>() { startWord };
            allWordSteps = new List<List<IWord>>() { wordLadder };
            allWords = allWordsInput.DistinctBy(x => x.Text).Select(y => y).ToList();
            intermediateWords = new List<IWord>();

            bool stopWhile = false;

            if (!startWord.IsValidLength(wordsLength))
                throw new BusinessException(string.Format(ValidationMessages.StartWordNotSameLength, startWord.Text, startWord.Text.Length, wordsLength), ExceptionLevel.Error);

            if (!endWord.IsValidLength(wordsLength))
                throw new BusinessException(string.Format(ValidationMessages.EndWordNotSameLength, endWord.Text, endWord.Text.Length, wordsLength), ExceptionLevel.Error);

            if (!startWord.HasSameLength(endWord))
                throw new BusinessException(string.Format(ValidationMessages.StartAndEndWordNotSameLength, startWord.Text, startWord.Text.Length, endWord.Text, endWord.Text.Length), ExceptionLevel.Error);

            if (!startWord.IsOnWordsList(allWords))
                throw new BusinessException(string.Format(ValidationMessages.StartWordIsNotOnList, startWord.Text), ExceptionLevel.Error);

            if (!endWord.IsOnWordsList(allWords))
                throw new BusinessException(string.Format(ValidationMessages.EndWordIsNotOnList, endWord.Text), ExceptionLevel.Error);

            // Loop through the words steps until it's finished and the list returned is filled with all the words for the shortest path
            do
            {
                wordLadder = IterateWordSteps(endWord, wordLadder, out stopWhile).ToList();
            }
            while (wordLadder.Count() == 0 && stopWhile == false);

            return wordLadder;
        }

        public IEnumerable<IWord> IterateWordSteps(IWord endWord, IEnumerable<IWord> words, out bool stopWhile)
        {
            List<IWord> finalWords = new List<IWord>();

            List<List<IWord>> allWordStepsCopy = new List<List<IWord>>(allWordSteps.Select(x => x)).ToList();
            stopWhile = false;
            int i = 0;
            bool isCalculationNotCompleted = false;
            bool isCalculationFinished = false;

            try
            {
                allWordSteps.Clear();

                // If there is no more steps to iterate, set the stopWhile to true so it stops the iteration in the caller
                if (words.Count() == 0 && allWordStepsCopy.Count() == 0)
                    stopWhile = true;

                // Multiple Threads used to make the iteration faster using multiple processes and to be ready if much more words steps are needed
                Parallel.ForEach(allWordStepsCopy, (wordSteps, state) =>
                {
                    i++;
                    // Get All the words with only one Different letter and was not added to the list already
                    var adjacent = allWords.Where(
                            x => x.IsOneLetterDifferent(x, wordSteps.Last()) &&
                                 !wordSteps.Any(y => y.Text.Equals(x.Text)));

                    // Check if the list contains the end word and then add it to the end of the list to be returned and it means the end of the search
                    if (adjacent.Any(x => x.Text.Equals(endWord.Text)))
                    {
                        wordSteps.Add(endWord);
                        finalWords = wordSteps;
                        isCalculationFinished = true;
                        state.Break();
                    }

                    if (!isCalculationFinished)
                    {
                        // Each word found is added to the list to be searched on the next iteration
                        Parallel.ForEach(adjacent, (word) =>
                        {
                            List<IWord> newWordStep = wordSteps.ToList();
                            newWordStep.Add(word);
                            allWordSteps.Add(newWordStep);
                        });
                    }

                    intermediateWords = wordSteps;
                });


                if (allWordStepsCopy.Count == 0)
                {
                    intermediateWords.Add(new Word(string.Format(ValidationMessages.NotEnoughWordsToTransform, intermediateWords.FirstOrDefault().Text, endWord.Text)));
                    intermediateWords.Add(endWord);
                    finalWords = intermediateWords;
                    isCalculationNotCompleted = true;
                }

                if (isCalculationNotCompleted)
                    stopWhile = isCalculationNotCompleted;
            }
            catch (Exception ex)
            {
                stopWhile = true;
                throw new Exception(string.Format("Exception Iterating the Words Steps during the Shortest Path Calculation using End Word: {0}. Exception Message: {1}", endWord, ex.Message));
            }

            return finalWords;
        }
    }
}
