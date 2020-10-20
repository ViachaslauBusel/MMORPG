using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class ErrorCreator : MonoBehaviour
    {

        private static Text txt_error;

        private void Start()
        {
            txt_error = GetComponent<Text>();
        }


        public static void ShowError(int id)
        {
            if (txt_error == null) return;

            switch (id)
            {
                case 1: //Имя не должно содержать меньше 3 или больше 30 символов = 1;
                    txt_error.text = "Имя не должно содержать меньше 3 или больше 30 символов";
                    break;
                case 2: //Имя уже используется
                    txt_error.text = "Имя уже используется";
                    break;
                case 3:
                    txt_error.text = "Ошибка выбора корабля";
                    break;
            }
        }
    }
}