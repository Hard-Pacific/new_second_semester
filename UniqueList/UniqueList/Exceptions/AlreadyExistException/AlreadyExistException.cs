namespace UniqueList.Exceptions.AlreadyExistException;

/// <summary>
/// Exception that is thrown when you want to interact with element that already exist.
/// </summary>
[Serializable]
public class ElementAlreadyExistsException : Exception
{
    public ElementAlreadyExistsException() { }
    public ElementAlreadyExistsException(string message) : base(message) { }
    public ElementAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    protected ElementAlreadyExistsException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}