using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombatManager : MonoBehaviour
{
    private GameObject playerTeam;

    private GameObject enemyTeam;

    private Vector3 playerPosition;

    private List<BaseFighter> turnList = new List<BaseFighter>();

    public GameObject combatOptionsPanel;

    private Stage currentStage;

    private int currentTurn = 0;

    private void Start()
    {
        EventManager.Subscribe(EventManager.Parameter.StartCombat, StartCombat);
        EventManager.Subscribe(EventManager.Parameter.TurnEnds, OnTurnEnds);
    }

    private void OnTurnEnds(params object[] objects)
    {
        SetCombatOptions(false);
        currentTurn++;

        if (currentTurn > turnList.Count - 1)
            currentTurn = 0;

        StartTurn();
    }

    private void StartTurn()
    {
        SetCombatOptions(turnList[currentTurn].stats.allied);
        EventManager.CallEvent(EventManager.Parameter.TurnStarts, turnList[currentTurn]);
    }

    private void StartCombat(params object[] objects)
    {
        currentStage = (Stage)objects[0];
        List<GameObject> enemyTeam = (List<GameObject>)objects[1];
        List<GameObject> playerTeam = (List<GameObject>)objects[2];
        this.playerTeam = playerTeam.First();
        this.enemyTeam = enemyTeam.First();

        SetFightersPositions(currentStage, enemyTeam, playerTeam);

        SetTurnList(enemyTeam.Select((gm) => gm.GetComponent<BaseFighter>()).ToList(), playerTeam.Select((gm) => gm.GetComponent<BaseFighter>()).ToList());

        Camera.main.gameObject.SetActive(false);
        currentStage.stageCamera.SetActive(true);

        StartTurn();
    }

    private void MakeEnemyAttack()
    {
        playerTeam.GetComponent<PlayerModel>().life -= 5;
    }

    private void MakePlayerAttack()
    {
        enemyTeam.GetComponent<EnemyModel>().life -= 10;
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
                enemyTeam[i].transform.position = (Vector3.up * 2) + stage.enemyTeamPositions[i].position;
            else break;
        }

        for (var i = 0; i < stage.playerTeamPositions.Count; i++)
        {
            if (playerTeam[i] != null)
                playerTeam[i].transform.position = (Vector3.up * 2) + stage.playerTeamPositions[i].position;
            else break;
        }
    }

    public void OnAttackButtonDown()
    {
        MakeAction(Parameter.Attack);
    }
    public void OnMagicButtonDown(int p)
    {
        MakeAction(Parameter.Magic);
    }
    public void OnSkipButtonDown()
    {
        MakeAction(Parameter.Skip);
    }

    public void MakeAction(Parameter action)
    {
        SetCombatOptions(false);
        EventManager.CallEvent(EventManager.Parameter.Action, action, currentStage);
    }

    private void SetCombatOptions(bool state)
    {
        combatOptionsPanel.SetActive(state);
    }

    public enum Parameter
    {
        Attack,
        Magic,
        Skip,
    }
}
