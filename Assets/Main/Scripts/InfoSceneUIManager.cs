using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfoSceneUIManager : MonoBehaviour
{
    public string sceneName;

    private void Awake()
    {
        Debug.Log("MER");
    }
    public void StartGame()
    {
        Debug.Log("ASDADADSD");
        SceneManager.LoadScene(sceneName);
    }
}
