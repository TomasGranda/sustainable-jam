using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombatManager : MonoBehaviour
{
    private List<Transform> playerTeam;

    private List<Transform> enemyTeam;

    private Vector3 playerPosition;

    private void Start()
    {
        EventManager.Subscribe(EventManager.Parameter.StartCombat, StartCombat);
    }

    private void StartCombat(params object[] objects)
    {
        Stage stage = (Stage)objects[0];
        SetFightersPositions(stage, (List<GameObject>)objects[1], (List<GameObject>)objects[2]);

        Camera.main.gameObject.SetActive(false);
        stage.stageCamera.SetActive(true);

    }

    private void SetFightersPositions(Stage stage, List<GameObject> enemyTeam, List<GameObject> playerTeam)
    {
        for (var i = 0; i < stage.enemyTeamPositions.Count; i++)
        {
            enemyTeam[i].transform.position = stage.enemyTeamPositions[i].position;
        }

        for (var i = 0; i < stage.playerTeamPositions.Count; i++)
        {
            playerTeam[i].transform.position = stage.playerTeamPositions[i].position;
        }
    }


    private void OnDrawGizmos()
    {
        var a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
