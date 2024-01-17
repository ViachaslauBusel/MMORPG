using Characters;
using Monsters;
using RUCP;
using RUCP.Handler;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TargetView : MonoBehaviour
{
    private UnitTargetRequestSender m_unitTargetRequestSender;
    public GameObject targetCircle;
    private GameObject targetCircle_obj;
    [SerializeField]
    private Text m_nameLabel;
    [SerializeField]
    private Image m_hpBar;
    private GameObject targetView;
    public static ITargetObject target_obj;

    [Inject]
    public void Construct(UnitTargetRequestSender unitTargetRequestSender)
    {
        m_unitTargetRequestSender = unitTargetRequestSender;
    }

    private void Start()
    {
        m_nameLabel = GetComponentInChildren<Text>();
        targetView = transform.Find("ImageTarget").gameObject;
        targetView.SetActive(false);
    }

    public void SetTarget(string name, int percentHP)
    {
        //if(targetCircle_obj != null)
        //{
        //    Destroy(targetCircle_obj);
        //}

       

        UpdateTarget(percentHP);
        m_nameLabel.text = name;

        //targetCircle_obj = Instantiate(targetCircle);
        //targetCircle_obj.transform.SetParent(target_obj.GetTransform());
        //targetCircle_obj.transform.localPosition = Vector3.zero;
        //targetCircle_obj.transform.localScale = Vector3.one;
        if (percentHP == -1 && string.IsNullOrEmpty(name))
        {
            targetView.SetActive(false);
        }
        else
        {
            targetView.SetActive(true);
        }
    }

    public void CancelTarget()
    {
        m_unitTargetRequestSender.SetTarget(0);
    }

    public void UpdateTarget(int percentHP)
    {
        m_hpBar.fillAmount = percentHP / 100.0f;
    }
}
