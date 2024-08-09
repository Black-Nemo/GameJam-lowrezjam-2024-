using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class İnfectedScore : MonoBehaviour
{
    public ScoreBar scoreBar;
    public int minScore = 0;

    public int upScore;

    private void Start() {
        upScore = minScore;
        scoreBar.SetMinScore(minScore);
    }
    private void Update() {
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            TakeScore(20);
        }
    }

    public void TakeScore(int Score)
    {
        upScore += Score;
        scoreBar.SetScore(upScore);
    }
}
