Console.OutputEncoding = System.Text.Encoding.UTF8;

//ProcessFile();

FindInAllFiles("шанс захисту від стрілків".ToLower(), []);


static void FindInAllFiles(string wordToFind, string[] filesToCheck)
{
    var basePath = "C:\\Users\\MSI VECTOR\\Desktop\\Attila_Ukrainian_Translation\\en_localisation_files";

    var filenames = Directory.GetFiles(basePath);

    filenames = filesToCheck is null || filesToCheck.Length == 0
        ? filenames
        : filenames.Where(x => filesToCheck.Any(xx => x.Contains(xx))).ToArray();

    foreach (var file in filenames)
    {
        var fileLines = File.ReadAllLines(file).Select(x=>x.ToLower());

        var susLines = fileLines.Where(x => x.Contains(wordToFind)).ToList();

        if (susLines.Count != 0)
        {
            Console.WriteLine($"File {file} has {wordToFind} word in following lines");

            foreach (var susLine in susLines)
            {
                Console.WriteLine("\t" + susLine);
            }

            Console.WriteLine();
        }
    }
}

static void ProcessFile()
{
    var basePath = "C:\\Users\\MSI VECTOR\\Desktop\\Attila_Ukrainian_Translation\\tools\\AttilaTranslationTool\\";

    var initLines = File.ReadAllLines(basePath + "eng_input.txt");

    var engLines = new List<FileLine>();

    foreach (var line in initLines)
    {
        var words = line.Split('\t');

        engLines.Add(new(words[0], words[1].Replace("\"", "")));
    }

    //rezLines = rezLines.OrderBy(x => x.Value).ToList();

    File.WriteAllLines(basePath + "eng_output.txt", engLines.Select(x => x.Value));

    var ukrLines = File.ReadAllLines(basePath + "ukr_input.txt");

    var outLines = new List<string>();

    for (int i = 0; i < engLines.Count; i++)
    {
        var value = i < ukrLines.Length ? ukrLines[i] : engLines[i].Value;

        var outLine = $"{engLines[i].Key}\t\"{value}\"\t\"true\"";

        outLines.Add(outLine);
    }

    File.WriteAllLines(basePath + "ukr_output.txt", outLines);


    //var outLines = new List<string>();

    //foreach (var line in rezLines)
    //{
    //    outLines.Add(line.Value);
    //}

    //File.WriteAllLines("C:\\Users\\r.baitsar\\Desktop\\CohUkrTranslationTool\\NamesTool\\names_output.txt", outLines);
}


record FileLine(string Key, string Value);