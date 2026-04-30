using System.ComponentModel.DataAnnotations;

namespace Messenger.Models;

public enum FriendRequestStatus
{
    Pending,
    Accepted,
    Rejected
}

public class FriendRequest
{
    public int Id { get; set; }

    [Required]
    public string SenderId { get; set; } = string.Empty;
    public virtual User? Sender { get; set; }

    [Required]
    public string ReceiverId { get; set; } = string.Empty;
    public virtual User? Receiver { get; set; }

    public FriendRequestStatus Status { get; set; } = FriendRequestStatus.Pending;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

