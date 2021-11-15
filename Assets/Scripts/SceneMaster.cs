using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public string scene;

    public GameObject button;
    public GameObject panel;

    public void ChangeScene()
    {
        SceneManager.LoadScene(scene);
    }

    public void HiddenButton()
    {
        button.SetActive(false);
        panel.SetActive(true);
    }
}
