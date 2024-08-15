using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealt : MonoBehaviour
{
   public healtBar healtBar;
   
   public int MinHealt = 0;
   public int MaxHealt = 100;
   public int currentHealt;

   private void Start() 
   {
    currentHealt = MaxHealt ;
    healtBar.SetMaxHealt(MaxHealt);
    
   } 


    private void OnCollisionEnter2D(Collision2D other) {
        
            TakeDamage(20);
            healtBar.SetHealt(currentHealt);
        
    
    }

   public void TakeDamage (int damage)
   {
     currentHealt -= damage;
   }


}
