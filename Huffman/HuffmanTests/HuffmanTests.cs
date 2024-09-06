using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HuffmanCompressor;

namespace HuffmanCompressor.Tests;

[TestFixture]
public class HuffmanCompressorTests
{
    // Test data
    private readonly byte[] _testData = new byte[] { 0, 1, 1, 2, 2, 2, 3, 3, 3, 3 };

    [Test]
    public void Compress_ShouldCreateZippedFileWithCorrectData()
    {
        // Create temp file
        string tempFile = Path.GetTempFileName();
        File.WriteAllBytes(tempFile, _testData);

        Huffman.Compress(tempFile);

        // Checking the existence of a compressed file
        string zippedFile = tempFile + ".zipped";
        Assert.IsTrue(File.Exists(zippedFile));

        // Reading data from a compressed file
        using (BinaryReader reader = new BinaryReader(File.Open(zippedFile, FileMode.Open)))
        {
            Dictionary<byte, string> codes = Huffman.ReadDictionary(reader);
            List<bool> encodedData = Huffman.ReadBoolArray(reader);

            // Encoding verification
            List<byte> decodedData = DecodeData(codes, encodedData);
            Assert.AreEqual(_testData, decodedData.ToArray());
        }

        // Deleting temporary files
        File.Delete(tempFile);
        File.Delete(zippedFile);
    }

    [Test]
    public void Uncompress_ShouldCreateUnzippedFileWithCorrectData()
    {
        string tempFile = Path.GetTempFileName();
        File.WriteAllBytes(tempFile, _testData);

        Huffman.Compress(tempFile);
        Huffman.Uncompress(tempFile + ".zipped");

        string unzippedFile = tempFile;
        Assert.IsTrue(File.Exists(unzippedFile));

        // Verifying correct unzipping
        byte[] unzippedData = File.ReadAllBytes(unzippedFile);
        Assert.AreEqual(_testData, unzippedData);

        File.Delete(tempFile);
        File.Delete(tempFile + ".zipped");
    }

    // Helper method for decoding data
    private List<byte> DecodeData(Dictionary<byte, string> codes, List<bool> encodedData)
    {
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
        return decodedData;
    }
}
