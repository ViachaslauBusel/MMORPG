using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            GetGuest(1);
        }
        public  void GetGuest(int id)
        {
         //   StartCoroutine(LoadGuest(id));
        }

       /* private IEnumerator LoadGuest(int id)
        {
          //ResourceRequest request =  Resources.LoadAsync("Quests/" + id);
          //  yield return request;
          //  Debug.Log(request.asset.GetType());
           // Debug.Log(JsonUtility.FromJson<Quest>((string)).title);
        }*/
    }
}