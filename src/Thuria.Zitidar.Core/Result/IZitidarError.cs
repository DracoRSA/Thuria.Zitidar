namespace Thuria.Zitidar.Core.Result;

/// <summary>
/// Zitidar Error
/// </summary>
public interface IZitidarError
{
    /// <summary>
    /// Error Code
    /// </summary>
    int ErrorCode { get; }

    /// <summary>
    /// Error Message
    /// </summary>
    string Message { get; }

    /// <summary>
    /// Exception
    /// </summary>
    Exception? Exception { get; }
}