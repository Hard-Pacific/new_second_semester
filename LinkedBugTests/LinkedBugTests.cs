namespace DataStructures.Tests;

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using DataStructure;

[TestFixture]
public class LinkedBagTests
{
    private LinkedBag<int> _bag;

    [SetUp]
    public void Setup()
    {
        _bag = new LinkedBag<int>();
    }

    [Test]
    public void Add_AddsElementToBag()
    {
        _bag.Add(5);
        Assert.That(1, Is.EqualTo(_bag.Count));
        Assert.That(_bag.Contains(5), Is.True);
    }

    [Test]
    public void Clear_RemovesAllElementsFromBag()
    {
        _bag.Add(1);
        _bag.Add(2);
        _bag.Clear();
        Assert.That(0, Is.EqualTo(_bag.Count));
        Assert.That(_bag.Contains(1), Is.False);
        Assert.That(_bag.Contains(2), Is.False);
    }

    [Test]
    public void Contains_ReturnsTrueIfElementExists()
    {
        _bag.Add(10);
        Assert.That(_bag.Contains(10), Is.True);
    }

    [Test]
    public void Contains_ReturnsFalseIfElementDoesNotExist()
    {
        Assert.That(_bag.Contains(10), Is.False);
    }

    [Test]
    public void CopyTo_CopiesElementsToArray()
    {
        _bag.Add(1);
        _bag.Add(2);
        _bag.Add(3);
        int[] array = new int[3];
        _bag.CopyTo(array, 0);
        Assert.That(array, Is.EqualTo(new int[] { 1, 2, 3 }));
    }

    [Test]
    public void CopyTo_ThrowsExceptionWhenArrayIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _bag.CopyTo(null, 0));
    }

    [Test]
    public void CopyTo_ThrowsExceptionWhenArrayIndexIsInvalid()
    {
        _bag.Add(1);  // Add elements to the bag to trigger the exception
        _bag.Add(2);
        Assert.Throws<ArgumentOutOfRangeException>(() => _bag.CopyTo(new int[3], -1));
        Assert.Throws<ArgumentException>(() => _bag.CopyTo(new int[1], 0));
    }


    [Test]
    public void Remove_RemovesExistingElement()
    {
        _bag.Add(5);
        bool removed = _bag.Remove(5);
        Assert.That(removed, Is.True);
        Assert.That(0, Is.EqualTo(_bag.Count));
        Assert.That(_bag.Contains(5), Is.False);
    }

    [Test]
    public void Remove_RemovesOnlyOneInstanceOfDuplicateElement()
    {
        _bag.Add(5);
        _bag.Add(5);
        bool removed = _bag.Remove(5);
        Assert.That(removed, Is.True);
        Assert.That(1, Is.EqualTo(_bag.Count));
    }

    [Test]
    public void Remove_ReturnsFalseIfElementDoesNotExist()
    {
        bool removed = _bag.Remove(5);
        Assert.That(removed, Is.False);
        Assert.That(0, Is.EqualTo(_bag.Count));
    }

    [Test]
    public void GetEnumerator_EnumeratesElementsInInsertionOrder()
    {
        _bag.Add(1);
        _bag.Add(2);
        _bag.Add(3);
        List<int> list = new List<int>();
        foreach (int item in _bag)
        {
            list.Add(item);
        }
        Assert.That(new List<int> { 1, 2, 3 }, Is.EqualTo(list));
    }

    [Test]
    public void IsReadOnly_ReturnsFalse()
    {
        Assert.That(_bag.IsReadOnly, Is.False);
    }

    [Test]
    public void Remove_HeadElement()
    {
        _bag.Add(1);
        _bag.Add(2);
        _bag.Remove(1);
        Assert.That(1, Is.EqualTo(_bag.Count));
        Assert.That(_bag.Contains(1), Is.False);
        Assert.That(_bag.Contains(2), Is.True);
    }

    [Test]
    public void Add_MultipleElementsAndCheckCount()
    {
        _bag.Add(1);
        _bag.Add(2);
        _bag.Add(3);
        Assert.That(_bag.Count, Is.EqualTo(3));
    }

    [Test]
    public void Clear_EmptyBagAfterClear()
    {
        _bag.Add(1);
        _bag.Clear();
        Assert.That(_bag.Count, Is.EqualTo(0));
        Assert.That(_bag.Any(), Is.False);
    }

    [Test]
    public void CopyTo_LargeArray()
    {
        _bag.Add(1);
        int[] array = new int[10];
        _bag.CopyTo(array, 5);
        Assert.That(1, Is.EqualTo(array[5]));
    }

    [Test]
    public void Remove_EmptyBag_ReturnsFalse()
    {
        Assert.That(_bag.Remove(5), Is.False);
    }
}

