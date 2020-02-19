using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuestsRedactor
{
    [System.Serializable]
    public class Answer
    {
        public ConnectionPoint inLeft, inRight;
        public int idStage = -1;//ИД звена(уровня) на который ссылается этот ответ. -1 этот не свзяан не скаким звеном


        public Answer()
        {
            inLeft = new ConnectionPoint(this, ConnectionPointType.Out, ConnectionDirection.Left);
            inRight = new ConnectionPoint(this, ConnectionPointType.Out, ConnectionDirection.Right);
        }

        public bool Draw()
        {
            bool delet = false;

            GUILayout.BeginHorizontal();
            GUILayout.Space(15);
            inLeft.Draw();
            GUILayout.FlexibleSpace();
            QuestStage stage =  WindowQuests.Instance.selectQuest.FindStage(idStage);
            GUILayout.Label(stage == null? "unknown" : stage.title);
            //Кнопка удалить
            if (GUILayout.Button("", QuestStyle.ButtonDelet, GUILayout.Width(15), GUILayout.Height(15)))
            {
                delet = true;
            }
            GUILayout.FlexibleSpace();
            inRight.Draw();
            GUILayout.Space(15);
            GUILayout.EndHorizontal();

           
            return delet;
            /*GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(EditorGUIUtility.IconContent("d_P4_AddedLocal"), EditorStyles.miniButtonMid, GUILayout.Width(165.0f), GUILayout.Height(25.0f)))
            {
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();*/
        }
        public void DrawBezier()
        {
            WindowConnections.Draw(this);
        }
    }
}