using System;
using System.Collections.Generic;
using System.IO;

namespace WordCounter
{
    class WC
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к входному текстовому файлу:");
            string inputFile = Console.ReadLine();

            Console.WriteLine("Введите путь к выходному текстовому файлу:");
            string outputFile = Console.ReadLine();

            WordCounter wordCounter = new WordCounter();
            wordCounter.CountWords(inputFile);
            wordCounter.SortByCount();
            wordCounter.SaveToFile(outputFile);

            Console.WriteLine($"Уникальные слова и количество их употреблений сохранены в файл: {outputFile}");
        }
    }

    class WordCounter
    {
        private Dictionary<string, int> wordCounts;

        public WordCounter()
        {
            wordCounts = new Dictionary<string, int>();
        }

        public void CountWords(string inputFile)
        {
            try
            {
                string[] lines = File.ReadAllLines(inputFile);

                foreach (string line in lines)
                {
                    string[] words = line.Split(' ');

                    foreach (string word in words)
                    {
                        string cleanedWord = CleanWord(word);

                        if (!string.IsNullOrEmpty(cleanedWord))
                        {
                            if (wordCounts.ContainsKey(cleanedWord))
                            {
                                wordCounts[cleanedWord]++;
                            }
                            else
                            {
                                wordCounts[cleanedWord] = 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
        }

        public void SortByCount()
        {
            List<KeyValuePair<string, int>> sortedWordCounts = new List<KeyValuePair<string, int>>(wordCounts);
            sortedWordCounts.Sort((x, y) => y.Value.CompareTo(x.Value));

            wordCounts.Clear();

            foreach (var kvp in sortedWordCounts)
            {
                wordCounts.Add(kvp.Key, kvp.Value);
            }
        }

        public void SaveToFile(string outputFile)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    foreach (var kvp in wordCounts)
                    {
                        writer.WriteLine($"{kvp.Key}: {kvp.Value}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении в файл: {ex.Message}");
            }
        }

        private string CleanWord(string word)
        {
            // Удаление знаков препинания и пробелов из слова
            string cleanedWord = new string(word.Trim().Where(c => !char.IsPunctuation(c)).ToArray());
            return cleanedWord.ToLower(); // Приведение слова к нижнему регистру для учета регистра
        }
    }
}