using Cells;
using Network.Core;
using RUCP;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ItemEnchant : MonoBehaviour
{
    public Text textInfo;
    public Button button;
    public Text textButton;
    public GameObject indicator;
    public Image bar;
    private Canvas canvas;
    private EnchantCell enchantCell;
    private UISort uISort;
    private NetworkManager networkManager;

    [Inject]
    private void Construct(NetworkManager networkManager)
    {
        this.networkManager = networkManager;
        networkManager.RegisterHandler(Network.Core.Types.ItemEnchant, Enchant);
    }

    private void Awake()
    {
        uISort = GetComponentInParent<UISort>();
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        enchantCell = GetComponentInChildren<EnchantCell>();
        indicator.SetActive(false);

       
    }

    private void Enchant(Packet nw)
    {
        switch ((EnchantCommand) nw.ReadByte())
        {
            case EnchantCommand.OpenGUI://Open Enchant
                enchantCell.Clear();//Очистить ячейку
                Open();
                break;
            case EnchantCommand.CloseGUI://Close Enchant
                Close();
                break;
            case EnchantCommand.Continue:
                Open();
                break;
            case EnchantCommand.Completed://Результат заточки
                int answer = nw.ReadByte();
                if (answer > 0) textInfo.text = "Вы успешно улучшили предмет до +" + answer;
                else textInfo.text = "Неудачная попытка модификации предмета";
                button.onClick.RemoveAllListeners();//Очистить кнопку
                button.onClick.AddListener(Next);//Назначить метод продолжить
                button.interactable = true;
                textButton.text = "Продолжить";
                break;
        }
    }

    public void Refresh()
    {
        enchantCell.Refresh();
    }

    private void Open()
    {
        if (!canvas.enabled) InventoryWindow.RegisterUpdate(Refresh);
        canvas.enabled = true;
              uISort.PickUp(canvas);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Sharpen);
        button.interactable = true;
        
        textButton.text = "Заточить";
        textInfo.text = "";
        indicator.SetActive(false);
    }
    private void Close()
    {
        if (canvas.enabled) InventoryWindow.UnregisterUpdate(Refresh);
        canvas.enabled = false;
        enchantCell.PutItem(null);
    }

    public void Sharpen()
    {
        if (enchantCell.IsEmpty()) return;
        button.interactable = false;


        StartCoroutine(IESharpen());
       
    }

    private IEnumerator IESharpen()
    {
        indicator.SetActive(true);

        float time = 0.0f;
        bar.fillAmount = 0.0f;

        while (time < 1.0f)
        {
            yield return 0;
            time += Time.deltaTime;
            bar.fillAmount =  time;
        }
        //TODO msg
        //Packet nw = new Packet(Channel.Discard);
        //nw.WriteType(Types.ItemEnchant);
        //nw.WriteByte((byte)EnchantCommand.Completed);//Заточить
        //nw.WriteInt(enchantCell.GetObjectID());
        //NetworkManager.Send(nw);
    }

    public void Next()
    {
    //TODO msg
        //Packet nw = new Packet(Channel.Discard);
        //nw.WriteType(Types.ItemEnchant);
        //nw.WriteByte((byte)EnchantCommand.Continue);//Продолжить
        //NetworkManager.Send(nw);

        button.interactable = false;
    }

    public void Exit()
    {
    //TODO msg
        //Packet nw = new Packet(Channel.Discard);
        //nw.WriteType(Types.ItemEnchant);
        //nw.WriteByte((byte)EnchantCommand.CloseGUI);//Закрыть интерфейс заточки
        //NetworkManager.Send(nw);
    }

    private void OnDestroy()
    {
        networkManager?.UnregisterHandler(Network.Core.Types.ItemEnchant);
    }
}
