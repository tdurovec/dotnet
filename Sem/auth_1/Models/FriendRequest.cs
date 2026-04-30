using System;
using System.ComponentModel.DataAnnotations;
using auth_1.Models;

namespace auth_1.Models;

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
    public virtual ApplicationUser? Sender { get; set; }

    [Required]
    public string ReceiverId { get; set; } = string.Empty;
    public virtual ApplicationUser? Receiver { get; set; }

    public FriendRequestStatus Status { get; set; } = FriendRequestStatus.Pending;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
