#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
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

        public static void Save(QuestsList questsList)
        {
            string folder = "/Resources/Quests";
            string path = Application.dataPath + folder;
            if (Directory.Exists(path))
            { DeleteDirectory(path); }

            Directory.CreateDirectory(path);
            path += "/";

            foreach(Quest _q in questsList.quests)
            {
                Quests.Quest quest = new Quests.Quest();
                quest.id = _q.id;
                quest.title = _q.title;
                foreach(QuestStage _s in _q.stages)
                {
                    Quests.Stage stage = new Quests.Stage();
                    stage.id = _s.id;
                    stage.title = _s.title;
                    stage.descripton = _s.descripton;
                    foreach(Answer _a in _s.answers)
                    {
                        stage.answers.Add(_a.idStage);
                    }
                    quest.stages.Add(stage);
                }
                SaveFile(folder, quest);
            }
        }

        public static void SaveFile(string path, Quests.Quest quest)
        {
           // Quests.Quest asset = ScriptableObject.CreateInstance<Quests.Quest>();
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets"+path + "/" + quest.id + ".asset");
            AssetDatabase.CreateAsset(quest, assetPathAndName);
                /*
            string file = JsonUtility.ToJson(quest);

            using(StreamWriter writer = new StreamWriter(path + quest.id))
            {
                writer.Write(file);
            }*/
        }

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
    }
}
#endif