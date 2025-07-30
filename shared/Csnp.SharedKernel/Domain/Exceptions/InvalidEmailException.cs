namespace Csnp.SharedKernel.Domain.Exceptions;

/// <summary>
/// Exception thrown when an email address is in an invalid format.
/// </summary>
public class InvalidEmailException : Exception
{
    #region -- Constructors --

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidEmailException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public InvalidEmailException(string message) : base(message)
    {
    }

    #endregion
}
