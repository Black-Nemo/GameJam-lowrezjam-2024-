using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreBar : MonoBehaviour
{
 public Slider slider;

 public void SetScore(int score)
 {
    slider.value = score;
 }

 public void SetMinScore (int score)
 {
    slider.value = score;
    slider.minValue = score;
 }
}
