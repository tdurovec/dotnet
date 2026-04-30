using System;

namespace Messenger.Services;

public class ChatNotificationService
{
    public event Action<string, string>? OnMessageReceived;

    public void NotifyMessage(string receiverId, string senderName)
    {
        OnMessageReceived?.Invoke(receiverId, senderName);
    }
}
