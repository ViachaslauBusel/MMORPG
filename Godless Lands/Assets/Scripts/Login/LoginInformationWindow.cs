using Protocol.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginInformationWindow : MonoBehaviour {

    [SerializeField] Text info_text;
    [SerializeField] TMP_Text m_textWaitWindow;
    [SerializeField] GameObject m_waitWindow, m_infoWindow;
    private Canvas m_canvas;

    private void Awake()
    {
        m_canvas = GetComponent<Canvas>();
        m_canvas.enabled = false;
    }

    public void Wait(string text)
    {
        m_textWaitWindow.text = text;
        m_waitWindow.SetActive(true);
        m_infoWindow.SetActive(false);
        m_canvas.enabled = true;
    }
    public void ShowInfo(LoginInformationCode code)
    {
        m_waitWindow.SetActive(false);
        m_infoWindow.SetActive(true);
        switch (code)
        {
            case LoginInformationCode.WrongVersion:
                info_text.text = "Версия клиента не соответствует серверу";
                break;
            case LoginInformationCode.WrongLogin:
                info_text.text = "Логин не должен содержать меньше 3 или больше 30 символов";
                break;
            case LoginInformationCode.AuthorizationFail:
                info_text.text = "Логин или пароль неверны";
                break;
            case LoginInformationCode.AuthorizationSuccessful:
                break;
            case LoginInformationCode.LoginExist:
                info_text.text = "Ошибка логин уже используется";
                break;
            case LoginInformationCode.RegistrationSuccessful:
                info_text.text = "Регистрация успешно завершена";
                break;
            case LoginInformationCode.ConnectionFail:
                info_text.text = "Не удалось установить соеденение";
                break;
            case LoginInformationCode.AlreadyInGame:
                info_text.text = "Вы уже в игре";
                break;
            case LoginInformationCode.ServerNotReady:
                info_text.text = "Сервер не готов";
                break;
            default:
                info_text.text = "Неизвестная ошибка";
                break;
        }

       // info_text.text = "Ожидание подключения";
      //  info_text.text = "Пароль не должен содержать меньше 3 или больше 30 символов";
       
      
        m_canvas.enabled = true;
    }

    public void HideInfo()
    {
        m_canvas.enabled = false;
    }
}
