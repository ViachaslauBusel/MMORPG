using UnityEngine;
using UnityEngine.UI;
using Walkers;

namespace Characters
{
    public class Character : MonoBehaviour, ITargetObjectLegacy
    {

        private Text text_name;
        public int ID { get; private set; }

        public CharacterArmor Armor { get; private set; }

        private AnimationSkill animationSkill;



        public MovementController Controller { get; private set; }

        public void Initialize(string charName, int charID)
        {
            SetName(charName);

            ID = charID;

            animationSkill = GetComponent<AnimationSkill>();
            Controller = GetComponent<MovementController>();
            Armor = GetComponent<CharacterArmor>();
        }


        public void SetName(string charName)
        {
            text_name = GetComponentInChildren<Text>();
            text_name.text = charName;
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