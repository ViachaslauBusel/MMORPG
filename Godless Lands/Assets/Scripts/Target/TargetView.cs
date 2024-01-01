using Characters;
using Monsters;
using RUCP;
using RUCP.Handler;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TargetView : MonoBehaviour
{

    public GameObject targetCircle;
    private GameObject targetCircle_obj;
    [SerializeField]
    private Text m_nameLabel;
    [SerializeField]
    private Image m_hpBar;
    private GameObject targetView;
    public static ITargetObject target_obj;


    private void Start()
    {
        m_nameLabel = GetComponentInChildren<Text>();
        targetView = transform.Find("ImageTarget").gameObject;
        targetView.SetActive(false);
    }

    public void SetTarget(string name, int hp, int maxHp)
    {
        //if(targetCircle_obj != null)
        //{
        //    Destroy(targetCircle_obj);
        //}

        UpdateTarget(hp, maxHp);
        m_nameLabel.text = name;

        //targetCircle_obj = Instantiate(targetCircle);
        //targetCircle_obj.transform.SetParent(target_obj.GetTransform());
        //targetCircle_obj.transform.localPosition = Vector3.zero;
        //targetCircle_obj.transform.localScale = Vector3.one;
        targetView.SetActive(true);
    }

    public void UpdateTarget(int hp, int maxHP)
    {
        m_hpBar.fillAmount = hp / (float)maxHP;
    }
}
