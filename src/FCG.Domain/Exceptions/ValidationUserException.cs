namespace FCG.Domain.Exceptions
{
    public class ValidationUserException : Exception
    {
        public ValidationUserException(string message) : base(message) { }
    }
}
