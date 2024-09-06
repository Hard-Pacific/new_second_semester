namespace HTree;
public class HuffmanTree
{
    private Node root;

    public HuffmanTree(Dictionary<byte, int> frequencies)
    {
        // Creating tree leaves
        List<Node> nodes = frequencies.Select(f => new Node(f.Key, f.Value)).ToList();

        // Creating a Huffman Tree
        while (nodes.Count > 1)
        {
            // Finding the two nodes with the lowest frequency
            nodes = nodes.OrderBy(n => n.Frequency).ToList();
            Node left = nodes[0];
            Node right = nodes[1];

            // Creating a new node with the sum of frequencies
            Node newNode = new Node(null, left.Frequency + right.Frequency);
            newNode.Left = left;
            newNode.Right = right;

            // Removing old nodes and adding a new one
            nodes.Remove(left);
            nodes.Remove(right);
            nodes.Add(newNode);
        }

        root = nodes[0];
    }

    // Generating a coding table
    public Dictionary<byte, string> GenerateCodes()
    {
        Dictionary<byte, string> codes = new Dictionary<byte, string>();
        GenerateCodes(root, "", codes);
        return codes;
    }

    private void GenerateCodes(Node node, string code, Dictionary<byte, string> codes)
    {
        if (node.Data != null)
        {
            codes.Add((byte)node.Data, code);
        }
        else
        {
            GenerateCodes(node.Left, code + "0", codes);
            GenerateCodes(node.Right, code + "1", codes);
        }
    }

    // Class to represent a tree node
    private class Node
    {
        public byte? Data;
        public int Frequency;
        public Node Left;
        public Node Right;

        public Node(byte? data, int frequency)
        {
            Data = data;
            Frequency = frequency;
        }
    }
}