using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuffmanNode;

public class Node
{
    /// <summary>
    /// The symbol of the node of the Huffman tree.
    /// </summary>
    internal char Symbol { get; set; }

    /// <summary>
    /// The frequency of the node symbol.
    /// </summary>
    internal int Frequency { get; set; }

    /// <summary>
    /// The right branch of the node.
    /// </summary>
    internal Node? Right { get; set; }

    /// <summary>
    /// The left branch of the node.
    /// </summary>
    internal Node? Left { get; set; }

    /// <summary>
    /// A recursive method for traversing the Huffman tree and searching for a symbol.
    /// </summary>
    /// <param name="symbol">The desired symbol.</param>
    /// <param name="data">The path to the symbol (a list of Boolean values).</param>
    /// <returns>The path to the symbol in the form of a list of Boolean values.</returns>
    public List<bool>? Traverse(char symbol, List<bool> data)
    {
        // Leaf
        if (Right == null && Left == null)
        {
            return (symbol.Equals(this.Symbol)) ? data : null!;
        }
        else
        {
            List<bool>? left = null;
            List<bool>? right = null;

            if (Left != null)
            {
                List<bool> leftPath = new List<bool>();
                leftPath.AddRange(data);
                leftPath.Add(false);

                left = Left.Traverse(symbol, leftPath);
            }

            if (Right != null)
            {
                List<bool> rightPath = new List<bool>();
                rightPath.AddRange(data);
                rightPath.Add(true);
                right = Right.Traverse(symbol, rightPath);
            }

            if (left != null)
            {
                return left;
            }
            else
            {
                return right;
            }
        }
    }
}