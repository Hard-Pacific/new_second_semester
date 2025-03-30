namespace ParseTree;

/// <summary>
/// Class for multiplication operator.
/// </summary>
public class Multiplication : Operator
{
    /// <summary>
    /// Calculates the value of this subtree - multiplication of left and right children.
    /// </summary>
    public override double Calculate()
        => LeftChild!.Calculate() * RightChild!.Calculate();

    public override void Print()
    {
        Console.Write("( * ");
        base.Print();
    }
}