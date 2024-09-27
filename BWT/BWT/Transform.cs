namespace BurrowsWheelerTransform;

using System;
using System.Text;
using Rotation;

public class Transform
{
    const int AlphabetSize = 65536;

    /// <summary>
    /// Encodes the original string using the Burrows-Wheeler Transform (BWT) algorithm.
    /// </summary>
    /// <param name="encodingString">The original string to encode.</param>
    /// <returns>A tuple containing the BWT-encoded string and the position of the last original string's symbol.</returns>
    public static (string, int) Encode(string encodingString)
    {
        if (string.IsNullOrEmpty(encodingString))
        {
            throw new ArgumentException("Input string cannot be null or empty.");
        }
        var rotations = new int[encodingString.Length];

        for (var i = 0; i < rotations.Length; ++i)
        {
            rotations[i] = i;
        }

        Array.Sort(rotations, new RotationsComparer(encodingString));

        var result = new StringBuilder();
        int positionOfStringEnd = 0;

        for (var i = 0; i < encodingString.Length; ++i)
        {
            result.Append(encodingString[(rotations[i] + encodingString.Length - 1) % encodingString.Length]);

            if (rotations[i] == 0)
            {
                positionOfStringEnd = i;
            }
        }

        return (result.ToString(), positionOfStringEnd);
    }

    /// <summary>
    /// Decodes the BWT-encoded string using the Burrows-Wheeler Transform (BWT) algorithm.
    /// </summary>
    /// <param name="decodingString">The BWT-encoded string.</param>
    /// <param name="positionOfStringEnd">The position of the last original string's symbol in the encoded string.</param>
    /// <returns>The decoded original string.</returns>
    public static string Decode(string decodingString, int positionOfStringEnd)
    {
        if (string.IsNullOrEmpty(decodingString))
        {
            throw new ArgumentException("Input string cannot be null or empty.");
        }
        if (positionOfStringEnd < 0 || positionOfStringEnd >= decodingString.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(positionOfStringEnd), "The position of the end of the original string must be within the range of the encoded string.");
        }

        var decodingStringSymbolFrequences = new int[AlphabetSize];
        for (var i = 0; i < decodingString.Length; ++i)
        {
            ++decodingStringSymbolFrequences[decodingString[i]];
        }

        int temporarySum = 0;
        for (var i = 0; i < AlphabetSize; ++i)
        {
            temporarySum += decodingStringSymbolFrequences[i];
            decodingStringSymbolFrequences[i] = temporarySum - decodingStringSymbolFrequences[i];
        }

        var nextSymbols = new int[decodingString.Length];
        for (var i = 0; i < decodingString.Length; ++i)
        {
            nextSymbols[decodingStringSymbolFrequences[decodingString[i]]] = i;
            ++decodingStringSymbolFrequences[decodingString[i]];
        }

        int nextSymbol = nextSymbols[positionOfStringEnd];
        var resultString = new StringBuilder();
        for (var i = 0; i < decodingString.Length; ++i)
        {
            resultString.Append(decodingString[nextSymbol]);
            nextSymbol = nextSymbols[nextSymbol];
        }

        return resultString.ToString();
    }
}