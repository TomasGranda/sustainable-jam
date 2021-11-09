using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Commands
{
    Dictionary<List<KeyCode>, Command> commandsKeys = new Dictionary<List<KeyCode>, Command>();
    private Command defaultCommand;

    public Commands(Command defaultCommand)
    {
        this.defaultCommand = defaultCommand;
    }

    public Commands() { }

    public void AddDefaultCommmand(Command command)
    {
        defaultCommand = command;
    }

    public void AddKeysCommand(Command command, params KeyCode[] keys)
    {
        commandsKeys.Add(keys.ToList<KeyCode>(), command);
    }

    public void ExecuteCommands()
    {
        if (!Input.anyKey && defaultCommand != null)
        {
            defaultCommand.Execute();
        }

        foreach (var command in commandsKeys)
        {
            if (command.Key.Any((k) => Input.GetKey(k)))
            {
                command.Value.Execute();
            }
            else
            {
                command.Value.Reset();
            }

            if (command.Key.Any((k) => Input.GetKeyDown(k)))
            {
                command.Value.ExecuteDown();
            }
        }
    }
}
