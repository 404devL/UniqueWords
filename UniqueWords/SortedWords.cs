namespace UniqueWords;

public class SortedWords
{
    private readonly string _path;

    public SortedWords(string inputPath)
    {
        _path = inputPath;
    }

    public void CreateSortedFile()
    {

        Dictionary<string, int> wordCount = new Dictionary<string, int>();

        using (StreamReader reader = new StreamReader(_path))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] words = line.Split(' ', ',', '.', '!', '?', ':', ';', '-', '(', ')', '"', '\'', '\r', '\n');

                foreach (var word in words)
                {
                    if (!string.IsNullOrWhiteSpace(word))
                    {
                        string cleanedWord = word.ToLower().Trim();
                        if (wordCount.ContainsKey(cleanedWord))
                        {
                            wordCount[cleanedWord]++;
                        }
                        else
                        {
                            wordCount.Add(cleanedWord, 1);
                        }
                    }
                }
            }

            string outputPath = Path.Combine(Path.GetDirectoryName(_path), "wordcount.txt");

            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                foreach (var pair in wordCount.OrderByDescending(x => x.Value))
                {
                    writer.WriteLine("{0}\t\t{1}", pair.Key, pair.Value);
                }
            }

            Console.WriteLine($"Результат записан в файл: {outputPath}");
        }
    }
}
