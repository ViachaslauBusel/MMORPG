using RUCP;
using RUCP.Handler;
using UnityEngine;
using Zenject;

public class ProfessionManager : MonoBehaviour
{
    public Profession blacksmith;
    public Profession tanner;
    private Canvas canvas;
    private NetworkManager networkManager;

    [Inject]
    private void Construct(NetworkManager networkManager)
    {
        this.networkManager = networkManager;
        networkManager.RegisterHandler(Types.ProfessionUpdate, ProfessionUpdate);
    }

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
       
    }

    private void ProfessionUpdate(Packet nw)
    {
        ProfessionEnum profession = (ProfessionEnum)nw.ReadInt();
        switch (profession)
        {
            case ProfessionEnum.Blacksmith:
                blacksmith.UpdatePruf(nw);
                break;
            case ProfessionEnum.Tanner:
                tanner.UpdatePruf(nw);
                break;
            default:
                print("Error update profession");
                break;
        }
    }

    public void OpenClose()
    {
        canvas.enabled = !canvas.enabled;
    }

    private void OnDestroy()
    {
        networkManager?.UnregisterHandler(Types.ProfessionUpdate);
    }
}
