#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Redactor
{
    public class TextWrapper
    {
        public static void Label(string text, float width, int line)
        {
            width -= 13.0f;
            string[] word = text.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (word.Length == 0) return;

            int draw = 0;
            string lineStr = word[0];
            for(int i=1; i<word.Length; i++)
            {
                if (GUI.skin.label.CalcSize(new GUIContent(lineStr + word[i])).x > width)
                {
                    if (++draw < line)
                    { Draw(lineStr, width); lineStr = word[i]; }
                    else
                        return;
                }
                else
                { lineStr += " " + word[i]; }
            }
            Draw(lineStr, width);
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