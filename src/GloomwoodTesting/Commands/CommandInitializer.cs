using System.Collections.Generic;
using On.Gloomwood.RuntimeConsole;

namespace GloomwoodTesting.Commands;

public static class CommandInitializer
{
    private static readonly List<ICommand> Commands = [];
    
    public static void AddCommand(ICommand command)
    {
        Plugin.Log.LogDebug($"Adding console command: {command.Name}");
        Commands.Add(command);
    }

    public static void InitializeCommands()
    {
        ConsoleController.Start += ConsoleControllerOnStart;
    }

    private static void ConsoleControllerOnStart(ConsoleController.orig_Start orig, 
        Gloomwood.RuntimeConsole.ConsoleController self)
    {

        foreach (var command in Commands)
        {
            self.RegisterCommand(command.GetCommand());
        }
        
        orig(self);
    }
}