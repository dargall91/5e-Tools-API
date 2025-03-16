using System.Text.Json.Serialization;

namespace _5eTools.API.Models;

public class Message
{
    [JsonPropertyName("Message")]
    public required string MessageText { get; set; }
    public required string MessageType { get; set; }

    public static Message CreateErrorMessage(string message)
    {
        return new Message
        {
            MessageText = message,
            MessageType = "error"
        };
    }
}
