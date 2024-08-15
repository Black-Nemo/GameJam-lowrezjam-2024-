using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class İnfectedScore : MonoBehaviour
{
    public healtBar healtBar;
    public EnemyHealt enemyHealt;
    public ScoreBar scoreBar;
    public int minScore = 0;

    public bool isInfected ;

    public int upScore;

    private void Start() {
        upScore = minScore;
        scoreBar.SetMinScore(minScore);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    { 
        
        if(enemyHealt.currentHealt <= 0 )
        {
            if( isInfected != true ){

            TakeScore(20);
             isInfected = true ;

            }
            isInfected = false;
          }
          
         
        
       
        
    }
    

    public void TakeScore(int Score)
    {
        upScore += Score;
        scoreBar.SetScore(upScore);
    }
}
