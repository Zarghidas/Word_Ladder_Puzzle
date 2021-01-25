using System.Collections.Generic;

namespace WordLadder.Infrastructure
{
    public interface IFileOperator
    {
        /// <summary>
        /// List of Words
        /// </summary>
        public IEnumerable<string> wordSet { get; set; }

        /// <summary>
        /// Return a List of words generated before the calculation of shortest path between Start Word and End Word provided
        /// </summary>
        /// <param name="filePath">Name of the file to Load</param>
        /// <param name="wordsLength">Size of the words to be loaded</param>
        /// <returns></returns>
        public IEnumerable<string> LoadWordsDictionary(string filePath, int wordsLength);

        /// <summary>
        /// Return a List of words, generated after the calculation of shortest path between Start Word and End Word provided
        /// </summary>
        /// <param name="filePath">Name of the file to Load</param>
        /// <returns>List of Result Words</returns>
        public IEnumerable<string> LoadResultsDictionary(string filePath);


        /// <summary>
        /// Save the result of the words after shortest path calculated
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        /// <param name="resultList">List of the words to be saved</param>
        public void SaveResultFile(string filePath, IEnumerable<string> resultList);

        /// <summary>
        /// Check if the File path provided exists
        /// </summary>
        /// <param name="filePath">Path of the File</param>
        /// <returns></returns>
        public bool FileExists(string filePath);
    }
}
