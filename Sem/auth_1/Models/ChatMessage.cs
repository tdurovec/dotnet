using System;
using System.ComponentModel.DataAnnotations;
using auth_1.Models;

namespace auth_1.Models;

public class ChatMessage
{
    public int Id { get; set; }

    [Required]
    public string SenderId { get; set; } = string.Empty;
    public virtual ApplicationUser? Sender { get; set; }

    [Required]
    public string ReceiverId { get; set; } = string.Empty;
    public virtual ApplicationUser? Receiver { get; set; }

    [Required]
    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; } = false;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
