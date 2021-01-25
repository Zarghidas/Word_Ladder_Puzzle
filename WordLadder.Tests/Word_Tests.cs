using WordLadder.Api;
using System;
using System.Collections.Generic;
using Xunit;

namespace WordLadder.Tests
{
    public class Word_Tests : IDisposable
    {
        Word startWord;
        Word endWord;

        [Fact(DisplayName = "TestIfWordIsBeingSetAndGetCorrectly")]
        public void Word_IsBeingSetAndGetCorrectly()
        {
            string testWord;

            startWord = new Word("math");
            testWord = startWord.Text;

            Assert.Equal("math", testWord);
        }

        [Fact(DisplayName = "TestIfWordIsAllLetter")]
        public void Word_IsAllLetter()
        {
            startWord = new Word("math");
            Assert.True(startWord.IsValidWord());
        }

        [Fact(DisplayName = "TestIfWordIsNotAllLetter")]
        public void Word_IsNotAllLetter()
        {
            startWord = new Word("m4th");

            Assert.False(startWord.IsValidWord());
        }

        [Fact(DisplayName = "TestIfWordLengthIsCorrect")]
        public void Word_LengthIsCorrect()
        {
            var length = 4;
            startWord = new Word("math");

            Assert.True(startWord.IsValidLength(length));
        }

        [Fact(DisplayName = "TestIfWordLengthIsNotCorrect")]
        public void Word_LengthIsNotCorrect()
        {
            var length = 4;
            startWord = new Word("mathx");

            Assert.False(startWord.IsValidLength(length));
        }

        [Fact(DisplayName = "TestIfStartWordAndEndWordHasTheSameLength")]
        public void Word_StartWordAndEndWordHasTheSameLength()
        {
            startWord = new Word("math");
            endWord = new Word("nice");

            Assert.True(startWord.HasSameLength(endWord));
        }

        [Fact(DisplayName = "TestIfWordIsEmpty")]
        public void Word_IsEmpty()
        {
            startWord = new Word(string.Empty);

            Assert.False(startWord.IsNotNullOrEmptyOrWhiteSpace());
        }

        [Fact(DisplayName = "TestIfWordIsNull")]
        public void Word_IsNull()
        {
            startWord = new Word(null);

            Assert.False(startWord.IsNotNullOrEmptyOrWhiteSpace());
        }

        [Fact(DisplayName = "TestIfWordIsWhiteSpace")]
        public void Word_IsWhiteSpace()
        {
            startWord = new Word(" ");

            Assert.False(startWord.IsNotNullOrEmptyOrWhiteSpace());
        }

        [Fact(DisplayName = "TestIfWordIsOnWordsDictionary")]
        public void Word_IsOnWordsDictionary()
        {
            var wordsList = new List<Word>();
            startWord = new Word("math");

            wordsList.Add(new Word("math"));
            wordsList.Add(new Word("mach"));
            wordsList.Add(new Word("mace"));
            wordsList.Add(new Word("mice"));
            wordsList.Add(new Word("nice"));

            Assert.True(startWord.IsOnWordsList(wordsList));
        }

        [Fact(DisplayName = "TestIfWordIsNotOnWordsDictionary")]
        public void Word_IsNotOnWordsDictionary()
        {
            var wordsList = new List<Word>();
            startWord = new Word("matt");

            wordsList.Add(new Word("math"));
            wordsList.Add(new Word("mach"));
            wordsList.Add(new Word("mace"));
            wordsList.Add(new Word("mice"));
            wordsList.Add(new Word("nice"));

            Assert.False(startWord.IsOnWordsList(wordsList));
        }

        [Fact(DisplayName = "TestIfWordIsOneLetterDifferent")]
        public void Word_IsOneLetterDifferent()
        {
            startWord = new Word("math");
            endWord = new Word("mach");

            Assert.True(startWord.IsOneLetterDifferent(startWord, endWord));
        }

        [Fact(DisplayName = "TestIfWordIsNotOnlyOneLetterDifferent")]
        public void Word_IsNotOnlyOneLetterDifferent()
        {
            startWord = new Word("math");
            endWord = new Word("make");

            Assert.False(startWord.IsOneLetterDifferent(startWord, endWord));
        }

        public void Dispose()
        {
        }
    }
}