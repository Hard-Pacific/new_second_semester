namespace UniqueList.Exceptions.DoesntExistException;
/// <summary>
/// Exception that is thrown when you want to interact with element that doesn't exist.
/// </summary>
[Serializable]
public class ElementDoesntExistException : Exception
{
    public ElementDoesntExistException() { }
    public ElementDoesntExistException(string message) : base(message) { }
    public ElementDoesntExistException(string message, Exception inner) : base(message, inner) { }
    protected ElementDoesntExistException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}