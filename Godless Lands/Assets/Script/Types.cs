using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Types {

    public const short CloseConnection = 1;// S->S Метод выполняемы во время отключения игрока 
    public const short LobbyEntrance = 2; //c->s,s->c Вход игрока в общую комнату
    public const short MyCharacters = 3; //C->S запрос на получение списка персонажей, S->C Список персонажей
    public const short OwnCharacterCreate = 4; //C->S->C Отправка на сервер запроса на создание персонажа и отпрака обратно подтверждение или ошибки
    public const short CharacterCreate = 5; //S ->C отправка данных о других кораблях в системе
    public const short MapEntrance = 6; //C->S Запрос на получение информации о выбраном персонаже S->C Информация о выбраном персонаже
    public const short SelectCharacter = 7; // C->S Выбор персонажа в лоби // S->C Ответ от сервера
    public const short Login = 8;  //C->S отпровляет логин и пароль на сервер
    public const short Registration = 9; //C->S отпровляет логин и пароль  для регистрации акаунта
    public const short LoginInfo = 10; //S->C  Отпровляет код ошибки или успешного выполнение запроса
    public const short LoginOk = 11; //S->C Логин успешен
    public const short ServersList = 12; //C->S запрос на получение списка серверов S->C Список серверов
    public const short Move = 13;  //C->S Передвижение персонажа на карте
    public const short CharacterMove = 14; //S->C Передвижение других игроков
    public const short CharacterDelete = 15; //S->C Отпровляет ид с отключенным клиентом
    public const short ChatMessage = 16; // C-S отправка сообщение на сервер S->C рассылка всем игрокам
    public const short Target = 17;//C->S Запрос на взятие цели по ид. S->C ответ о возможности взять в цель
    public const short MonsterCreate = 18;//S-C отправка на клиент данных для создания монстра
    public const short MonsterDelete = 19;//S-C отправка на клиент данных для удаление монстра
    public const short Skill = 20;//C->S Запрос на использвония умения.
    public const short CharacterAnim = 21;
    public const short TeleportToPoint = 22;
    public const short MonsterAnim = 23;
    public const short TargetUpdate = 24;
    public const short HPViewUpdate = 25;//0-load name hp mp stamina/ 1 - hp mp stamina / 2 - hp/ 3 - mp/ 4 - stamina
    public const short PlayerDead = 26;//S->C отпрвка игроку
    public const short PlayerResurrection = 27;
    public const short LoadInventory = 28;//C->S запрос на получение содержимого сумки S->C содержимое сумки
    public const short FindDrop = 29;
    public const short TakeDrop = 30;
    public const short LoadStats = 31;
    public const short UpdateInventory = 32;
    public const short UseItem = 33;
    public const short UpdateArmor = 34;
    public const short TakeOffArmor = 35;
    public const short GhostUpdateArmor = 36;
    public const short DeletItem = 37;
    public const short UpdateStats = 38;
    public const short Rotation = 39; //C->S Вращение персонажа на карте
    public const short CharacterRotation = 40; //S->C Вращение других игроков
    public const short EndMove = 41; //S->C Точка в которой остановился игрок
    public const short CharacterEndMove = 42; //S->C Точка в которой остановились другие игроки
    public const short CombatState = 43; //Переключение боевого состояния 
    public const short CharacterCombatState = 44;
    public const short PlayerAnim = 45;
    public const short OfferTrade = 46;
    public const short ConfirmTrade = 47;
    public const short ItemTrade = 48; //C1-S Выставить предмет на обмен S-C2 показать выставленные предметы
    public const short LobbyReload = 49; //Перезаходи в лобби
    public const short TestRay = 50;
    public const short MonsterMove = 51;
    public const short MonsterEndMove = 52;
    public const short PlayAnimSkill = 53;
    public const short loadBar = 54;
    public const short updateBarCell = 55;
    public const short UseItemByKey = 56;
    public const short WrapBarCell = 57;
    public const short WrapItem = 58;
    public const short ResourceCreate = 59;//S-C отправка на клиент данных для создания ресурса
    public const short ResourceDelete = 60;//S-C отправка на клиент данных для удаление ресурса
    public const short ResourceUse = 61;
    public const short LoadArmor = 62;
    public const short RecipeUse = 63;
    public const short MachineUse = 64;
    public const short MachineCreate = 65;
    public const short MachineDelete = 66;
    public const short MachineAddComponent = 67;
    public const short MachineRemoveComponent = 68;
    public const short MachineClose = 69;
    public const short ProfessionUpdate = 70;
    public const short ProfessionLoad = 71;

}
