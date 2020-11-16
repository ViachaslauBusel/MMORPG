using Items;
using RUCP;
using RUCP.Network;
using RUCP.Packets;
using Skills;
using SkillsBar;
using SkillsRedactor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Cells
{
    public class BarCell : Cell
    {
        private Image hide;
        private Text help;
        private Token token;
        private Coroutine coroutineHide;
        private Cell cell;
        private int cellID;
        private int index;//Номер этой ячейки
        private Text countTxt;
     //   private int count;

        private new void Awake()
        {
            countTxt = transform.Find("Count").GetComponent<Text>();
            base.Awake();
            hide = transform.Find("Hide").GetComponent<Image>();
            hide.enabled = false;
            help = transform.Find("help").GetComponent<Text>();
           
        
        }

        internal ItemCell GetItemCell()
        {
            return cell as ItemCell;
        }

        internal Cell GetCell()
        {
            return cell;
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
            return cell == null || cell.IsEmpty();
        }

        public override void Use()
        {
            if (cell != null)
                cell.Use();

        }

        public override void Put(Cell cell)
        {
            

            if (cell == null || cell.IsEmpty()) {  return; }


            if(cell.GetType() == typeof(BarCell))//Поменять местами содержимое ячейки
            {
                Packet writer = new Packet(Channel.Reliable);
                writer.WriteType(Types.WrapBarCell);
                writer.WriteByte((byte)index);//Номер ячейки на панели
                writer.WriteByte((byte)(cell as BarCell).index);//Номер ячейки на панели
                NetworkManager.Send(writer);
                 return;
            }

     
            Packet nw = new Packet(Channel.Reliable);
            nw.WriteType(Types.updateBarCell);
            nw.WriteByte((byte)index);//Номер ячейки на панели

      
            if (cell.GetType() == typeof(SkillCell))
            {
                nw.WriteInt((int)SkillbarType.Skill);//type
                nw.WriteInt((cell as SkillCell).GetSkill().id);
            }
            else if (cell.GetType() == typeof(ItemCell))
            {
                ItemCell item = cell as ItemCell;
                nw.WriteInt((int)SkillbarType.Item);//type
                nw.WriteInt(item.GetObjectID());
            }
            else if (cell.GetType() == typeof(ArmorCell))
            {
                ArmorCell item = cell as ArmorCell;
                nw.WriteInt((int)SkillbarType.Item);//type
                nw.WriteInt(item.GetObjectID());
            }
            else return;
            NetworkManager.Send(nw);
        }



        public void InsertCell(Cell cell)
        {
            this.cell = cell;
            if (cell != null)
            { cellID = cell.GetObjectID(); }
            else cellID = 0;

            UpdateCell();

        }

        private void UpdateCell()
        {
           // Debug.Log("update: " + cell?.GetType() + ": " + cell?.GetObjectID());
            if (cell == null)
            { HideIcon(); }
            else
            {
                ShowIcon();
                icon.sprite = cell.GetSprite();
                countTxt.text = cell.GetText();
            }
        }


        //Обновилось количество какого то предмета в инвентаре
        public void Refresh()
        {
            //   print("refresh");
            if (cell is ItemCell)
            {
                cell = Inventory.GetItemCellByObjectID(cellID);
                UpdateCell();
            } else if(cell is SkillCell)
            {
                cell = SkillsBook.GetSkillCellByID(cellID);
                UpdateCell();
            }
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
            countTxt.enabled = false;
        }
        public override void ShowIcon()
        {
            base.ShowIcon();
            countTxt.enabled = true;
        }
        public override void Abort()
        {
            Packet nw = new Packet(Channel.Reliable);
            nw.WriteType(Types.updateBarCell);
            nw.WriteByte((byte)index);//Номер ячейки на панели
            nw.WriteInt((int)SkillbarType.None);//type
            NetworkManager.Send(nw);
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }
    }
}