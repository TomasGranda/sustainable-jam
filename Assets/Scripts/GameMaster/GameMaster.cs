using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [Header("Layer Config")]
    public LayerMask floorLayer;

    public LayerMask enemyLayer;

    private static GameMaster gameMaster;

    private void Awake()
    {
        gameMaster = this;
    }

    public static GameMaster GetGameMaster()
    {
        return gameMaster;
    }
}
