using WordLadder.Api;
using WordLadder.DependencyInjection;
using WordLadder.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WordLadder
{
    class Program
    {
        public static IFileOperator fileLoader;
        public static IWordCalculator<IWord> wordCalculator;

        static void Main(string[] args)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();

                //Dependency Injection
                Bootstrapper.Initialize();
                fileLoader = InjectFactory.Resolve<IFileOperator>();
                wordCalculator = InjectFactory.Resolve<IWordCalculator<IWord>>();

                //Variables
                Word startWord;
                Word endWord;
                int wordsLength = 0;
                string dictionaryFile;
                string resultFileName;
                List<Word> result = new List<Word>();

                //Production
                Console.WriteLine(string.Format("Enter the Start Word: "));
                startWord = new Word(Console.ReadLine());
                Console.WriteLine(Environment.NewLine);

                Console.WriteLine(string.Format("Enter the End Word: "));
                endWord = new Word(Console.ReadLine());
                Console.WriteLine(Environment.NewLine);

                Console.WriteLine(string.Format("Enter the Words Length: "));
                wordsLength = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(Environment.NewLine);

                Console.WriteLine(string.Format("Enter the Dictionary File (ex: words-english.txt): "));
                dictionaryFile = Console.ReadLine();
                Console.WriteLine(Environment.NewLine);

                Console.WriteLine(string.Format("Enter the Answer File Name (ex: test.txt): "));
                resultFileName = Console.ReadLine();
                Console.WriteLine(Environment.NewLine);

                stopWatch.Start();

                result = ExecuteCalculationProcess(startWord,
                    endWord,
                    wordsLength,
                    dictionaryFile,
                    resultFileName).ToList();

                stopWatch.Stop();

                if (result.Count == 0)
                    Console.WriteLine("There were no words returned from Result File");

                // Write the results on prompt
                result.ForEach(x => Console.WriteLine(x.Text));

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(string.Format("The calculation is taking: {0} seconds ", Math.Round(stopWatch.Elapsed.TotalSeconds, 3)));
                Console.WriteLine(Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Press <Esc> to exit... ");
                while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            }
        }

        static IEnumerable<Word> ExecuteCalculationProcess(Word startWord, Word endWord, int wordsLength, string dictionaryFile, string resultFileName)
        {
            List<Word> wordsList = new List<Word>();
            List<Word> result = new List<Word>();

            if (fileLoader.FileExists(dictionaryFile))
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Loading Words Dictionary File...");
                // Load the Word List
                wordsList = fileLoader.LoadWordsDictionary(dictionaryFile, wordsLength).ToList()
                    .Select(x => new Word(x.ToLower()))
                    .Where(y => y.IsValidWord())
                    .Where(z => z.IsValidLength(wordsLength)).ToList();

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Loaded " + fileLoader.wordSet.Count() + " words");

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Calculating Shortest Path...");
                Console.WriteLine(Environment.NewLine);
                // Calculate the Shortest Path from Start Word up to End Word
                var calculator = wordCalculator.CalculateShortestPath(startWord, endWord, wordsLength, wordsList).Select(y => y.Text);

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Saving Results to Output File...");

                if (!string.IsNullOrWhiteSpace(resultFileName))
                {
                    // Save the Result to a file
                    fileLoader.SaveResultFile(resultFileName, calculator);
                    Console.WriteLine(Environment.NewLine);

                    Console.WriteLine("Loading Results from Output File...");
                    // It was not asked to be done but i added it anyway to show the results on screen
                    result = fileLoader.LoadResultsDictionary(resultFileName).Select(x => new Word(x.ToLower())).ToList();
                }
                else
                    Console.WriteLine("The Answer File name was not informed");
            }
            else
            {
                Console.WriteLine(string.Format("The Dictionary File does not exists in the path provided: {0})", dictionaryFile));
            }

            return result;
        }
    }
}
