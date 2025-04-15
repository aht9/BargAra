namespace BargAra.Domain.Helper;

public static class ConstClass
{
    public static string CurrentEnvironment { get; private set; } = "";

    public static void SetCurrentEnvironment(string envName)
    {
        CurrentEnvironment = envName;
    } 
}