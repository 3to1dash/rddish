using System.Text.Json.Serialization;

namespace rddish.Application;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public List<ErrorDto>? Errors { get; set; }

    public Result<T> Success(T data)
    {
        var result = new Result<T>
        {
            IsSuccess = true,
            Data = data,
            Errors = new List<ErrorDto>()
        };

        return result;
    }

    public Result<T> Success()
    {
        var result = new Result<T>
        {
            IsSuccess = true,
            Errors = new List<ErrorDto>()
        };

        return result;
    }

    public Result<T> Fail(IEnumerable<string> errors)
    {
        var result = new Result<T>
        {
            IsSuccess = false,
            Errors = errors.Select(e => new ErrorDto { Message = e }).ToList()
        };

        return result;
    }

    public Result<T> Fail(string error)
    {
        var result = new Result<T>
        {
            IsSuccess = false,
            Errors = new List<ErrorDto> { new() { Message = error } }
        };

        return result;
    }

    public Result<T> Fail(Exception exception)
    {
        var result = new Result<T>
        {
            IsSuccess = false,
            Errors = new List<ErrorDto> { new() { Message = exception.Message } }
        };

        return result;
    }

    public Result<T> Fail(List<ErrorDto> errors)
    {
        var result = new Result<T>
        {
            IsSuccess = false,
            Errors = errors,
        };

        return result;
    }

    public Result<T> AppendErrors(IEnumerable<string> errors)
    {
        Errors = errors.Select(e => new ErrorDto { Message = e }).ToList();
        return this;
    }

    public Result<T> AppendError(string error)
    {
        Errors = new List<ErrorDto> { new() { Message = error } };
        return this;
    }

    public Result<T> AppendError(Exception exception)
    {
        Errors = new List<ErrorDto> { new() { Message = exception.Message } };
        return this;
    }
}

public class ErrorDto
{
    public string? Message { get; set; }
}