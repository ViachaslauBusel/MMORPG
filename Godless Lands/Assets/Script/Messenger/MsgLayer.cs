using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Messenger
{
    public enum MsgLayer
    {
        System = 0,//Системные сообщения
        DamageIn = 1,//Полученный урон
        DamageOut = 2,//Нанесенный урон
        Drop = 3,//Получение предметов
        AroundMsg = 4,//Сообщения от игроков в радиусе видимости
        AllMsg = 5//Сообщения от игроков на весь сервер
    }
}