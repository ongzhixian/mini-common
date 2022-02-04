namespace Mini.Common.Models;

public record struct EncryptedMessage(byte[] IV, byte[] EncryptedSessionKey, byte[] EncryptedMessageBytes)
{
    public override string ToString()
    {
        return $"IV:{IV.Length}, EncryptedSessionKey:{EncryptedSessionKey.Length}, EncryptedMessageBytes:{EncryptedMessageBytes.Length}";
    }
}