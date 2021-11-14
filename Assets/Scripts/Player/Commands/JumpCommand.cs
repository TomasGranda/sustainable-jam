using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : Command
{
    private PlayerModel model;
    private PlayerView view;

    private Rigidbody2D rigidbody;

    private LayerMask floorLayer;

    public JumpCommand(PlayerModel model, PlayerView view, Rigidbody2D rigidbody, LayerMask floorLayer)
    {
        this.model = model;
        this.view = view;
        this.rigidbody = rigidbody;
        this.floorLayer = floorLayer;
    }

    public void Execute() { }

    public void ExecuteDown()
    {
        if (Physics2D.Raycast(model.transform.position, Vector2.down, 1.2f, floorLayer))
        {
            rigidbody.AddForce(Vector2.up * model.jumpForce, ForceMode2D.Impulse);
        }
        Always();
    }

    public void Reset()
    {
        Always();
    }

    private void Always()
    {
        if (Physics2D.Raycast(model.transform.position, Vector2.down, 1.2f, floorLayer))
        {
            view.animator.SetBool("Jump", false);
        }
        else
        {
            view.animator.SetBool("Jump", true);
        }
        rigidbody.gravityScale = rigidbody.velocity.y < 0 ? model.fallGravityScale : model.defaultGravityScale;
    }
}
