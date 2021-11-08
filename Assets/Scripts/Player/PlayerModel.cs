using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public float speed;

    public float jumpForce;

    public float fallGravityScale;

    public float defaultGravityScale;

    public float combatTriggerRange;

    public List<GameObject> team = new List<GameObject>();

}
