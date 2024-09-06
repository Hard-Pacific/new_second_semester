using HuffmanCompressor;
public class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine(
                "Invalid number of arguments. Use:" +
                "HuffmanCompressor.exe [filename] [--compress | --uncompress | -c | -u]");
        }
        string filename = args[0];
        string operation = args[1];

        if (operation == "--compress" || operation == "-c")
        {
            Huffman.Compress(filename);
        }
        else if (operation == "--uncompress" || operation == "-u")
        {
            Huffman.Uncompress(filename);
        }
        else
        {
            Console.WriteLine("Invalid operation key. Use: --compress | --uncompress | -c | -u");
        }

    }
}
