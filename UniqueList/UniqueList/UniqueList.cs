using UniqueList.Exceptions.AlreadyExistException;

namespace Lists;

/// <summary>
/// Class that represents Unique List data structure.
/// </summary>
public class UniqueList : List
{

    /// <inheritdoc/>
    /// <exception cref="ElementAlreadyExistsException"></exception>
    public override void Add(int value)
    {
        if (Contains(value))
        {
            throw new ElementAlreadyExistsException("This element already exists!");
        }

        head = new Node(value, head);
    }
    public int Count
    {
        get
        {
            int count = 0;
            Node? current = head;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }
    }
    /// <inheritdoc/>
    public override void ReplaceElementByIndex(int value, int index)
    {
        if (head == null)
        {
            throw new ArgumentNullException("List is empty!");
        }

        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException("Index is out of range!");
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
}