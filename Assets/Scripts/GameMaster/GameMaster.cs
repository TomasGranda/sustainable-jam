using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [Header("Layer Config")]
    public LayerMask floorLayer;
    public LayerMask enemyLayer;

    private static GameMaster gameMaster;
    public static GameMaster Instance { get { return gameMaster; } set { if (Instance != null) Destroy(value); else gameMaster = value; } }

    private void Awake()
    {
        gameMaster = this;
    }
}
