using RUCP;
using Skills.Data;
using SkillsRedactor;
using UnityEngine;

namespace Cells
{
    public class SkillCell : Cell
    {
        private SkillData skill;

        private void Awake()
        {
           Init();
        }

        public override bool IsEmpty()
        {
            return skill == null;
        }

        public override void Use()
        {
        //TODO msg
       //     Packet nw = new Packet(Channel.Reliable);
       //     nw.WriteType(Types.Skill);
       //     nw.WriteInt(skill.id);
       //   //  float angle = Vector2.Angle(new Vector2(playerTransform.forward.x, playerTransform.forward.z), Vector2.up);
       ////     print("angle: " + angle + "pi: "+ (Mathf.Deg2Rad * 180.0f));
       //     nw.WriteFloat(playerTransform.forward.x);//Направление игрока
       //     nw.WriteFloat(playerTransform.forward.z);
       //     NetworkManager.Send(nw);
        }

        public void PutSkill(SkillData skill)
        {
            this.skill = skill;
            if (IsEmpty())
            {
                _icon.enabled = false;
                return;
            }
            _icon.enabled = true;
            _icon.sprite = Sprite.Create(skill.Icon, new Rect(0.0f, 0.0f, skill.Icon.width, skill.Icon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        public SkillData GetSkill()
        {
            return skill;
        }

        public override long GetItemUID()
        {
            if(IsEmpty()) return base.GetItemUID();
            return skill.ID;

        }
    }
}