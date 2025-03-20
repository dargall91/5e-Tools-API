using _5eTools.API.Models;

namespace _5eTools.API.Extensions;

public static class MessageExtensions
{
    public static IEnumerable<ResponseMessage> CreateErrorMessages(this string message)
    {
        return new List<ResponseMessage>
        {
            new()
            {
                Message = message,
                MessageType = "Error"
            }
        };
    }

    public static IEnumerable<ResponseMessage> CreateErrorMessages(this IEnumerable<string> messages)
    {
        return messages.Select(x => new ResponseMessage
        {
            Message = x,
            MessageType = "Error"
        });
    }

    public static IEnumerable<ResponseMessage> CreateMessages(this IEnumerable<string> messages, string messageType)
    {
        return messages.Select(x => new ResponseMessage
        {
            Message = x,
            MessageType = messageType
        });
    }

    public static IEnumerable<ResponseMessage> CreateMessages(this string message, string messageType)
    {
        return new List<ResponseMessage>
        {
            new()
            {
                Message = message,
                MessageType = messageType
            }
        };
    }
}
