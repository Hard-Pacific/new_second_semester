namespace Rotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Compares rotations of a string based on lexicographical order.
/// Used for sorting rotations in BWT encoding.
/// </summary>
class RotationsComparer : IComparer<int>
{
    private string word;

    public RotationsComparer(string word)
    {
        this.word = word;
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
        for (var i = 0; i < word.Length; ++i)
        {
            if (word[(first + i) % word.Length] > word[(second + i) % word.Length])
            {
                return 1;
            }
            else if (word[(first + i) % word.Length] < word[(second + i) % word.Length])
            {
                return -1;
            }
        }
        return 0;
    }
}
