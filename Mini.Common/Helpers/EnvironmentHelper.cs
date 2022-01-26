namespace Mini.Common.Helpers;

public static class EnvironmentHelper
{
    // We cannot add extension methods to a static class.

    public static string GetVariable(string variableName)
    {
        string? variableValue = Environment.GetEnvironmentVariable(variableName);

        if (variableValue == null)
            throw new InvalidOperationException($"Environment variable {variableName} does not exists.");

        return variableValue;
    }
}