using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public float Timer;
    public Text timerText;

    public List<GameObject> enemys = new List<GameObject>();
    public Slider scoreSilder;
    public float kazanmaYuzdesi = 70;


    public UnityEvent VictoryEvent;
    public UnityEvent GameOverEvent;

    public Button RetryButton;

    private void Awake()
    {
        Time.timeScale = 1;
        enemys = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        RetryButton.onClick.AddListener(() => { Time.timeScale = 1; SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
    }

    private void LateUpdate()
    {
        Timer -= Time.deltaTime;
        timerText.text = Timer.ToString("0,0");

        if (Timer <= 0)
        {
            GameOverEvent?.Invoke();
            Time.timeScale = 0;
        }

        scoreSilder.value = 0;
        scoreSilder.maxValue = enemys.Count();
        foreach (var item in enemys)
        {
            if (item.GetComponent<Enemy>().isInfected)
            {
                scoreSilder.value++;
            }
        }
        if (scoreSilder.maxValue * (float)(kazanmaYuzdesi / 100) < scoreSilder.value)
        {
            Debug.Log(scoreSilder.maxValue * (kazanmaYuzdesi / 100) + "   :   " + scoreSilder.value);
            Debug.Log("Kazandiniz...");
            VictoryEvent?.Invoke();
            //StartCoroutine(enumerator());
            Task.Run(ExitFunc);
            Time.timeScale = 0;
        }
    }

    public async Task ExitFunc()
    {
        await Task.Delay(5000);
        Debug.Log("cik");
        Application.Quit();
    }

    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("cik");
        Application.Quit();
    }
}
