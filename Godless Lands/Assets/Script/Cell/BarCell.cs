using Items;
using RUCP;
using SkillsRedactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cells
{
    public class BarCell : Cell
    {
        private Image hide;
        private Text help;
        private Token token;
        private Coroutine coroutineHide;
        private Skill skill;//Выбраная умения,предмет, действия для использования в ячейке
        private Item item;
        private int key;//уникальный ид предмета
        private Transform playerTransform;
        private int index;//Номер этой ячейки
        private Text count;

        private new void Awake()
        {
            count = transform.Find("Count").GetComponent<Text>();
            base.Awake();
            hide = transform.Find("Hide").GetComponent<Image>();
            hide.enabled = false;
            help = transform.Find("help").GetComponent<Text>();
           
            playerTransform = GameObject.Find("Player").transform;
        }

        public bool IsUse(Token _token)
        {
            if (token != null && token.Equals(_token)) return true;
            return false;
        }
        public void SetToken(Token token)
        {
            this.token = token;
            help.text = token.name;
        }

        private void Update()
        {
            if (Input.GetKeyDown(token.key))
            {
                Use();
            }
        }

        public override bool IsEmpty()
        {
            return skill == null && item == null;
        }

        public override void Use()
        {
            if(skill != null)
            {
                NetworkWriter nw = new NetworkWriter(Channels.Reliable);
                nw.SetTypePack(Types.Skill);
                nw.write(skill.id);
                nw.write(playerTransform.forward.x);//Направление игрока
                nw.write(playerTransform.forward.z);
                NetworkManager.Send(nw);
            }
            if(item != null)
            {
                NetworkWriter nw = new NetworkWriter(Channels.Reliable);
                nw.SetTypePack(Types.UseItemByKey);
                nw.write(key);
                nw.write(item.id);
                NetworkManager.Send(nw);
            }

        }

        public override void Put(Cell cell)
        {

            if (cell == null || cell.IsEmpty() || cell.GetType() == typeof(TradeCell) || cell.GetType() == typeof(OfferCell)) { icon.enabled = false; return; }

            if(cell.GetType() == typeof(BarCell))//Поменять местами содержимое ячейки
            {
                NetworkWriter writer = new NetworkWriter(Channels.Reliable);
                writer.SetTypePack(Types.WrapBarCell);
                writer.write((byte)index);//Номер ячейки на панели
                writer.write((byte)(cell as BarCell).index);//Номер ячейки на панели
                NetworkManager.Send(writer);
                return;
            }

            NetworkWriter nw = new NetworkWriter(Channels.Reliable);
            nw.SetTypePack(Types.updateBarCell);
            nw.write((byte)index);//Номер ячейки на панели

            //тип ячейки -   0 умения, 1 предмет
            if (cell.GetType() == typeof(SkillCell))
            {
                nw.write(0);//type
                nw.write((cell as SkillCell).GetSkill().id);
  
            }
            else if (cell.GetType() == typeof(ItemCell))
            {
                ItemCell item = cell as ItemCell;
                nw.write(1);//type
                nw.write(item.GetItem().id);
                nw.write(item.GetKey());
            }
            else if (cell.GetType() == typeof(ArmorCell))
            {
                ArmorCell item = cell as ArmorCell;
                nw.write(1);//type
                nw.write(item.GetItem().id);
                nw.write(item.GetKey());
            }
            else return;
            NetworkManager.Send(nw);
            // icon.enabled = true;
            // this.targetCell = cell;
            //  icon.sprite = Sprite.Create(cell.GetIcon(), new Rect(0.0f, 0.0f, cell.GetIcon().width, cell.GetIcon().height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        public void SetSkill(Skill skill)
        {
            item = null;
            this.skill = skill;
             icon.enabled = true;
             icon.sprite = Sprite.Create(skill.icon, new Rect(0.0f, 0.0f, skill.icon.width, skill.icon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        public void SetItem(Item item, int key, bool setCount = true)
        {
            if (item == null) { Clear(); return; }
            skill = null;
            this.item = item;
            this.key = key;
            icon.enabled = true;
            icon.sprite = Sprite.Create(item.texture, new Rect(0.0f, 0.0f, item.texture.width, item.texture.height), new Vector2(0.5f, 0.5f), 100.0f);

            if (item.stack && setCount)
            {
                count.text = Inventory.GetCount(key).ToString();
                count.enabled = true;
            }
            else
                count.text = "";
        }

        //Обновилось количество какого то предмета в инвентаре
        public void Refresh()
        {
         //   print("refresh");
            if (item != null && item.stack)
            {
           //     print("count"+Inventory.GetCount(item.id).ToString());
           
                count.text = Inventory.GetCount(key).ToString();
                count.enabled = true;
            }
        }
        public void Clear()
        {
            item = null;
            skill = null;
            icon.enabled = false;
            count.text = "";
        }

        public Skill GetSkill()
        {
            return skill;
        }
        public Item GetItem()
        {
            return item;
        }
        public void Hide(float time)
        {
           if (coroutineHide != null) StopCoroutine(coroutineHide);
          coroutineHide = StartCoroutine(IEHide(time));
        }
        IEnumerator IEHide(float time)
        {
            float allTime = time;
            hide.fillAmount = 1.0f;
           hide.enabled = true;
            while(time > 0.0f)
            {
                hide.fillAmount = time / allTime;
                yield return 0;
                time -= Time.deltaTime;
            }
            hide.enabled = false;
        }

        /*  public System.Object GetTargetCell()
          {
              return targetCell;
          }*/
        public override void HideIcon()
        {
            base.HideIcon();
            count.enabled = false;
        }
        public override void ShowIcon()
        {
            base.ShowIcon();
            count.enabled = true;
        }
        public override void Abort()
        {
            NetworkWriter nw = new NetworkWriter(Channels.Reliable);
            nw.SetTypePack(Types.updateBarCell);
            nw.write((byte)index);//Номер ячейки на панели
            nw.write(-1);//type
            NetworkManager.Send(nw);
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }
    }
}