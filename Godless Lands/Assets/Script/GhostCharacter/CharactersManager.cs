using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RUCP;
using System;
using System.Text;
using Items;
using RUCP.Packets;

public class CharactersManager : MonoBehaviour, Manager {

    public GameObject shipObject;
     private static Dictionary<int, GhostCharacter> characters; //логин ид, GhostShip

    private void Awake()
    {
        HandlersStorage.RegisterHandler(Types.CharacterCreate, CharacterCreate);
        HandlersStorage.RegisterHandler(Types.CharacterMove, CharacterMove);
        HandlersStorage.RegisterHandler(Types.CharacterDelete, CharacterDelete);
        HandlersStorage.RegisterHandler(Types.CharacterAnim, CharacterAnim);
        HandlersStorage.RegisterHandler(Types.GhostUpdateArmor, GhostUpdateArmor);
        HandlersStorage.RegisterHandler(Types.CharacterRotation, CharacterRotation);
        HandlersStorage.RegisterHandler(Types.CharacterEndMove, CharacterEndMove);
        HandlersStorage.RegisterHandler(Types.CharacterCombatState, CharacterCombatState);
      //  RegisteredTypes.RegisterTypes(Types.CharacterDead, CharacterDead);
    }

   /* private void CharacterDead(NetworkWriter nw)
    {
        int login_id = nw.ReadInt();
        int corpse_id = nw.ReadInt();
        print("Transfer: " + login_id);
        if (characters.ContainsKey(login_id))
        {
            CorpsesManager.AddUnconfirmedBode(corpse_id, characters[login_id].gameObject);
            characters.Remove(login_id);
        }
        else { print("Error not found character"); }
    }*/

    public static GameObject FindChar(int id)
    {
        try
        {
            GameObject obj = characters[id].gameObject;
            characters.Remove(id);
            return obj;
        }
        catch(KeyNotFoundException e)
        {
            return null;
        }
      
    }

    public void AllDestroy()
    {
        foreach (GhostCharacter ghost in characters.Values)
            Destroy(ghost.gameObject);
        characters.Clear();
       
    }
    private void CharacterCombatState(Packet nw)
    {

        int id_login = nw.ReadInt();

        if (characters.ContainsKey(id_login))
        {
            characters[id_login].SetCombatState(nw.ReadBool());
        }
    }

    private void CharacterEndMove(Packet nw)
    {
        int id_login = nw.ReadInt();

        if (characters.ContainsKey(id_login))
        {
            Vector3 pos = nw.ReadVector3();
            int indicator = nw.ReadByte();
            float rot = nw.ReadFloat();

            characters[id_login].EndPosition(pos, indicator);

            characters[id_login].NextRotation(rot);


        }
    }
    private void CharacterMove(Packet nw)
    {
        int id_login = nw.ReadInt();

        if (characters.ContainsKey(id_login))
        {
            Vector3 pos = nw.ReadVector3();
            int indicator = nw.ReadByte();
            int moveIndex = nw.ReadByte();
            float rot = nw.ReadFloat();
          
            characters[id_login].NextPosition(pos, indicator, moveIndex);

            characters[id_login].NextRotation(rot);


        }
        else
        {
            print("delet character not found: " + id_login);
        }
    }
    private void CharacterRotation(Packet nw)
    {
        int id_login = nw.ReadInt();

        if (characters.ContainsKey(id_login))
        {

            characters[id_login].NextRotation(nw.ReadFloat());

        }
    }

    private void GhostUpdateArmor(Packet nw)
    {
        int id_login = nw.ReadInt();

        if (characters.ContainsKey(id_login))
        {
            characters[id_login].UpdateArmor(nw);
        }
    }

    private void CharacterAnim(Packet nw)
    {
        int id_login = nw.ReadInt();
//print("use anim");
        if (characters.ContainsKey(id_login))
        {
            int layer = nw.ReadByte();
            int anim = nw.ReadInt();
            //  print("anim: "+anim);
            switch (layer)
            {
                case 1: //layer 1 = Проиграть анимацию умений с контролем времени
                    int milliseconds = nw.ReadInt();
                    characters[id_login].GetComponent<AnimationSkill>().UseAnimationSkill(anim, (milliseconds / 1000.0f));
                    break;
                case 2://layer 2 = Проиграть анимацию состояния без контроля времени
                    characters[id_login].GetComponent<AnimationSkill>().UseAnimState(anim);
                    break;
                case 3: //layer 3 = Проиграть анимацию состояния с контролем времени
                    int timeMilli = nw.ReadInt();
                    characters[id_login].GetComponent<AnimationSkill>().UseAnimState(anim, (timeMilli / 1000.0f));
                    break;
            }
        }
    }

    void Start () {
        characters = new Dictionary<int, GhostCharacter>(100);

      
    }

    public static TargetObject GetCharacter(int id)
    {
        if(characters.ContainsKey(id))
        return characters[id];
        else return null;
    }

    private void CharacterCreate(Packet nw)
    {
        int login_id = nw.ReadInt();
        string char_name = nw.ReadString();

        print("Character create: " + char_name);

        if (!characters.ContainsKey(login_id))
        {

                GameObject obj = Instantiate(shipObject, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(transform);
            GhostCharacter ship_obj = obj.GetComponent<GhostCharacter>();
            ship_obj.SetStartPosition(nw.ReadVector3(), nw.ReadByte(), nw.ReadByte());
            ship_obj.NextRotation(nw.ReadFloat());
                 ship_obj.SetName(char_name);
                 ship_obj.ID = login_id;
            ship_obj.SetArmor(nw);

            ship_obj.SetCombatState(nw.ReadBool());

            characters.Add(login_id, ship_obj);
        }
        else
        {
            print(char_name + " персонаж уже был загружен");
        }
    }

    private void CharacterDelete(Packet nw)
    {
        int login_id = nw.ReadInt();
        print("Delet: " + login_id);
        if (characters.ContainsKey(login_id))
        {
            GameObject.Destroy(characters[login_id].gameObject);
            characters.Remove(login_id);
        }else { print( "Error not found character"); }
    }

   

    private void OnDestroy()
    {
        HandlersStorage.UnregisterHandler(Types.CharacterCreate);
        HandlersStorage.UnregisterHandler(Types.CharacterMove);
        HandlersStorage.UnregisterHandler(Types.CharacterDelete);
        HandlersStorage.UnregisterHandler(Types.CharacterAnim);
        HandlersStorage.UnregisterHandler(Types.GhostUpdateArmor);
        HandlersStorage.UnregisterHandler(Types.CharacterRotation);
        HandlersStorage.UnregisterHandler(Types.CharacterEndMove);
        HandlersStorage.UnregisterHandler(Types.CharacterCombatState);
    }

   
}
