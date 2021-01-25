using WordLadder.Api;
using WordLadder.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WordLadder.Tests
{
    public class Word_Calculator_Tests : IDisposable
    {
        private readonly WordCalculator _wordCalculator;
        Word startWord;
        Word endWord;
        int wordsLength;

        public Word_Calculator_Tests()
        {
            _wordCalculator = new WordCalculator();
        }

        [Fact(DisplayName = "TestIfWordsBeingCalculatedHasTheSameLength")]
        public void WordCalculator_WordsBeingCalculatedHasTheSameLength()
        {
            var wordsDictionary = new List<Word>();
            startWord = new Word("math");
            endWord = new Word("nice");
            wordsLength = 4;

            wordsDictionary.Add(new Word("math"));
            wordsDictionary.Add(new Word("mach"));
            wordsDictionary.Add(new Word("mace"));
            wordsDictionary.Add(new Word("mice"));
            wordsDictionary.Add(new Word("nice"));

            Assert.True(_wordCalculator.CalculateShortestPath(startWord, endWord, wordsLength, wordsDictionary).Count() == 5);
        }

        [Fact(DisplayName = "TestIfWordsBeingCalculatedHasNotTheSameLength")]
        public void WordCalculator_WordsBeingCalculatedHasNotTheSameLength()
        {
            var wordsDictionary = new List<Word>();
            startWord = new Word("math");
            endWord = new Word("nicet");
            wordsLength = 4;

            wordsDictionary.Add(new Word("math"));
            wordsDictionary.Add(new Word("mach"));
            wordsDictionary.Add(new Word("mace"));
            wordsDictionary.Add(new Word("mice"));
            wordsDictionary.Add(new Word("nice"));

            Assert.Throws<BusinessException>(() => _wordCalculator.CalculateShortestPath(startWord, endWord, wordsLength, wordsDictionary));
        }

        [Fact(DisplayName = "TestIfStartWordBeingCalculatedIsNotOnWordsDictionary")]
        public void WordCalculator_StartWordBeingCalculatedIsNotOnWordsDictionary()
        {
            var wordsDictionary = new List<Word>();
            startWord = new Word("math");
            endWord = new Word("nice");
            wordsLength = 4;

            wordsDictionary.Add(new Word("magh"));
            wordsDictionary.Add(new Word("mach"));
            wordsDictionary.Add(new Word("mace"));
            wordsDictionary.Add(new Word("mice"));
            wordsDictionary.Add(new Word("nice"));

            Assert.Throws<BusinessException>(() => _wordCalculator.CalculateShortestPath(startWord, endWord, wordsLength, wordsDictionary));
        }

        [Fact(DisplayName = "TestIfEndWordBeingCalculatedIsNotOnWordsDictionary")]
        public void WordCalculator_EndWordBeingCalculatedIsNotOnWordsDictionary()
        {
            var wordsDictionary = new List<Word>();
            startWord = new Word("math");
            endWord = new Word("nice");
            wordsLength = 4;

            wordsDictionary.Add(new Word("math"));
            wordsDictionary.Add(new Word("mach"));
            wordsDictionary.Add(new Word("mace"));
            wordsDictionary.Add(new Word("mice"));
            wordsDictionary.Add(new Word("vice"));

            Assert.Throws<BusinessException>(() => _wordCalculator.CalculateShortestPath(startWord, endWord, wordsLength, wordsDictionary));
        }

        [Fact(DisplayName = "TestIfThereWasNotEnoughWordsToReachFromStartWordToEndWord")]
        public void WordCalculator_WasNotEnoughWordsToReachFromStartWordToEndWord()
        {
            var wordsDictionary = new List<Word>();
            startWord = new Word("divert");
            endWord = new Word("extort");
            wordsLength = 6;

            wordsDictionary.Add(new Word("divert"));
            wordsDictionary.Add(new Word("digert"));
            wordsDictionary.Add(new Word("ditert"));
            wordsDictionary.Add(new Word("extort"));

            Assert.True(_wordCalculator.CalculateShortestPath(startWord, endWord, wordsLength, wordsDictionary).Where(x => x.Text.Contains("There was not enough words to transform from Start Word to the End Word to conclude the Word Ladder")).Count() > 0);
        }

        [Fact(DisplayName = "TestIfThereWasEnoughWordsToReachFromStartWordToEndWord")]
        public void WordCalculator_WasEnoughWordsToReachFromStartWordToEndWord()
        {
            var wordsDictionary = new List<Word>();
            startWord = new Word("math");
            endWord = new Word("nice");
            wordsLength = 4;

            wordsDictionary.Add(new Word("math"));
            wordsDictionary.Add(new Word("mach"));
            wordsDictionary.Add(new Word("mace"));
            wordsDictionary.Add(new Word("mice"));
            wordsDictionary.Add(new Word("nice"));

            Assert.True(_wordCalculator.CalculateShortestPath(startWord, endWord, wordsLength, wordsDictionary).Where(x => x.Text.Contains("There was not enough words to transform from Start Word to the End Word to conclude the Word Ladder")).Count() == 0);
        }

        public void Dispose()
        {

        }
    }
}
