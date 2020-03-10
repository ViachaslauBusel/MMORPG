using Cells;
using RUCP;
using RUCP.Handler;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        uISort = GetComponentInParent<UISort>();
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        enchantCell = GetComponentInChildren<EnchantCell>();
        indicator.SetActive(false);

        RegisteredTypes.RegisterTypes(Types.ItemEnchant, Enchant);
    }

    private void Enchant(NetworkWriter nw)
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
        if (!canvas.enabled) Inventory.RegisterUpdate(Refresh);
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
        if (canvas.enabled) Inventory.UnregisterUpdate(Refresh);
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

        NetworkWriter nw = new NetworkWriter(Channels.Reliable | Channels.Discard);
        nw.SetTypePack(Types.ItemEnchant);
        nw.write((byte)EnchantCommand.Completed);//Заточить
        nw.write(enchantCell.GetObjectID());
        NetworkManager.Send(nw);
    }

    public void Next()
    {

        NetworkWriter nw = new NetworkWriter(Channels.Reliable | Channels.Discard);
        nw.SetTypePack(Types.ItemEnchant);
        nw.write((byte)EnchantCommand.Continue);//Продолжить
        NetworkManager.Send(nw);

        button.interactable = false;
    }

    public void Exit()
    {
        NetworkWriter nw = new NetworkWriter(Channels.Reliable | Channels.Discard);
        nw.SetTypePack(Types.ItemEnchant);
        nw.write((byte)EnchantCommand.CloseGUI);//Закрыть интерфейс заточки
        NetworkManager.Send(nw);
    }

    private void OnDestroy()
    {
        RegisteredTypes.UnregisterTypes(Types.ItemEnchant);
    }
}
