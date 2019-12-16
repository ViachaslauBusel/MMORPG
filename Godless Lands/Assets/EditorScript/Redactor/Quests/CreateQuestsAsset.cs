#if UNITY_EDITOR
using Quests;
using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuestsRedactor
{
    public class CreateQuestsAsset
    {
        [MenuItem("Assets/Create/QuestsList")]
        public static void Create()
        {
            CreateAsset.Create<QuestsList>("QuestsList");
        }
    }
}
#endif