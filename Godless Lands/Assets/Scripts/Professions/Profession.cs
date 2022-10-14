using RUCP;
using UnityEngine;
using UnityEngine.UI;

public class Profession : MonoBehaviour
{
    public Text level;
    public Image bar;
    public Text point;

    public void SetLevel(int level)
    {
        this.level.text = level.ToString();
    }
    public void SetBar(int exp, int allExp)
    {
        bar.fillAmount = exp / (float)allExp;
        point.text = exp.ToString() + '/' + allExp.ToString();
    }

    public void UpdatePruf(Packet nw)
    {
        int level = nw.ReadInt();
        int exp = nw.ReadInt();
        int allExp = nw.ReadInt();
        SetLevel(level);
        SetBar(exp, allExp);
    }
}
