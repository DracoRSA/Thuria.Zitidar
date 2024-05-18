using Thuria.Zitidar.Core.Models;

namespace Thuria.Zitidar.Core.Result;

/// <summary>
/// Zitidar Result
/// </summary>
/// <typeparam name="T">Result Value</typeparam>
public sealed class ZitidarResult<T>
{
    private readonly T? _resultValue;
    private readonly ZitidarError? _resultError;

    public ZitidarResult(T resultValue)
    {
        IsError      = false;
        _resultValue = resultValue;
        _resultError = default;
    }

    public ZitidarResult(ZitidarError resultError)
    {
        IsError      = true;
        _resultValue = default;
        _resultError = resultError;
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
    public ZitidarError? Error => _resultError;

    /// <summary>
    /// Success Implicit operator
    /// </summary>
    /// <param name="resultValue">Success Value</param>
    public static implicit operator ZitidarResult<T>(T resultValue) => new(resultValue);

    /// <summary>
    /// Error Implicit operator
    /// </summary>
    /// <param name="resultError">Error value</param>
    public static implicit operator ZitidarResult<T>(ZitidarError resultError) => new(resultError);

    /// <summary>
    /// Create Success Result
    /// </summary>
    /// <param name="successValue">Success Value</param>
    /// <returns>
    /// Newly created Success Result object
    /// </returns>
    public static ZitidarResult<T> Ok(T successValue)
    {
        return new ZitidarResult<T>(successValue);
    }

    /// <summary>
    /// Create Failure Result
    /// </summary>
    /// <param name="failureValue">Failure value</param>
    /// <returns>
    /// Newly created Error Result object
    /// </returns>
    public static ZitidarResult<T> Fail(ZitidarError failureValue)
    {
        return new ZitidarResult<T>(failureValue);
    }

    /// <summary>
    /// Match Result to its appropriate result path
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="success">Success path</param>
    /// <param name="failure">Error path</param>
    /// <returns>
    /// Newly created result
    /// </returns>
    public T Match(Func<T, T> success,
                   Func<ZitidarError, T> failure) => !IsError ? success(_resultValue!) : failure(_resultError!);
}