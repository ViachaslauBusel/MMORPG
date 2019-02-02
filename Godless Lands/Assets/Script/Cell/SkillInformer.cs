using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells
{
    public class SkillInformer : Informer
    {
        public void SetName(string _name)
        {
            description.text += "Название: " + _name + '\n';
        }
        public void setDescription(string _description)
        {
            description.text += "Описание: " + _description + '\n';
        }
        public void SetRange(float range)
        {
            description.text += "Радиус: " + range + "м"+ '\n';
        }
        public void SetAngle(float angle)
        {
            description.text += "Угол: " + angle + "г" + '\n';
        }

        public void SetApplyTime(float time)
        {
            description.text += "Время применение: " + time + "с"+'\n';
        }
        public void SetReuseTime(float time)
        {
            description.text += "Время повтора: " + time + "с" + '\n';
        }
        public void SetStamina(int vitality)
        {
            description.text += "Затраты: " + vitality + '\n';
        }
    }
}