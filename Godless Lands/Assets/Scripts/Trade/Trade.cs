using Cells;
using RUCP;
using RUCP.Handler;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Trade : MonoBehaviour
{
    
   
    public Inventory inventory;
    public GameObject tradeCell;//Префаб ячейки
    public GameObject offerCell;
    public Transform parent_myBag;
    public Transform parent_myOffer;
    public Transform parent_otherOffer;
    public GameObject trade;
    public GameObject tradeRequest;
    public Text myName;
    public Text OtherName;
    public Image MyHidePanel;
    public Image OtherHidePanel;
    private TradeCell[] items;
    private OfferCell[] myOfferItems;
    private OfferCell[] otherOfferItem;
    private int myIndex = 0;
    private int otherIndex = 0;
    private NetworkManager networkManager;

    [Inject]
    private void Construt(NetworkManager networkManager)
    {
        this.networkManager = networkManager;

        networkManager.RegisterHandler(Types.OfferTrade, OfferTrade);
        networkManager.RegisterHandler(Types.ConfirmTrade, ConfirmTrade);
        networkManager.RegisterHandler(Types.ItemTrade, ItemTrade);
    }



    private void ItemTrade(Packet nw)//Список предметов предложеных на обмен
    {
        ClearMyTradeCell(); ClearOtherTradeCell();//Очистить все ячейки

        while(nw.AvailableBytesForReading > 0)
        {
            int id = nw.ReadInt();//ID предмета
            int count = nw.ReadInt();//Количество предметов
            bool mine = nw.ReadBool();//true - предмет принадлежит этому игроку
          //  print("Mine?: " + mine);
            if (mine) PutMyOfferItem(id);
            else PutOtherOfferItem(id, count);
        }
    }

    private void PutMyOfferItem(int id)
    {
        myOfferItems[myIndex++].PutItem(inventory.itemsList.GetDuplicateItem(id));
    }
    private void PutOtherOfferItem(int id, int count)
    {
        otherOfferItem[otherIndex++].PutItem(inventory.itemsList.GetDuplicateItem(id));
    }

    private void ClearMyTradeCell()//Очистить все ячейки этого игрока
    {
        myIndex = 0;
        for (int i = 0; i < myOfferItems.Length; i++)
        {
            myOfferItems[i].PutItem(null);
        }

      
    }
    private void ClearOtherTradeCell()//Очистить все ячейки другого игрока
    {
        otherIndex = 0;
        for (int i = 0; i < otherOfferItem.Length; i++)
        {
            otherOfferItem[i].PutItem(null);
        }
    }

    public void SendConfirmTrade() //1 - Подтвердить обмен//Кнопка ДА в окне
    {
        MyHidePanel.enabled = true;
        SendTradeCommand(1);
    }
    public void SendCancelTrade()//2 - Отменить обмен
    {
        SendTradeCommand(2);
        trade.SetActive(false);
    }

    //1 - Подтвердить обмен
    //2 - Отменить обмен
    private void SendTradeCommand(byte command)
    {
    //TODO msg
        //Packet nw = new Packet(Channel.Reliable);
        //nw.WriteType(Types.ConfirmTrade);
        //nw.WriteByte((byte)command);
        //NetworkManager.Send(nw);
    }

    //1 - открыть окно обмена
    //2 - подтверждение обмена от другого игрока
    //3 - Закрытие окна обмена
    private void ConfirmTrade(Packet nw)//Подтверждение о начале обмена, или подтверждение об завершении или отмене обмена
    {
        int comand = nw.ReadByte();
        switch (comand)
        {
            case 1:
                myName.text = nw.ReadString();//Имя этого игрока
                OtherName.text = nw.ReadString();//Имя игрока с которым начат обмен
                MyHidePanel.enabled = false;
                OtherHidePanel.enabled = false;
                trade.SetActive(true);//Открыть окно обмена
                tradeRequest.SetActive(false);//Закрыть окно приглашение на обмен
                OpenTrade();//Заполнить окно обмена пустыми ячейками
                break;
            case 2:
                OtherHidePanel.enabled = true;
                break;
            case 3:
                trade.SetActive(false);
                tradeRequest.SetActive(false);
                break;
        }
    }

    private void OfferTrade(Packet nw)//Открытие окна запроса на обмен
    {
        float timeOffer = nw.ReadFloat();
        string offerName = nw.ReadString();
        tradeRequest.SetActive(true);
        tradeRequest.GetComponent<TradeRequest>().StartTimer(timeOffer, offerName);
    }

    private void Start()
    {
        trade.SetActive(false);
        tradeRequest.SetActive(false);
       
    }

    private void CleatTrade()//Удалить все ячейки инвенторя
    {
        foreach(TradeCell tradeCell in items)
        {
            Destroy(tradeCell.gameObject);
        }
    }
    public void OpenTrade()
    {
        if(items != null) CleatTrade();//Удалить все ячейки инвенторя
        //Заполнить левый Scroll view  содержимым инвентаря ->
        ItemCell[] inventoryItems = inventory.GetCellItems();
        items = new TradeCell[inventoryItems.Length];

        for(int i=0; i<items.Length; i++)
        {
            GameObject _obj = Instantiate(tradeCell);
            _obj.transform.SetParent(parent_myBag);
            TradeCell _tradeCell = _obj.GetComponent<TradeCell>();
            _tradeCell.PutItemCell(inventoryItems[i]);
            items[i] = _tradeCell;
        }
        //Заполнить левый Scroll view  содержимым инвентаря <-


        if (myOfferItems != null)//Если ячейки уже существую очистить, если нет создать
        {
            ClearMyTradeCell();
        }
        else
        {
            myOfferItems = new OfferCell[50]; //Максимум 50 предлогаемых вещей

            for (int i = 0; i < myOfferItems.Length; i++)
            {
                GameObject _obj = Instantiate(offerCell);
                _obj.transform.SetParent(parent_myOffer);
                OfferCell _offerCell = _obj.GetComponent<OfferCell>();
                _offerCell.PutItem(null);
                _offerCell.SetOpen(true);
                myOfferItems[i] = _offerCell;
            }
        }
        //Заполнить правый верхний Scroll view  пустыми ячейками <-

        //Заполнить правый нижний Scroll view  пустыми ячейками ->
        if (otherOfferItem != null)//Если ячейки уже существую очистить, если нет создать
        {
            ClearOtherTradeCell();
        }
        else
        {
            otherOfferItem = new OfferCell[50]; //Максимум 50 предложеных вещей

            for (int i = 0; i < otherOfferItem.Length; i++)
            {
                GameObject _obj = Instantiate(offerCell);
                _obj.transform.SetParent(parent_otherOffer);
                OfferCell _offerCell = _obj.GetComponent<OfferCell>();
                _offerCell.SetOpen(false);
                otherOfferItem[i] = _offerCell;
            }
        }
        //Заполнить правый нижний Scroll view  пустыми ячейками <-
    }

    public void UseSkill()
    {
    //TODO msg
        //Packet nw = new Packet(Channel.Reliable);
        //nw.WriteType(Types.OfferTrade);
        //nw.WriteByte((byte)1);
        //NetworkManager.Send(nw);
    }

    private void OnDestroy()
    {
        networkManager?.UnregisterHandler(Types.OfferTrade);
        networkManager?.UnregisterHandler(Types.ConfirmTrade);
        networkManager?.UnregisterHandler(Types.ItemTrade);
    }
}
