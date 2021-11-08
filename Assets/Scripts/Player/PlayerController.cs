using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Commands commands;
    private PlayerModel model;
    private PlayerView view;

    private GameMaster gameMaster;

    void Start()
    {
        gameMaster = GameMaster.GetGameMaster();
        view = GetComponent<PlayerView>();
        model = GetComponent<PlayerModel>();

        commands = new Commands();

        MoveCommand moveCommand = new MoveCommand(model, view, GetComponent<Rigidbody2D>());
        JumpCommand jumpCommand = new JumpCommand(model, view, GetComponent<Rigidbody2D>(), gameMaster.floorLayer);

        commands.AddKeysCommand(moveCommand, KeyCode.A, KeyCode.D);
        commands.AddKeysCommand(jumpCommand, KeyCode.Space);
    }

    void Update()
    {
        commands.ExecuteCommands();
    }
}
