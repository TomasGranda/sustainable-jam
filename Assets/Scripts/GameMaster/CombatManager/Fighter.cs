using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Fighter", menuName = "sustainable-jam/Fighter", order = 0)]
public class Fighter : ScriptableObject
{
    public float life;

    public int initiative;

    public bool allied;

}