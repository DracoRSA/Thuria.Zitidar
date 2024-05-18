using Thuria.Zitidar.Core.Models;

namespace Thuria.Zitidar.Core.Result;

/// <summary>
/// Zitidar Result
/// </summary>
/// <typeparam name="T">Result Value</typeparam>
public sealed class ZitidarResults<T>
{
    private readonly T? _resultValue;
    private readonly List<ZitidarError>? _resultErrors;

    /// <summary>
    /// Zitidar Results Constructor
    /// </summary>
    /// <param name="resultValue">Success Result Value</param>
    public ZitidarResults(T resultValue)
    {
        IsError       = false;
        _resultValue  = resultValue;
        _resultErrors = default;
    }

    /// <summary>
    /// Zitidar Result Constructor
    /// </summary>
    /// <param name="resultError">Error Result</param>
    public ZitidarResults(List<ZitidarError> resultError)
    {
        IsError       = true;
        _resultValue  = default;
        _resultErrors = resultError;
    }

    /// <summary>
    /// Success indicator
    /// </summary>
    public bool IsError { get; } = false;

    /// <summary>
    /// Error indicator
    /// </summary>
    public bool IsSuccess => !IsError;

    /// <summary>
    /// Result Value
    /// </summary>
    public T? Value => _resultValue;

    /// <summary>
    /// Result Error
    /// </summary>
    public List<ZitidarError>? Errors => _resultErrors;

    /// <summary>
    /// Success Implicit operator
    /// </summary>
    /// <param name="resultValue">Success Value</param>
    public static implicit operator ZitidarResults<T>(T resultValue) => new(resultValue);

    /// <summary>
    /// Error Implicit operator
    /// </summary>
    /// <param name="resultError">Error value</param>
    public static implicit operator ZitidarResults<T>(List<ZitidarError> resultError) => new(resultError);

    /// <summary>
    /// Create Success Result
    /// </summary>
    /// <param name="successValue">Success Value</param>
    /// <returns>
    /// Newly created Success Result object
    /// </returns>
    public static ZitidarResults<T> Ok(T successValue)
    {
        return new ZitidarResults<T>(successValue);
    }

    /// <summary>
    /// Create Failure Result
    /// </summary>
    /// <param name="failureValues">List of Failure value</param>
    /// <returns>
    /// Newly created Error Result object
    /// </returns>
    public static ZitidarResults<T> Fail(List<ZitidarError> failureValues)
    {
        return new ZitidarResults<T>(failureValues);
    }

    /// <summary>
    /// Match Result to its appropriate result path
    /// </summary>
    /// <param name="success">Success path</param>
    /// <param name="failure">Error path</param>
    /// <returns>
    /// Newly created result
    /// </returns>
    public T Match(Func<T, T> success,
                   Func<List<ZitidarError>, T> failure) => !IsError ? success(_resultValue!) : failure(_resultErrors!);
}