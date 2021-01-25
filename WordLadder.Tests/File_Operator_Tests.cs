using WordLadder.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace WordLadder.Tests
{
    public class File_Operator_Tests : IDisposable
    {
        private readonly FileOperator _fileOperator;

        public File_Operator_Tests()
        {
            _fileOperator = new FileOperator();
        }

        [Fact(DisplayName = "TestWordsDictionaryExists")]
        public void File_WordsDictionaryExists()
        {
            var filePath = "words-english.txt";
            var fileExists = _fileOperator.FileExists(filePath);

            Assert.True(fileExists);
        }

        [Fact(DisplayName = "TestWordsDictionaryNotExists")]
        public void File_WordsDictionaryNotExists()
        {
            var filePath = "notexists.txt";
            var fileExists = _fileOperator.FileExists(filePath);

            Assert.False(fileExists);
        }

        [Fact(DisplayName = "TestWordsDictionaryLoadSuccess")]
        public void File_WordsDictionaryLoadSuccess()
        {
            var filePath = "words-english.txt";
            var wordsLength = 4;
            var wordsDictionary = _fileOperator.LoadWordsDictionary(filePath, wordsLength);

            Assert.True(wordsDictionary != null);
        }

        [Fact(DisplayName = "TestWordsDictionaryLoadFailure")]
        public void File_WordsDictionaryLoadFailure()
        {
            var filePath = "words.txt";
            var wordsLength = 4;

            Assert.Throws<Exception>(() => _fileOperator.LoadWordsDictionary(filePath, wordsLength));
        }

        [Fact(DisplayName = "TestWordsResultDictionaryExists")]
        public void File_WordsResultDictionaryExists()
        {
            var filePath = "test.txt";

            var wordsDictionary = new List<string>();

            File.WriteAllLines(filePath, wordsDictionary);

            var fileExists = _fileOperator.FileExists(filePath);

            Assert.True(fileExists);
        }

        [Fact(DisplayName = "TestWordsResultDictionaryNotExists")]
        public void File_WordsResultDictionaryNotExists()
        {
            var filePath = "notexists.txt";

            var wordsDictionary = new List<string>();

            File.WriteAllLines(filePath, wordsDictionary);

            var fileExists = _fileOperator.FileExists("test.txt");

            Assert.False(fileExists);
        }

        [Fact(DisplayName = "TestWordsResultDictionaryLoadSuccess")]
        public void File_WordsResultDictionaryLoadSuccess()
        {
            var filePath = "test.txt";
            var wordsDictionary = new List<string>();

            File.WriteAllLines(filePath, wordsDictionary);

            wordsDictionary = _fileOperator.LoadResultsDictionary(filePath).ToList();

            Assert.True(wordsDictionary != null);
        }

        [Fact(DisplayName = "TestWordsResultDictionaryLoadFailure")]
        public void File_WordsResultDictionaryLoadFailure()
        {
            var filePath = "notexists.txt";
            var wordsDictionary = new List<string>();

            File.WriteAllLines(filePath, wordsDictionary);

            Assert.Throws<Exception>(() => _fileOperator.LoadResultsDictionary("test.txt"));
        }

        [Fact(DisplayName = "TestSaveResultFileSuccess")]
        public void File_SaveResultFileSuccess()
        {
            var filePath = "test.txt";
            var wordsDictionary = new List<string>();

            wordsDictionary.Add("math");
            wordsDictionary.Add("mach");
            wordsDictionary.Add("mace");
            wordsDictionary.Add("mice");
            wordsDictionary.Add("nice");

            _fileOperator.SaveResultFile(filePath, wordsDictionary);

            var fileSavedSuccessfully = File.Exists(filePath);

            Assert.True(fileSavedSuccessfully);
        }

        [Fact(DisplayName = "TestSaveResultFileFailure")]
        public void File_SaveResultFileFailure()
        {
            var filePath = "../../xpto\\abs//test.txt";
            var wordsDictionary = new List<string>();

            wordsDictionary.Add("math");
            wordsDictionary.Add("mach");
            wordsDictionary.Add("mace");
            wordsDictionary.Add("mice");
            wordsDictionary.Add("nice");

            Assert.Throws<Exception>(() => _fileOperator.SaveResultFile(filePath, wordsDictionary));
        }

        public void Dispose()
        {
            File.Delete("test.txt");
            File.Delete("notexists.txt");
            File.Delete("words.txt");
        }
    }
}