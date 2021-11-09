using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombatManager : MonoBehaviour
{
    private List<Transform> playerTeam;

    private List<Transform> enemyTeam;

    private Vector3 playerPosition;

    private List<BaseFighter> turnList = new List<BaseFighter>();

    private int currentTurn = 0;

    private void Start()
    {
        EventManager.Subscribe(EventManager.Parameter.StartCombat, StartCombat);
        EventManager.Subscribe(EventManager.Parameter.TurnEnds, OnTurnEnds);
    }

    private void OnTurnEnds(params object[] objects)
    {
        currentTurn++;

        if (currentTurn > turnList.Count - 1)
            currentTurn = 0;

        EventManager.CallEvent(EventManager.Parameter.TurnStarts, turnList[currentTurn]);
    }

    private void StartCombat(params object[] objects)
    {
        Stage stage = (Stage)objects[0];
        List<GameObject> enemyTeam = (List<GameObject>)objects[1];
        List<GameObject> playerTeam = (List<GameObject>)objects[2];

        SetFightersPositions(stage, enemyTeam, playerTeam);

        SetTurnList(enemyTeam.Select((gm) => gm.GetComponent<BaseFighter>()).ToList(), playerTeam.Select((gm) => gm.GetComponent<BaseFighter>()).ToList());

        Camera.main.gameObject.SetActive(false);
        stage.stageCamera.SetActive(true);

        EventManager.CallEvent(EventManager.Parameter.TurnStarts, turnList.First());
    }

    private void SetTurnList(List<BaseFighter> enemyTeam, List<BaseFighter> playerTeam)
    {
        turnList.AddRange(enemyTeam);
        turnList.AddRange(playerTeam);

        // Order By Initiative
        turnList.Sort((figher1, figher2) => figher2.stats.initiative - figher1.stats.initiative);
    }

    private void SetFightersPositions(Stage stage, List<GameObject> enemyTeam, List<GameObject> playerTeam)
    {
        for (var i = 0; i < stage.enemyTeamPositions.Count; i++)
        {
            if (enemyTeam[i] != null)
                enemyTeam[i].transform.position = stage.enemyTeamPositions[i].position;
            else break;
        }

        for (var i = 0; i < stage.playerTeamPositions.Count; i++)
        {
            if (playerTeam[i] != null)
                playerTeam[i].transform.position = stage.playerTeamPositions[i].position;
            else break;
        }
    }
}