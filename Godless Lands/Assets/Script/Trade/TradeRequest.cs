﻿using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeRequest : MonoBehaviour
{

    public Text offer_txt;
    public Image img_timer;
    private float timer;
    

    public void StartTimer(float timer, string offerName)
    {
        this.timer = timer;
        offer_txt.text = offerName + " предлагает сделку. Принять?";
        StartCoroutine(IETimer());
    }

    public void ConfirmTrade()
    {
        NetworkWriter nw = new NetworkWriter(Channels.Reliable);
        nw.SetTypePack(Types.OfferTrade);
        nw.write((byte)2);
        NetworkManager.Send(nw);
    }

    public void HideTradeRequest()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator IETimer()
    {
        float _timer = timer;
        while (_timer > 0.0f)
        {
            yield return null;
            _timer -= Time.deltaTime;
            img_timer.fillAmount = _timer / timer;
        }
        gameObject.SetActive(false);
    }
}
