using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : BaseFighter
{
    public float speed;

    public float jumpForce;

    public float fallGravityScale;

    public float defaultGravityScale;

    public List<GameObject> team = new List<GameObject>();
}
