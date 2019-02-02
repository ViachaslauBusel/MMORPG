using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cells
{
    public class ItemInformer : Informer
    {

       

       

       
        public void SetName(string _name)
        {
            description.text += "Название: " + _name + '\n';
        }
        public void SetCount(int count)
        {
            description.text += "Количество: " + count + '\n';
        }
        public void SetAtack(int atack)
        {
            description.text += "Физ. Атака: " + atack + '\n';
        }
        public void SetSpeedAtack(float speed)
        {
            description.text += "Скор. Атаки: " + speed + '\n';
        }
        public void SetPrickingDamage(int atack)
        {
            description.text += "Колющий урон: " + atack + '\n';
        }
        public void SetCrushingDamage(int atack)
        {
            description.text += "Дробящий урон: " + atack + '\n';
        }
        public void SetChoppingDamage(int atack)
        {
            description.text += "Рубящий урон: " + atack + '\n';
        }


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

        public void setDescription(string _description)
        {
            description.text += "Описание: " + _description;
        }

        


    }
}