namespace BWT;

using System;
using System.Text;

/// <summary>
/// Compares rotations of a string based on lexicographical order.
/// Used for sorting rotations in BWT encoding.
/// </summary>
class RotationsComparer : IComparer<int>
{
    private string str;

    public RotationsComparer(string str)
    {
        this.str = str;
    }
    /// <summary>
    /// Compares two rotations of the string.
    /// </summary>
    /// <param name="first">The index of the first rotation.</param>
    /// <param name="second">The index of the second rotation.</param>
    /// <returns>1 if the first rotation is lexicographically greater than the second,
    /// -1 if the first rotation is lexicographically less than the second,
    /// 0 if the rotations are equal.</returns>
    public int Compare(int first, int second)
    {
        for (var i = 0; i < str.Length; ++i)
        {
            if (str[(first + i) % str.Length] > str[(second + i) % str.Length])
            {
                return 1;
            }
            else if (str[(first + i) % str.Length] < str[(second + i) % str.Length])
            {
                return -1;
            }
        }
        return 0;
    }
}

public class Program
{
    const int AlphabetSize = 65536;

    /// <summary>
    /// Encodes the original string using the Burrows-Wheeler Transform (BWT) algorithm.
    /// </summary>
    /// <param name="encodingString">The original string to encode.</param>
    /// <returns>A tuple containing the BWT-encoded string and the position of the last original string's symbol.</returns>

    public static (string, int) Encoder(string encodingString)
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
    public static string Decoder(string decodingString, int positionOfStringEnd)
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