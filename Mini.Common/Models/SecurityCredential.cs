using Microsoft.IdentityModel.Tokens;

namespace Mini.Common.Models;


public readonly record struct SecurityCredential(
    string SecurityAlgorithm
    , string SecurityDigest
    , string Xml)
{
    // SecurityAlgorithms.RsaSsaPssSha256
    //public readonly string SecurityAlgorithm { get; init; } = string.Empty;

    // SecurityAlgorithms.RsaSha256Signature
    //public readonly string SecurityDigest { get; init; } = string.Empty;

    //public readonly string Xml { get; init; } = string.Empty;

    public override string ToString() =>
        $"SecurityAlgorithm:{SecurityAlgorithm}, SecurityDigest:{SecurityDigest}, Xml:{Xml}";
}