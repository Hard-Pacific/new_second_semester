using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HTree;

namespace HuffmanCompressor;

public class Huffman
{
    public static void Compress(string filename)
    {
        byte[] fileData = File.ReadAllBytes(filename);

        // Counting byte frequency
        Dictionary<byte, int> frequencies = new Dictionary<byte, int>();
        foreach (byte b in fileData)
        {
            if (frequencies.ContainsKey(b))
            {
                frequencies[b]++;
            }
            else
            {
                frequencies.Add(b, 1);
            }
        }

        HuffmanTree tree = new HuffmanTree(frequencies);

        // Creating a coding table

        Dictionary<byte, string> codes = tree.GenerateCodes();

        // File encoding
        List<bool> encodedData = new List<bool>();
        foreach (byte b in fileData)
        {
            encodedData.AddRange(codes[b].Select(c => c == '1'));
        }

        // Writing compressed data to a file
        string outputFilename = filename + ".zipped";
        using (BinaryWriter writer = new BinaryWriter(File.Open(outputFilename, FileMode.Create)))
        {
            // Write a coding table
            WriteDictionary(writer, codes);

            // Writing encoded data
            WriteBoolArray(writer, encodedData.ToArray());
        }

        // Output of compression ratio
        double compressionRatio = (double)fileData.Length / encodedData.Count;
        Console.WriteLine($"Сжатый файл {outputFilename} создан. Коэффициент сжатия: {compressionRatio:F2}");
    }

    public static void Uncompress(string filename)
    {
        // Reading data from a compressed file
        using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
        {
            // Reading the coding table
            Dictionary<byte, string> codes = ReadDictionary(reader);

            // Reading encoded data
            List<bool> encodedData = ReadBoolArray(reader);

            // Data Decoding
            List<byte> decodedData = new List<byte>();
            string currentCode = "";
            foreach (bool bit in encodedData)
            {
                currentCode += bit ? "1" : "0";
                if (codes.ContainsValue(currentCode))
                {
                    decodedData.Add(codes.FirstOrDefault(x => x.Value == currentCode).Key);
                    currentCode = "";
                }
            }

            // Writing decoded data to a file
            string outputFilename = filename.Replace(".zipped", "");
            File.WriteAllBytes(outputFilename, decodedData.ToArray());
            Console.WriteLine($"Разархивированный файл {outputFilename} создан.");
        }
    }

    // Methods for writing and reading a coding table
    public static void WriteDictionary(BinaryWriter writer, Dictionary<byte, string> codes)
    {
        writer.Write(codes.Count);
        foreach (var code in codes)
        {
            writer.Write(code.Key);
            writer.Write(code.Value.Length);
            writer.Write(code.Value.ToCharArray());
        }
    }

    public static Dictionary<byte, string> ReadDictionary(BinaryReader reader)
    {
        int count = reader.ReadInt32();
        Dictionary<byte, string> codes = new Dictionary<byte, string>();
        for (int i = 0; i < count; i++)
        {
            byte key = reader.ReadByte();
            int length = reader.ReadInt32();

            char[] value = reader.ReadChars(length);
            codes.Add(key, new string(value));
        }
        return codes;
    }

    public static void WriteBoolArray(BinaryWriter writer, bool[] data)
    {
        writer.Write(data.Length);
        foreach (bool bit in data)
        {
            writer.Write(bit);
        }
    }

    public static List<bool> ReadBoolArray(BinaryReader reader)
    {
        int length = reader.ReadInt32();
        List<bool> data = new List<bool>();
        for (int i = 0; i < length; i++)
        {
            data.Add(reader.ReadBoolean());
        }
        return data;
    }
}
