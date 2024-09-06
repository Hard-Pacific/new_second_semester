using HuffmanCompressor;

if (args.Length != 2)
{
    Console.WriteLine(
        "Неверное количество аргументов. Используйте: " +
        "HuffmanCompressor.exe [имя_файла] [--compress | --uncompress | -c | -u]");
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
    Console.WriteLine("Неверный ключ операции. Используйте: --compress | --uncompress | -c | -u");
}
