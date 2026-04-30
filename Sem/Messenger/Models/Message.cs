using System.ComponentModel.DataAnnotations;

namespace Messenger.Models;

public class ChatMessage
{
    public int Id { get; set; }

    [Required]
    public string SenderId { get; set; } = string.Empty;
    public virtual User? Sender { get; set; }

    [Required]
    public string ReceiverId { get; set; } = string.Empty;
    public virtual User? Receiver { get; set; }

    [Required]
    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; } = false;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
