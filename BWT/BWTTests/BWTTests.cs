using NUnit.Framework;
using System;
using BurrowsWheelerTransform;
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
        (string encodedString, int positionOfStringEnd) = Transform.Encode(testString);

        // Act
        string decodedString = Transform.Decode(encodedString, positionOfStringEnd);

        // Assert
        Assert.That(testString, Is.EqualTo(decodedString), $"Failed for input string: {testString}");
    }

    /// <summary>
    /// Tests that the encoder returns the correct position of the last symbol of the original string.
    /// </summary>
    [Test]
    [TestCaseSource(nameof(testStrings))]
    public void Encoder_ShouldReturnCorrectPositionOfStringEnd(string testString)
    {
        // Arrange
        (string encodedString, int positionOfStringEnd) = Transform.Encode(testString);

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
        (string encodedString, int _) = Transform.Encode(testString);
        int invalidPosition = testString.Length; // Out of bounds

        // Act
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => Transform.Decode(encodedString, invalidPosition));
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
        Assert.Throws<ArgumentException>(() => Transform.Decode(emptyString, positionOfStringEnd));
    }
}
