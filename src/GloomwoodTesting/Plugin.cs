using BepInEx;
using BepInEx.Logging;
using GloomwoodTesting.Commands;

[BepInPlugin(LCMPluginInfo.PLUGIN_GUID, LCMPluginInfo.PLUGIN_NAME, LCMPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public static ManualLogSource Log = null!;

    private void Awake()
    {
        Log = Logger;

        CommandInitializer.AddCommand(new WeaponDamageCommand());
        CommandInitializer.AddCommand(new SetDamageTypeCommand());
        CommandInitializer.InitializeCommands();

        // Log our awake here, so we can see it in LogOutput.txt file
        Log.LogInfo($"Plugin {LCMPluginInfo.PLUGIN_NAME} is loaded!");
    }
}