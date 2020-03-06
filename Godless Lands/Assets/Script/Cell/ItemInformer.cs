using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cells
{
    public class ItemInformer : Informer
    {

        public static readonly string[] colorQuality =
        {
            "black",
            "magenta",
            "green",
            "blue",
            "red"
        };
        public static string GetColor(int index)
        {
            if (index < 0 || index >= colorQuality.Length) return "white";
            return colorQuality[index];
        }
       public static readonly string[] nameQuality =
       {
            "Бесполезный предмет",
            "Бракованный предмет",
            "Обычный предмет",
            "Качественный предмет",
            "Мастерский предмет"
        };
        public static string GetName(int index)
        {
            if (index < 0 || index >= nameQuality.Length) return "ошибка: "+index;
            return nameQuality[index];
        }
        public static readonly float[] percentQuality =
        {
            0.9f,
            0.95f,
            1.0f,
            1.05f,
            1.1f
        };
        public static readonly float percentEnchant = 0.02f;

        public Text nameTxt;
        public Text level;
        public Text durability;

        private void Awake()
        {
            durability.enabled = false;
        }


        public void SetName(int enchant_level, string _name, int quality, int durab)
        {
            nameTxt.text = "<size=26><color=" + GetColor(quality) + ">" + ((enchant_level != 0)? '+'+enchant_level.ToString()+' ':"") + _name + "</color></size>" + '\n' ;
            nameTxt.text += "<size=22><color=" + GetColor(quality) + ">" + GetName(quality) + "</color></size>" + '\n';
            if(durab < 1) nameTxt.text += "<color=red>Сломанный предмет</color>" + '\n';
            nameTxt.text += '\n';

            //  description.text += "Качество: " + nameQuality[quality] + '\n';
        }
        public void SetName(string _name)
        {
            nameTxt.text =  _name +  '\n' + '\n';
        }
        public void SetLevel(SkillBranch skill)
        {
            level.text = "Одноручный" + '\n';
            switch (skill)
            {
                case SkillBranch.Sword:
                    level.text += "   Мечи(Уровень 1)";
                    break;
                case SkillBranch.Blunt:
                    level.text += "   Дубины(Уровень 1)";
                    break;
                case SkillBranch.Bow:
                    level.text += "   Луки(Уровень 1)";
                    break;
            }
        }
        public void SetCount(int count)
        {
            description.text += "Количество: " + count + '\n';
        }
        public void SetAtack(int min, int max, int quality, int enchant_level)
        {
            float cul_min = (min * percentQuality[quality]) + (min * (enchant_level * percentEnchant));
            float cul_max = (max * percentQuality[quality]) + (max * (enchant_level * percentEnchant));

            description.text += "Урон: " + Math.Truncate(cul_min) + " - "            
                             + Math.Truncate(cul_max) + '\n' + '\n';
        }
        public void SetSpeedAtack(float speed)
        {
            description.text += "Скор. Атаки: " + speed + '\n';
        }
        public void SetDurability(int durab, int maxDurability)
        {
            durability.enabled = true;
            durability.text = "Прочность ["+ durab + " / "+maxDurability+"]";
        }
     /*   public void SetPrickingDamage(int atack, int quality)
        {
            description.text += "Колющий урон: " + (int)(atack * percentQuality[quality]) + '\n';
        }
        public void SetCrushingDamage(int atack, int quality)
        {
            description.text += "Дробящий урон: " + (int)(atack * percentQuality[quality]) + '\n';
        }
        public void SetChoppingDamage(int atack, int quality)
        {
            description.text += "Рубящий урон: " + (int)(atack * percentQuality[quality]) + '\n';
        }*/


        public void SetHP(int HP)
        {
            description.text += "Восстанавливает: " + HP + "ХП" + '\n';
        }
        public void SetMP(int MP)
        {
            description.text += "Восстанавливает: " + MP + "МП" + '\n';
        }
        public void SetStamina(int stamina)
        {
            description.text += "Восстанавливает: " + stamina + "СТ" + '\n';
        }

        internal void setWeight(int weight)
        {
            description.text += "Вес: " + (weight / 1000.0f).ToString("0.0##") +"кг";
        }

        public void setDescription(string _description)
        {
            description.text += _description + '\n' + '\n';
        }

        


    }
}