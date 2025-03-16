using Microsoft.AspNetCore.Mvc;

namespace _5eTools.API.Models;

public class ResponseWrapper<T>() : ObjectResult(null)
{
    public IEnumerable<Message>? Messages { get; set; }
    public T? Data { get; set; }

    private ResponseWrapper(int statusCode) : this()
    {
        StatusCode = statusCode;
    }

    private ResponseWrapper(T? data, int statusCode) : this()
    {
        Data = data;
        StatusCode = statusCode;
    }

    private ResponseWrapper(int statusCode, Message? message) : this()
    {
        StatusCode = statusCode;
        Messages = message == null ? null : new List<Message> { message };
    }

    private ResponseWrapper(int statusCode, IEnumerable<Message>? messages) : this()
    {
        StatusCode = statusCode;
        Messages = messages;
    }

    public static ResponseWrapper<T> Ok(T? data) => new(data, StatusCodes.Status200OK);

    public static ResponseWrapper<T> Created() => new(StatusCodes.Status201Created);

    public static ResponseWrapper<T> BadRequest(string error)
        => new(StatusCodes.Status400BadRequest, Message.CreateErrorMessage(error));

    public static ResponseWrapper<T> BadRequest(IEnumerable<string> errors)
        => new(StatusCodes.Status400BadRequest, errors.Select(Message.CreateErrorMessage));

    public static ResponseWrapper<T> Unauthorized(string error)
        => new(StatusCodes.Status401Unauthorized, Message.CreateErrorMessage(error));

    public static ResponseWrapper<T> Unauthorized(IEnumerable<string> errors)
        => new(StatusCodes.Status401Unauthorized, errors.Select(Message.CreateErrorMessage));

    public static ResponseWrapper<T> InternalServerError(string error)
        => new(StatusCodes.Status500InternalServerError, Message.CreateErrorMessage(error));

    public static ResponseWrapper<T> InternalServerError(IEnumerable<string> errors)
        => new(StatusCodes.Status500InternalServerError, errors.Select(Message.CreateErrorMessage));
}
