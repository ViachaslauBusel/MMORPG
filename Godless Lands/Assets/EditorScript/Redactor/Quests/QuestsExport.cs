#if UNITY_EDITOR
using Quests;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace QuestsRedactor {
    public class QuestsExport
    {
        public static void Export(QuestsList recipesList)
        {
            using (BinaryWriter stream_out = new BinaryWriter(File.Open(@"Export/quests.dat", FileMode.Create)))
            {
                foreach (Quest _quest in recipesList.quests)
                {
                    stream_out.Write(_quest.title);
                    stream_out.Write((int)_quest.id);
                }
            }
        }
    }
}
#endif