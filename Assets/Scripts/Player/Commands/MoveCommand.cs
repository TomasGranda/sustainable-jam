using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    private PlayerModel model;
    private PlayerView view;
    private Rigidbody2D rigidbody;

    public MoveCommand(PlayerModel model, PlayerView view, Rigidbody2D rigidbody)
    {
        this.model = model;
        this.view = view;
        this.rigidbody = rigidbody;
    }

    public void Execute()
    {
        float axis = Input.GetAxis("Horizontal");

        Vector2 movement = model.transform.right * axis * model.speed;

        rigidbody.velocity = new Vector2(movement.x, rigidbody.velocity.y);
        // model.transform.Translate(movement * model.speed * Time.deltaTime);
    }

    public void ExecuteDown() { }

    public void Reset() { }
}
