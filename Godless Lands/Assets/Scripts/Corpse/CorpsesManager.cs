using Characters;
using RUCP;
using RUCP.Handler;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CorpsesManager : MonoBehaviour, Manager
{
    public GameObject prefPlayer;
  //  private static Dictionary<int, GameObject> unconfirmedBody;
    private static Dictionary<int, GameObject> corpses;
    private NetworkManager networkManager;


    [Inject]
    private void Construct(NetworkManager networkManager)
    {
        this.networkManager = networkManager;
        networkManager.RegisterHandler(Types.CorpseCreate, CorpseCreate);
        networkManager.RegisterHandler(Types.CorpseDelete, CorpseDelete);
        networkManager.RegisterHandler(Types.CorpseUpdate, CorpseUpdate);
    }

    private void Awake()
    {
     //   unconfirmedBody = new Dictionary<int, GameObject>();
        corpses = new Dictionary<int, GameObject>();

    }

    private void CorpseUpdate(Packet nw)
    {
        int corpse_id = nw.ReadInt();
        int leftTime = nw.ReadInt() - (int)networkManager.Client.Statistic.Ping;
        print("left time: " + ((leftTime / 1000.0f) / 60.0f) + "min");
        if (corpses.ContainsKey(corpse_id))
        {
            Corpse corpse = corpses[corpse_id].GetComponent<Corpse>();
            corpse.SetEndTime(leftTime);
        }
       
    }

    private void CorpseDelete(Packet nw)
    {
        int corpse_id = nw.ReadInt();
        Destroy(corpses[corpse_id]);
        corpses.Remove(corpse_id);
    }

    private void CorpseCreate(Packet nw)
    {
        print("create body");
        int corpse_id = nw.ReadInt();
        int char_id = nw.ReadInt();
        string char_name = nw.ReadString();
        Vector3 position = Vector3.zero; // TDOD  nw.ReadVector3();
        float rotation = nw.ReadFloat();
        int weapon = nw.ReadInt();
        int leftTime = nw.ReadInt() - (int)networkManager.Client.Statistic.Ping;
        print("left time: " + ((leftTime / 1000.0f) / 60.0f) + "min");
        /*  List<Item> items = new List<Item>();
          while(nw.AvailableBytes >= 4)
          {
              items.Add(Inventory.CreateItem(nw.ReadInt()));
          }*/

        GameObject obj = CharactersManager.FindChar(char_id);
      //  unconfirmedBody.TryGetValue(corpse_id, out obj);
        print("find key: " + corpse_id);
        if (obj == null)
        {
            print("not find");
            obj = Instantiate(prefPlayer);
           
            obj.transform.position = position;
        }
        else
        {
            print("find");

            if (Vector3.Distance(obj.transform.position, position) > 1.5f)
                obj.transform.position = position;
        }
        obj.GetComponent<Character>().SetName(char_name);
        InitBody(obj);
        corpses.Add(corpse_id, obj);

        obj.transform.SetParent(transform);
        obj.layer = 14;//Cursor take drop

        Destroy(obj.GetComponent<Character>());
        Corpse corpse = obj.AddComponent<Corpse>();
        corpse.id = corpse_id;
        corpse.SetEndTime(leftTime);
    }

    public void AllDestroy()
    {
     /*   foreach (GameObject obj in unconfirmedBody.Values)
            Destroy(obj);
        unconfirmedBody.Clear();*/

        foreach (GameObject obj in corpses.Values)
            Destroy(obj);
        corpses.Clear();

    }

    public static void InitBody(GameObject body)
    {
       
        body.GetComponent<AnimationSkill>().DeadOn();
        body.GetComponent<CharacterController>().enabled = false;


        SphereCollider sphere = body.AddComponent<SphereCollider>();
        sphere.radius = 1.0f;
        sphere.isTrigger = true;
    }

   /* public static void AddUnconfirmedBode(int id, GameObject body)
    {
        if(unconfirmedBody.ContainsKey(id)) { Destroy(body); return; }

        unconfirmedBody.Add(id, body);

        InitBody(body);
        print("add key: " + id);
        body.AddComponent<DelayDestroy>().StartTimer(4.0f);

    }*/

    private void OnDestroy()
    { 
        networkManager.UnregisterHandler(Types.CorpseCreate);
        networkManager.UnregisterHandler(Types.CorpseDelete);
    }
}
