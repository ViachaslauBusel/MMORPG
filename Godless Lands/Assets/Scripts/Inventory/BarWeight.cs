using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarWeight : MonoBehaviour
{
    public Image bar;
    public Text text;
   // private int maxWeight;

   /* public void LoadWeight(int weight, int maxWeight)
    {
        this.maxWeight = maxWeight;
        UpdateWeight(weight);
    }*/

    public void UpdateWeight(int weight, int maxWeight)
    {
        bar.fillAmount = weight / (float)maxWeight;
        text.text = weight + "/" + maxWeight;
    }
}
