using _5eTools.API.Extensions;

namespace _5eTools.API.Models;

public class ResponseWrapper<T>
{
    public IEnumerable<ResponseMessage>? Messages { get; set; }
    public T? Data { get; set; }

    public ResponseWrapper() { }

    public ResponseWrapper(T? data)
    {
        Data = data;
    }

    public ResponseWrapper(string message)
    {
        Messages = message.CreateErrorMessages();
    }

    public ResponseWrapper(IEnumerable<string> messages)
    {
        Messages = messages.CreateErrorMessages();
    }

    public ResponseWrapper(string message, string messageType)
    {
        Messages = message.CreateMessages(messageType);
    }

    public ResponseWrapper(IEnumerable<string> messages, string messageType)
    {
        Messages = messages.CreateMessages(messageType);
    }

    public ResponseWrapper(T? data, string message)
    {
        Data = data;
        Messages = message.CreateErrorMessages();
    }

    public ResponseWrapper(T? data, IEnumerable<string> messages)
    {
        Data = data;
        Messages = messages.CreateErrorMessages();
    }

    public ResponseWrapper(T? data, IEnumerable<string> messages, string messageType)
    {
        Data = data;
        Messages = messages.CreateMessages(messageType);
    }
}
