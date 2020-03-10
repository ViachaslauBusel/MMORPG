using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillsRedactor;

namespace Cells
{
    public class SkillCell : Cell
    {
        private Skill skill;
        private Transform playerTransform;

        private new void Awake()
        {
            base.Awake();
            playerTransform = GameObject.Find("Player").transform;
        }

        public override bool IsEmpty()
        {
            return skill == null;
        }

        public override void Use()
        {
            NetworkWriter nw = new NetworkWriter(Channels.Reliable);
            nw.SetTypePack(Types.Skill);
            nw.write(skill.id);
          //  float angle = Vector2.Angle(new Vector2(playerTransform.forward.x, playerTransform.forward.z), Vector2.up);
       //     print("angle: " + angle + "pi: "+ (Mathf.Deg2Rad * 180.0f));
            nw.write(playerTransform.forward.x);//Направление игрока
            nw.write(playerTransform.forward.z);
            NetworkManager.Send(nw);
        }

        public void PutSkill(Skill skill)
        {
            this.skill = skill;
            if (IsEmpty())
            {
                icon.enabled = false;
                return;
            }
            icon.enabled = true;
            icon.sprite = Sprite.Create(skill.icon, new Rect(0.0f, 0.0f, skill.icon.width, skill.icon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        public Skill GetSkill()
        {
            return skill;
        }

        public override int GetObjectID()
        {
            if(IsEmpty()) return base.GetObjectID();
            return skill.id;

        }
    }
}