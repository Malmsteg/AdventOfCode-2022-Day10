string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
string file = Path.Combine(currentDirectory, "../../../input.txt");
string path = Path.GetFullPath(file);
string[] text = File.ReadAllText(path).Split("\n");
string[] part2Array = new string[6];
for (int i = 0; i < 6; i++)
{
    part2Array[i] = new string('.', 40);
}

int maxCycles = 0;
List<int> instructions = new();
for (int i = 0; i < text.Length; i++)
{
    if (text[i].Split()[0].Equals("noop"))
    {
        instructions.Add(0);
        maxCycles++;
    }
    else
    {
        instructions.Add(Convert.ToInt32(text[i].Split()[1]));
        maxCycles += 2;
    }
}

int signalStrength = 1;
int signalSum = 0;
int count = 0;
Dictionary<int, int> result = new();
for (int cycle = 1; cycle < maxCycles; cycle++)
{
    int lineCount = (cycle - 1) / 40;
    int pixelPosition = (cycle - 1) % 40;
    if (Math.Abs(pixelPosition - signalStrength) == 1 || pixelPosition == signalStrength)
    {
        char[] ch = part2Array[lineCount].ToCharArray();
        ch[pixelPosition] = '#';
        part2Array[lineCount] = new string(ch);
    }
    if (cycle == 20 || ((cycle > 20) && ((cycle % 40) - 20 == 0)))
    {
        signalSum += signalStrength * cycle;
        result.Add(cycle, signalStrength * cycle);
    }
    if (instructions[count] != 0)
    {
        cycle++;
        lineCount = (cycle - 1) / 40;
        pixelPosition = (cycle - 1) % 40;
        if (Math.Abs(pixelPosition - signalStrength) == 1 || pixelPosition == signalStrength)
        {
            char[] ch = part2Array[lineCount].ToCharArray();
            ch[pixelPosition] = '#';
            part2Array[lineCount] = new string(ch);
        }
        if (cycle == 20 || ((cycle > 20) && ((cycle % 40) - 20 == 0)))
        {
            signalSum += signalStrength * cycle;
            result.Add(cycle, signalStrength * cycle);
        }
        signalStrength += instructions[count];
    }
    count++;
}
Console.WriteLine(signalStrength);
Console.WriteLine(signalSum);
foreach (var item in result)
{
    Console.WriteLine($"Cycle : {item.Key} has value {item.Value}");
}

Console.WriteLine("Part 2");
for (int i = 0; i < part2Array.Length; i++)
{
    Console.WriteLine(part2Array[i]);
}