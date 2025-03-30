namespace ParseTree;

/// <summary>
/// Class that represents operand-subtree.
/// </summary>
public class Operand : ITree
{
    /// <summary>
    /// Value of operand.
    /// </summary>
    private int Value;

    public Operand(int value) => Value = value;

    /// <summary>
    /// Returns value of operand.
    /// </summary>
    public double Calculate() => Value;

    /// <summary>
    /// Prints value of operand.
    /// </summary>
    public void Print() => Console.Write($"{Value} ");
}