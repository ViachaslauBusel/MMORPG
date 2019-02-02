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
        private Cell targetCell;//Выбраная умения,предмет, действия для использования в ячейке

        private new void Awake()
        {
            base.Awake();
            hide = transform.Find("Hide").GetComponent<Image>();
            hide.enabled = false;
            help = transform.Find("help").GetComponent<Text>();
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
            return targetCell == null;
        }

        public override void Use()
        {

            if (IsEmpty()) return;
            targetCell.Use();

        }

        public override void Put(Cell cell)
        {

            if (cell == null || cell.IsEmpty() || cell.GetType() == typeof(TradeCell) || cell.GetType() == typeof(OfferCell)) { icon.enabled = false; return; }
            if(cell.GetType() == typeof(BarCell))//Поменять местами содержимое ячейки
            {
                Cell _instCell = this.targetCell;
                BarCell barCell = cell as BarCell;
                Put(barCell.GetTargetCell());
                barCell.Put(_instCell);
                return;
            }
   
            icon.enabled = true;
            this.targetCell = cell;
            icon.sprite = Sprite.Create(cell.GetIcon(), new Rect(0.0f, 0.0f, cell.GetIcon().width, cell.GetIcon().height), new Vector2(0.5f, 0.5f), 100.0f);
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

        public Cell GetTargetCell()
        {
            return targetCell;
        }
    }
}