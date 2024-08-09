using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class healtBar : MonoBehaviour
{
   public Slider slider;
    
   public void SetMaxHealt(int healt)
   {
    slider.maxValue = healt;
    slider.value = healt;
   }

   public void SetHealt(int healt)
   {
        slider.value = healt;
   }
}
