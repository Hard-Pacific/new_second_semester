namespace ParseTree;

/// <summary>
/// Class for addition operator.
/// </summary>
public class Addition : Operator
{
    /// <summary>
    /// Calculates the value of this subtree - sum of left and right children.
    /// </summary>
    public override double Calculate() 
        => LeftChild!.Calculate() + RightChild!.Calculate();

    /// <summary>
    /// Prints "( + [LeftChild] [RightChild]".
    /// </summary>
    public override void Print()
    {
        Console.Write("( + ");
        base.Print();
    }
}