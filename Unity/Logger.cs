using System.Diagnostics;


public static class Logger
{
    [Conditional("LOGGING")]
    public static void Debug(object message)
    {
        UnityEngine.Debug.Log(message);
    }

    [Conditional("LOGGING")]
    public static void Debug(string format, object message)
    {
        UnityEngine.Debug.LogFormat(format, message);
    }

    [Conditional("LOGGING")]
    public static void Warning(object message)
    {
        UnityEngine.Debug.LogWarning(message);
    }

    [Conditional("LOGGING")]
    public static void Warning(string format, object message)
    {
        UnityEngine.Debug.LogWarningFormat(format, message);
    }

    [Conditional("LOGGING")]
    public static void Error(object message)
    {
        UnityEngine.Debug.LogError(message);
    }

    [Conditional("LOGGING")]
    public static void Error(string format, object message)
    {
        UnityEngine.Debug.LogErrorFormat(format, message);
    }

    public static void CriticalError(object message)
    {
        UnityEngine.Debug.LogError(message);
    }

    public static void CriticalError(string format, object message)
    {
        UnityEngine.Debug.LogErrorFormat(format, message);
    }
}
