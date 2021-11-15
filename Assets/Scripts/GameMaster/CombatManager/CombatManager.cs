using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    private GameObject playerTeam;

    private GameObject enemyTeam;

    private Vector3 playerPosition;

    private List<BaseFighter> turnList = new List<BaseFighter>();

    public GameObject combatOptionsPanel;
    public GameObject combatOptionsPanel2;

    public AudioClip combatAudio;
    public AudioClip levelAudio;

    private Stage currentStage;

    public Camera mainCamera;

    private int currentTurn = 0;

    private float time = 0.5f;

    private bool combat = false;

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

        StartCoroutine(StartTurn());
    }

    private IEnumerator StartTurn()
    {
        yield return new WaitForSeconds(0.5f);
        if (combat)
        {
            SetCombatOptions(turnList[currentTurn].stats.allied);
            EventManager.CallEvent(EventManager.Parameter.TurnStarts, turnList[currentTurn]);
        }
    }

    private void StartCombat(params object[] objects)
    {
        GetComponent<AudioSource>().clip = combatAudio;
        GetComponent<AudioSource>().Play();
        combat = true;
        EventManager.Subscribe(EventManager.Parameter.EnemyAttack, MakeEnemyAttack);
        EventManager.Subscribe(EventManager.Parameter.PlayerAttack, MakePlayerAttack);
        EventManager.Subscribe(EventManager.Parameter.EndCombat, EndCombat);

        currentStage = (Stage)objects[0];
        List<GameObject> enemyTeam = (List<GameObject>)objects[1];
        List<GameObject> playerTeam = (List<GameObject>)objects[2];
        this.playerTeam = playerTeam.First();
        this.enemyTeam = enemyTeam.First();

        SetFightersPositions(currentStage, enemyTeam, playerTeam);

        SetTurnList(enemyTeam.Select((gm) => gm.GetComponent<BaseFighter>()).ToList(), playerTeam.Select((gm) => gm.GetComponent<BaseFighter>()).ToList());

        mainCamera.gameObject.SetActive(false);
        currentStage.stageCamera.SetActive(true);

        combatOptionsPanel2.SetActive(true);
        StartCoroutine(StartTurn());
    }

    private void EndCombat(params object[] objects)
    {
        StartCoroutine(UnSetCombatCamera());
    }



    private void MakeEnemyAttack(params object[] objects)
    {
        playerTeam.GetComponent<PlayerModel>().life -= 5;
        var playerBar = combatOptionsPanel2.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        playerBar.fillAmount = playerTeam.GetComponent<PlayerModel>().life / playerTeam.GetComponent<PlayerModel>().maxLife;
    }

    private void MakePlayerAttack(params object[] objects)
    {
        enemyTeam.GetComponent<EnemyModel>().life -= 10;
        var enemyBar = combatOptionsPanel2.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        enemyBar.fillAmount = enemyTeam.GetComponent<EnemyModel>().life / enemyTeam.GetComponent<EnemyModel>().maxLife;
    }

    private void SetTurnList(List<BaseFighter> enemyTeam, List<BaseFighter> playerTeam)
    {
        turnList.AddRange(enemyTeam);
        turnList.AddRange(playerTeam);

        // Order By Initiative
        turnList.Sort((figher1, figher2) => figher2.stats.initiative - figher1.stats.initiative);
    }

    private IEnumerator UnSetCombatCamera()
    {
        yield return new WaitForSeconds(1);
        if (enemyTeam.name == "EnemyBoss")
        {
            SceneManager.LoadScene("CreditsScene");
        }
        combat = false;
        EventManager.Unsubscribe(EventManager.Parameter.EnemyAttack, MakeEnemyAttack);
        EventManager.Unsubscribe(EventManager.Parameter.PlayerAttack, MakePlayerAttack);

        enemyTeam = null;
        playerTeam = null;

        turnList.Clear();
        combatOptionsPanel2.SetActive(false);

        mainCamera.gameObject.SetActive(true);
        currentStage.stageCamera.SetActive(false);
        currentStage = null;
        var enemyBar = combatOptionsPanel2.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        enemyBar.fillAmount = 1;
        GetComponent<AudioSource>().clip = levelAudio;
        GetComponent<AudioSource>().Play();

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
