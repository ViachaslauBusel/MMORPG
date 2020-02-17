#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Redactor
{
    public class TextWrapper
    {
        /// <summary>
        /// Отрисовывает текст построчно. С шириной сторк width. Число строк = line
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <param name="line"></param>
        public static void Label(string text, float width, int line)
        {
            if (text == null) return;
            width -= 13.0f;
            //Массив слов
            string[] word = text.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            int wordIndex = 0;
            string lineStr;
            for (int i = 0; i < line; i++)
            {
                lineStr = "";
                for (; wordIndex < word.Length; wordIndex++)
                {
                    //Если ширина строки больше нужной, выйти из цикла for
                    if (GUI.skin.label.CalcSize(new GUIContent(lineStr + word[wordIndex])).x > width)
                    {
                        break;
                    }
                    else//Если нет, добавить слово к строке
                    { lineStr += " " + word[wordIndex]; }
                }


                Draw(lineStr, width);
            }
        }

        private static void Draw(string text, float width)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(8.0f);
            GUILayout.Label(text);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}
#endif