if (args.Length != 2)
{
    Console.WriteLine("Usage: $0 \"input.m2ts\" \"output.m2ts\"");
    Environment.Exit(1);
}

var filePath = args[0];
if (!File.Exists(filePath))
{
    Console.WriteLine("File not found.");
    Environment.Exit(2);
}

var writeFilePath = args[1];
if (File.Exists(writeFilePath))
{
    Console.WriteLine("Output file already exists.");
    Environment.Exit(3);
}

using (var fr = File.OpenRead(filePath))
using (var fw = File.OpenWrite(writeFilePath))
{
    var buffer = new byte[1];
    do
    {
        if (fr.Read(buffer, 0, 1) != 1)
        {
            Console.WriteLine("File ended. No MPEG-2 TS header exists.");
            Environment.Exit(4);
        }
    }
    while (buffer[0] != 0x47);

    fr.Position--;
    fr.CopyTo(fw);
}

Console.WriteLine("Success");
