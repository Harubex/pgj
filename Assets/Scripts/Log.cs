using UnityLog = UnityEngine.Debug;

public static class Log {
    public static bool Enabled { get; set; } = true;
    /// <summary>
    /// Whether or not to include verbose logs. Defaults to false.
    /// This is gonna spam the shit out of you and should only be used for sanity checking.
    /// </summary>
    /// <seealso cref="Verbose(string)"/>
    public static bool IncludeVerbose { get; set; } = false;

    /// <summary>
    /// Logs the provided text to the console if <c>IncludeVerbose</c> is set to true.
    /// Meant for logging things that are inherently spammy.
    /// </summary>
    /// <param name="msg">The message to print out.</param>
    /// <seealso cref="IncludeVerbose"/>
    public static void Verbose(string msg) {
        if (Enabled && IncludeVerbose) {
            UnityLog.Log(msg);
        }
    }

    /// <summary>
    /// Logs the provided text to the console.
    /// </summary>
    /// <param name="msg">The message to print out.</param>
    public static void Debug(string msg) {
        if (Enabled) {
            UnityLog.Log(msg);
        }
    }

    /// <summary>
    /// Stringifies provided object and logs it to the console.
    /// </summary>
    /// <param name="msg">The message to print out.</param>
    public static void Debug(object obj) {
        if (Enabled) {
            UnityLog.Log(obj);
        }
    }

    /// <summary>
    /// Logs the provided warning to the console.
    /// </summary>
    /// <param name="msg">The warning to print out.</param>
    public static void Warning(string msg) {
        if (Enabled) {
            UnityLog.LogWarning(msg);
        }
    }

    /// <summary>
    /// Logs the provided error to the console.
    /// </summary>
    /// <param name="msg">The error to print out.</param>
    public static void Error(string msg) {
        if (Enabled) {
            UnityLog.LogError(msg);
        }
    }
}
