using Mini.Common.Models;

namespace Mini.Common.Requests;

public readonly record struct LoginRequest(string Username, string Password, SecurityCredential? Encrypting = null)
{
    public override string ToString() =>
        $"Username:{Username}, Password:{new string('*', Password.Length)}, Encrypting:{Encrypting}";
}

#pragma warning disable S125 // Sections of code should not be commented out
/*
 * 
public readonly record struct LoginRequest(string Username, string Password, string PublicKey = "")
{
    public override string ToString() =>
        $"Username:{Username}, Password:{Password}, PublicKey:{PublicKey}";
}

*/
#pragma warning restore S125 // Sections of code should not be commented out