using NUnit.Framework;
namespace Trie.nUnitTests;

[TestFixture]
public class TrieTests
{
    [Test]
    public void StandartTest()
    {
        MyTrie.Trie trie = new();
        Assert.IsTrue(trie.Add("apple"));
        Assert.IsTrue(trie.Contains("apple"));
        Assert.IsFalse(trie.Contains("banana"));
        Assert.IsTrue(trie.Add("banana"));
        Assert.IsTrue(trie.Contains("banana"));
        int actual = trie.HowManyStartWithPrefix("a");
        Assert.That(actual, Is.EqualTo(1));
        actual = trie.HowManyStartWithPrefix("b");
        Assert.That(actual, Is.EqualTo(1));
        actual = trie.Size;
        Assert.That(actual, Is.EqualTo(2));
        Assert.IsTrue(trie.Remove("apple"));
        Assert.IsFalse(trie.Contains("apple"));
        Assert.IsFalse(trie.Remove("apple"));
        Assert.IsFalse(trie.Remove("cat")); 
        actual = trie.Size;
        Assert.That(actual, Is.EqualTo(1));
    }

    [Test]
    public void EmptyTest()
    {
        MyTrie.Trie trie = new();
        Assert.IsFalse(trie.Contains("something"));
        Assert.IsFalse(trie.Contains("a"));
        int actual = trie.HowManyStartWithPrefix("a");
        Assert.That(actual, Is.EqualTo(0));
        actual = trie.Size;
        Assert.That(actual, Is.EqualTo(0));
    }

    [Test]
    public void LongTreeTest()
    {
        MyTrie.Trie trie = new();
        Assert.IsTrue(trie.Add("car"));
        Assert.IsTrue(trie.Add("carrier"));
        Assert.IsTrue(trie.Add("carriers"));
        int actual = trie.HowManyStartWithPrefix("car");
        Assert.That(actual, Is.EqualTo(3));
        actual = trie.HowManyStartWithPrefix("carr");
        Assert.That(actual, Is.EqualTo(2));
        actual = trie.Size;
        Assert.That(actual, Is.EqualTo(3));
        Assert.IsTrue(trie.Contains("carrier"));
        Assert.IsFalse(trie.Contains("carr"));
        Assert.IsTrue(trie.Add(""));
    }
}