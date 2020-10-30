using Items;
using RUCP;
using RUCP.Packets;
using RUCP.Tools;
using UnityEngine;
using UnityEngine.UI;
using Walkers;

namespace Characters
{
    public class Character : MonoBehaviour, TargetObject
    {

        private Text text_name;
        public int ID;

        private Armor armor;

        private AnimationSkill animationSkill;



        public MovementController Controller { get; private set; }

        public void Initialize()
        {
            animationSkill = GetComponent<AnimationSkill>();

            Controller = GetComponent<MovementController>();

        }

        public void SetCombatState(bool state)
        {
            armor.SetCombatstate(state);
        }
        public void SetArmor(Packet nw)
        {
            armor = GetComponent<Armor>();
            armor.Init();
            while (nw.AvailableBytes >= 8)
            {
                UpdateArmor(nw);
            }
        }
        public void UpdateArmor(Packet nw)
        {
            ItemType type = (ItemType)nw.ReadInt();
            ArmorPart part = (ArmorPart)nw.ReadInt();
            int id_item = nw.ReadInt();
            Item _item = Inventory.CreateItem(id_item);
            armor.PutItem(type, _item);
        }


        public void SetName(string char_name)
        {
            text_name = GetComponentInChildren<Text>();
            text_name.text = char_name;
        }

     


        public void OnTarget()
        {

        }

        public void OffTarget()
        {

        }

        public string GetName()
        {
            return text_name.text;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public int Layer()
        {
            return 1;
        }

        public int Id()
        {
            return ID;
        }

        public bool IsAlive()
        {
            return true;
        }

        public Vector3 GetPosition() => transform.position;
    }
}