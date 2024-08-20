using NUnit.Framework;
using System;
using BWT;
namespace BWTTests;

[TestFixture]
public class BWTEncoderDecoderTests
{
    // Test data for encoding and decoding
    private static readonly string[] testStrings = new string[]
    {
        "banana",
        "mississippi",
        "abracadabra",
        "hello world"
    };

    /// <summary>
    /// Tests the BWT encoder and decoder with a set of test strings.
    /// </summary>
    [Test]
    [TestCaseSource(nameof(testStrings))]
    public void EncodeDecode_ShouldReturnOriginalString(string testString)
    {
        // Arrange
        (string encodedString, int positionOfStringEnd) = Program.Encoder(testString);

        // Act
        string decodedString = Program.Decoder(encodedString, positionOfStringEnd);

        // Assert
        Assert.AreEqual(testString, decodedString, $"Failed for input string: {testString}");
    }

    /// <summary>
    /// Tests that the encoder returns the correct position of the last symbol of the original string.
    /// </summary>
    [Test]
    [TestCaseSource(nameof(testStrings))]
    public void Encoder_ShouldReturnCorrectPositionOfStringEnd(string testString)
    {
        // Arrange
        (string encodedString, int positionOfStringEnd) = Program.Encoder(testString);

        // Act
        // No action needed, the assertion uses the returned value.

        // Assert
        Assert.That(positionOfStringEnd, Is.GreaterThanOrEqualTo(0) & Is.LessThan(testString.Length), $"Failed for input string: {testString}");
    }

    /// <summary>
    /// Tests that the decoder handles invalid input position of the end of the original string.
    /// </summary>
    [Test]
    [TestCaseSource(nameof(testStrings))]
    public void Decoder_ShouldThrowExceptionForInvalidPositionOfStringEnd(string testString)
    {
        // Arrange
        (string encodedString, int _) = Program.Encoder(testString);
        int invalidPosition = testString.Length; // Out of bounds

        // Act
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => Program.Decoder(encodedString, invalidPosition));
    }

    /// <summary>
    /// Tests that the decoder handles an empty input string.
    /// </summary>
    [Test]
    public void Decoder_ShouldHandleEmptyInputString()
    {
        // Arrange
        string emptyString = "";
        int positionOfStringEnd = 0; // Valid position for an empty string

        // Añt
        Assert.Throws<ArgumentException>(() => Program.Decoder(emptyString, positionOfStringEnd));
    }
}
