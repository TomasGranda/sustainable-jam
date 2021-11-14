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

        if (movement.x > 0)
        {
            model.sprite.flipX = false;
        }
        else if (movement.x < 0)
        {
            model.sprite.flipX = true;
        }

        view.animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    }

    public void ExecuteDown() { }

    public void Reset()
    {
        view.animator.SetFloat("Speed", 0);
    }
}
