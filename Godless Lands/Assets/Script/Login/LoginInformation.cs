using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginInformation : MonoBehaviour {

    private static Text info_text;
    private static Canvas canvas;

    private void Awake()
    {
        info_text = GameObject.Find("TextInformation").GetComponent<Text>();
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    /// <summary>
    /// 1 - Не удалось установить соеденение
    /// 2 - Логин должен содержать не более 30 символов
    /// 3 - Пароль должен содержать не более 30 символов
    /// </summary>
    /// <param name="id"></param>
    public static void ShowInfo(int id)
    {
        switch (id)
        {
            case 1:
                info_text.text = "Не удалось установить соеденение";
                break;
            case 2:
                info_text.text = "Логин не должен содержать меньше 3 или больше 30 символов";
                break;
            case 3:
                info_text.text = "Пароль не должен содержать меньше 3 или больше 30 символов";
                break;
            case 4:
                info_text.text = "Ошибка логин уже используется";
                break;
            case 5:
                info_text.text = "Регистрация успешно завершена";
                break;
            case 6:
                info_text.text = "Версия клиента не соответствует серверу";
                break;
            case 7:
                info_text.text = "Логин или пароль неверны";
                break;
            default:
                info_text.text = "Неизвестная ошибка";
                break;
        }
        canvas.enabled = true;
    }

    public void HideInfo()
    {
        canvas.enabled = false;
    }
}
