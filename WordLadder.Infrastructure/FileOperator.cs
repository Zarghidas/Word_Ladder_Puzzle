using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordLadder.Infrastructure
{
    public class FileOperator : IFileOperator
    {
        public IEnumerable<string> wordSet { get; set; }

        public IEnumerable<string> LoadWordsDictionary(string filePath, int wordsLength)
        {
            try
            {
                wordSet = File.ReadAllLines(filePath).ToList();
                
                return wordSet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Exception reading the start list of words from File Path: {0}, with Words Length: {1}. Exception Message: {2}", filePath, wordsLength, ex.Message));
            }
        }

        public IEnumerable<string> LoadResultsDictionary(string filePath)
        {
            try
            {
                wordSet = File.ReadAllLines(filePath).ToList();

                return wordSet;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Exception reading the Result list of words from File Path: {0}. Exception Message: {1}", filePath, ex.Message));
            }
        }

        public void SaveResultFile(string filePath, IEnumerable<string> resultList)
        {
            try
            {
                File.WriteAllLines(filePath, resultList);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Exception saving the Result list of words from File Path: {0}. Exception Message: {1}", filePath, ex.Message));
            }
        }

        public bool FileExists(string filePath)
        {
            try
            {
                return File.Exists(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Exception checking the file existance on Path: {0}. Exception Message: {1}", filePath, ex.Message));
            }
        }
    }
}
