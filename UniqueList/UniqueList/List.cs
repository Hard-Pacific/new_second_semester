using UniqueList.Exceptions.DoesntExistException;

namespace Lists;

/// <summary>
/// Class that represents List data structure.
/// </summary>
public class List
{
    /// <summary>
    /// Head of the list.
    /// </summary>
    protected Node? head;

    /// <summary>
    /// Class that represents separate element of the list - node.
    /// </summary>
    protected class Node
    {
        private int _value;
        private Node? _next;
        /// <summary>
        /// Value of the node.
        /// </summary>

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Next node for this node.
        /// </summary>
        public Node? Next
        {
            get { return _next; }
            set { _next = value; }
        }

        public Node(int value, Node? next)
        {
            this.Value = value;
            this.Next = next;
        }
    }
    public bool Contains(int value)
    {
        Node? currentNode = head;
        while (currentNode != null)
        {
            if (currentNode.Value == value)
            {
                return true;
            }
            currentNode = currentNode.Next;
        }

        return false;
    }

    /// <summary>
    /// Adds the element to the top of the list.
    /// </summary>
    /// <param name="value">Value of the element.</param>
    public virtual void Add(int value)
    {
        head = new Node(value, head);
    }

    /// <summary>
    /// Removes the element from the list.
    /// </summary>
    /// <param name="value">Value of removing element.</param>
    /// <exception cref="ElementDoesntExistException"></exception>
    public void Remove(int value)
    {
        if (head == null)
        {
            throw new ArgumentNullException("List is empty!");
        }

        Node currentNode = head;
        Node? previousNode = null;
        while (currentNode.Value != value && currentNode.Next != null)
        {
            previousNode = currentNode;
            currentNode = currentNode.Next;
        }

        if (currentNode.Value != value)
        {
            throw new ElementDoesntExistException("There is no such element in the list!");
        }

        if (previousNode == null)
        {
            head = currentNode.Next;
        }
        else
        {
            previousNode.Next = currentNode.Next;
        }
    }

    /// <summary>
    /// Replaces an element with a new one by index.
    /// </summary>
    /// <param name="value">Value of the new element.</param>
    /// <param name="index">Index of the element that will be replaced.</param>
    public virtual void ReplaceElementByIndex(int value, int index)
    {
        if (head == null)
        {
            throw new ArgumentNullException("List is empty!");
        }

        Node? currentNode = head;
        var currentIndex = 0;

        while (currentIndex != index && currentNode != null)
        {
            currentNode = currentNode.Next;
            ++currentIndex;
        }
        if (currentNode == null)
        {
            throw new System.IndexOutOfRangeException("There is no element with such index in the list!");
        }
        currentNode.Value = value;
    }

    /// <summary>
    /// Prints list.
    /// </summary>
    public void Print()
    {
        if (head == null)
        {
            throw new ArgumentNullException("List is empty!");
        }

        System.Console.Write("List: ");

        Node? currentNode = head;
        while (currentNode != null)
        {
            System.Console.Write($"{currentNode.Value} ");
            currentNode = currentNode.Next;
        }
        System.Console.WriteLine();
    }

    /// <summary>
    /// Checks if list is empty.
    /// </summary>
    /// <returns>true - if list is empty, false - if list is not empty.</returns>
    public bool IsEmpty { get { return head == null; } }
}