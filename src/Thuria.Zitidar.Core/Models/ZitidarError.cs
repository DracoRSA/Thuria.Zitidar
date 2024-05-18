using Thuria.Zitidar.Core.Result;

namespace Thuria.Zitidar.Core.Models;

/// <summary>
/// Zitidar Error
/// </summary>
public sealed class ZitidarError : IZitidarError
{
    /// <summary>
    /// Zitidar Error Constructor
    /// </summary>
    /// <param name="errorCode">Error Code</param>
    /// <param name="message">Error Message</param>
    /// <param name="exception">Exception details</param>
    public ZitidarError(int errorCode, string message, Exception? exception = null)
    {
        if (errorCode <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(errorCode));
        }

        ErrorCode = errorCode;
        Message   = message ?? throw new ArgumentNullException(nameof(message));
        Exception = exception;
    }

    /// <inheritdoc />
    public int ErrorCode { get; }

    /// <inheritdoc />
    public string Message { get; }

    /// <inheritdoc />
    public Exception? Exception { get; }
}