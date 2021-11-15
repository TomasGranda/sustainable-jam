using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : BaseFighter
{
    public Stage combatStage;

    public List<GameObject> team = new List<GameObject>();

    public List<Transform> points = new List<Transform>();

    public float life;

    public float maxLife = 100;

    public float speed;

    public SpriteRenderer sprite;

    public void DestroyGM()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1);
        transform.position = Vector2.down * 20000;
    }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

}
