namespace DataStructure;

using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A bag (multiset) data structure implemented using a linked list.
/// </summary>
public class LinkedBag<T> : ICollection<T>
{
    /// <summary>
    /// A node in the linked list.
    /// </summary>
    private class Node
    {
        /// <summary>
        /// Gets or sets the data stored in the node.
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Gets or sets the next node in the linked list.
        /// </summary>
        public Node Next { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    /// <summary>
    /// The head of the linked list.
    /// </summary>
    private Node _head;
    /// <summary>
    /// The tail of the linked list.
    /// </summary>
    private Node _tail;
    /// <summary>
    /// The number of elements in the bag.
    /// </summary>
    private int _count;

    /// <summary>
    /// Initializes a new instance of the class.
    /// </summary>
    public LinkedBag()
    {
        _head = null;
        _tail = null;
        _count = 0;
    }

    /// <summary>
    /// Gets the number of elements contained in the LinkedBag.
    /// </summary>
    public int Count => _count;

    /// <summary>
    /// Gets a value indicating whether the LinkedBag is read-only.
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Adds an item to the LinkedBag
    /// </summary>
    public void Add(T item)
    {
        Node newNode = new Node(item);

        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail.Next = newNode;
            _tail = newNode;
        }
        _count++;
    }

    /// <summary>
    /// Removes all items from the LinkedBag.
    /// </summary>
    public void Clear()
    {
        _head = null;
        _tail = null;
        _count = 0;
    }

    /// <summary>
    /// Determines whether the LinkedBag contains a specific value.
    /// </summary>
    public bool Contains(T item)
    {
        Node current = _head;
        while (current != null)
        {
            if (Equals(current.Data, item))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    /// <summary>
    /// Copies the elements of the LinkedBag to an Array, starting at a particular array index.
    /// </summary>
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));
        if (arrayIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        if (array.Length - arrayIndex < _count)
            throw new ArgumentException("The number of elements in the source ICollection<T> is greater than the available space from arrayIndex to the end of the destination array.");


        Node current = _head;
        int i = arrayIndex;
        while (current != null)
        {
            array[i++] = current.Data;
            current = current.Next;
        }
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the LinkedBag.
    /// </summary>
    public bool Remove(T item)
    {
        Node current = _head;
        Node previous = null;

        while (current != null)
        {
            if (Equals(current.Data, item))
            {
                if (previous == null)
                {
                    _head = current.Next;
                    if (_head == null)
                    {
                        _tail = null;
                    }
                }
                else
                {
                    previous.Next = current.Next;
                    if (current == _tail)
                    {
                        _tail = previous;
                    }
                }

                _count--;
                return true;
            }

            previous = current;
            current = current.Next;
        }

        return false;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the LinkedBag.
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        Node current = _head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the LinkedBag.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
