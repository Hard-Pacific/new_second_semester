using NUnit.Framework;
using Huffman;
using System.Collections;
using HuffmanNode;

namespace HuffmanTests;

[TestFixture]
public class HuffmanTreeTests
{
    [Test]
    public void Build_EmptyString_ThrowsArgumentException()
    {
        // Arrange
        var tree = new HuffmanTree();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => tree.Build(""));
    }

    [Test]
    public void Build_ValidString_CreatesTree()
    {
        // Arrange
        var tree = new HuffmanTree();
        var source = "this is a test string";

        // Act
        tree.Build(source);

        // Assert
        Assert.IsNotNull(tree.Root);
        Assert.That(tree.Frequencies.Count, Is.GreaterThan(0));
        Assert.That(tree.nodes.Count, Is.GreaterThan(0));
    }

    [Test]
    public void Encode_ValidString_ReturnsBitArray()
    {
        // Arrange
        var tree = new HuffmanTree();
        var source = "hello world";
        tree.Build(source);

        // Act
        var encoded = tree.Encode(source);

        // Assert
        Assert.IsNotNull(encoded);
        Assert.That(encoded.Length, Is.GreaterThan(0));
    }

    [Test]
    public void Decode_EncodedBitArray_ReturnsOriginalString()
    {
        // Arrange
        var tree = new HuffmanTree();
        var source = "hello world";
        tree.Build(source);
        var encoded = tree.Encode(source);

        // Act
        var decoded = tree.Decode(encoded);

        // Assert
        Assert.AreEqual(source, decoded);
    }

    [Test]
    public void Encode_EmptyRoot_ThrowsInvalidOperationException()
    {
        // Arrange
        var tree = new HuffmanTree();
        var source = "hello world";

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => tree.Encode(source));
    }

    [Test]
    public void Decode_EmptyRoot_ThrowsInvalidOperationException()
    {
        // Arrange
        var tree = new HuffmanTree();
        var bits = new BitArray(new bool[] { true, false, true });

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => tree.Decode(bits));
    }
}


