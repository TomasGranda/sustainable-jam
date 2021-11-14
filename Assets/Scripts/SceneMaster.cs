using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public string scene;

    public void ChangeScene()
    {
        SceneManager.LoadScene(scene);
    }
}
