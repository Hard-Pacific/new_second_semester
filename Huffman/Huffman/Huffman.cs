using System.Collections;
using HuffmanNode;

namespace Huffman;

/// <summary>
/// A class for representing the Huffman tree and encoding and decoding operations.
/// </summary>
public class HuffmanTree
{
    public List<Node> nodes = new();
    /// <summary>
    /// The root of the Huffman tree.
    /// </summary>
    public Node? Root { get; set; }
    /// <summary>
    /// The frequency of characters in the source string.
    /// </summary>
    public Dictionary<char, int> Frequencies = new();
    /// <summary>
    /// Building a Huffman tree based on the source string.
    /// </summary>
    /// <param name="source">The source string for building the tree.</param>
    public void Build(string source)
    {
        if (source == "")
        {
            throw new ArgumentException("string must be not empty");
        }
        // Calculate frequencies of each character
        foreach (char character in source)
        {
            if (!Frequencies.ContainsKey(character))
            {
                Frequencies.Add(character, 0);
            }

            Frequencies[character]++;
        }

        // Create initial nodes for each character
        foreach (KeyValuePair<char, int> symbol in Frequencies)
        {
            nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
        }

        // Build the tree by repeatedly combining nodes with the lowest frequencies
        while (nodes.Count > 1)
        {
            // Sort nodes in ascending order of frequency
            List<Node> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

            // Combine the two nodes with the lowest frequencies
            if (orderedNodes.Count >= 2)
            {
                List<Node> taken = orderedNodes.Take(2).ToList<Node>();
                Node parent = new Node()
                {
                    Symbol = '*', // Symbol for internal nodes (doesn't matter)
                    Frequency = taken[0].Frequency + taken[1].Frequency,
                    Left = taken[0],
                    Right = taken[1]
                };

                // Remove the combined nodes and add the parent node
                nodes.Remove(taken[0]);
                nodes.Remove(taken[1]);
                nodes.Add(parent);
            }

            // Set the root of the tree to the remaining node
            this.Root = nodes.FirstOrDefault();
        }
    }
    /// <summary>
    /// Encoding of the source string.
    /// </summary>
    /// <param name="source">The source string for encoding.</param>
    /// <returns>A bitmap of encoded data.</returns>
    public BitArray Encode(string source)
    {
        List<bool> encodedSource = new List<bool>();

        // Encode each character in the source string
        foreach (char character in source)
        {
            // Traverse the tree to find the code for the character
            List<bool>? encodedSymbol = this.Root?.Traverse(character, new List<bool>());

            // Throw an exception if the root node is null or the code is not found
            if (encodedSymbol == null)
            {
                throw new InvalidOperationException("Root node is null or character code not found");
            }
            // Add the code to the encoded source list
            encodedSource.AddRange(encodedSymbol);
        }
        // Create a BitArray from the encoded source list
        BitArray bits = new BitArray(encodedSource.ToArray());

        return bits;
    }
    /// <summary>
    /// Decoding a bit array.
    /// </summary>
    /// <param name="bits">Encoded data in the form of a bit array.</param>
    /// <returns>The decoded source string.</returns>
    public string Decode(BitArray bits)
    {
        Node current = this.Root ?? throw new InvalidOperationException("Root node is null");
        string decoded = "";

        foreach (bool bit in bits)
        {
            // Traverse the tree based on the current bit
            if (bit)
            {
                if (current.Right != null)
                {
                    current = current.Right;
                }
                else
                {
                    throw new InvalidOperationException("Invalid encoded data: Right child is null");
                }
            }
            else
            {
                if (current.Left != null)
                {
                    current = current.Left;
                }
                else
                {
                    throw new InvalidOperationException("Invalid encoded data: Left child is null");
                }
            }
            // If we reach a leaf node, append the character to the decoded
            // string and reset the current node to the root
            if (IsLeaf(current))
            {
                decoded += current.Symbol;
                current = this.Root ?? throw new InvalidOperationException("Root node is null");

            }
        }
        return decoded;
    }
    /// <summary>
    /// Checking whether a node is a leaf in the tree.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <returns>True if the node is a leaf, otherwise False.</returns>
    public bool IsLeaf(Node node) => (node.Left == null && node.Right == null);
}
