using MyLMS.Debugging;

namespace MyLMS;

public class MyLMSConsts
{
    public const string LocalizationSourceName = "MyLMS";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "8e4afeb9083c4dafa5ddf30f6f20937c";
}
