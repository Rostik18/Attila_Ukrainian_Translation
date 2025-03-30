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


record FileLine(string Key, string Value);