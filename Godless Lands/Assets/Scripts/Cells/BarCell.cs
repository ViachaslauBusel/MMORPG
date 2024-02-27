using Protocol.MSG.Game.Hotbar;
using RUCP;
using Skills;
using Hotbar;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UnityEngine.InputSystem;

namespace Cells
{
    public class BarCell : Cell
    {
        private CellContentToRenderConverter _conventer;
        private NetworkManager _networkManager;
        private Image _hide;
        private Text _help;
        private InputAction _input;
        private Coroutine _coroutineHide;
        private CellContentInfo _contentInfo;
        private CellRenderInfo _celllRenderInfo;
        private int _index;//Номер этой ячейки
        private Text _countTxt;
        private SkillUsageRequestSender _skillController;
      

        //   private int count;

        [Inject]
        private void Construct(NetworkManager networkManager, CellContentToRenderConverter converter, SkillUsageRequestSender skillUsageRequestSender)
        {
            _networkManager = networkManager;
            _conventer = converter;
            _skillController = skillUsageRequestSender;
        }

        private new void Awake()
        {
            _countTxt = transform.Find("Count").GetComponent<Text>();
            base.Awake();
            _hide = transform.Find("Hide").GetComponent<Image>();
            _hide.enabled = false;
            _help = transform.Find("help").GetComponent<Text>();
           
        
        }

        internal ItemCell GetItemCell()
        {
            return null;
        }

        internal Cell GetCell()
        {
            return null;
        }

        public bool IsUse(Token _token)
        {
            if (this._input != null && this._input.Equals(_token)) return true;
            return false;
        }
        public void SetToken(InputAction input)
        {
            _input = input;
            _help.text = input.name;
            _input.performed += (context) => Use();
        }

        public override bool IsEmpty()
        {
            return _contentInfo == null;
        }

        public override void Use()
        {
            if(_contentInfo != null)
            {
                switch(_contentInfo.Type)
                {
                    case CellContentType.Skill:
                        _skillController.SendSkillUsageRequest(_contentInfo.ID);
                        break;
                    case CellContentType.Item:
                       // UseItem();
                        break;
                }
            }
        }

        public override void Put(Cell cell)
        {
            

            if (cell == null || cell.IsEmpty()) {  return; }


            if(cell.GetType() == typeof(BarCell))//Поменять местами содержимое ячейки
            {
            //TODO msg
                //Packet writer = new Packet(Channel.Reliable);
                //writer.WriteType(Types.WrapBarCell);
                //writer.WriteByte((byte)index);//Номер ячейки на панели
                //writer.WriteByte((byte)(cell as BarCell).index);//Номер ячейки на панели
                //NetworkManager.Send(writer);
                 return;
            }

            MSG_HOTBAR_SET_CELL_VALUE_CS msg = new MSG_HOTBAR_SET_CELL_VALUE_CS();
            msg.CellIndex = (byte)_index;
            msg.CellType = cell switch
            {
                SkillCell => HotbarCellType.Skill,
                ItemCell => HotbarCellType.Item,
                _ => HotbarCellType.Unknown,
            };
            msg.CellValue = (short)cell.GetObjectID();
            _networkManager.Client.Send(msg);

            //TODO msg
            //Packet nw = new Packet(Channel.Reliable);
            //nw.WriteType(Types.updateBarCell);
            //nw.WriteByte((byte)index);//Номер ячейки на панели


            //if (cell.GetType() == typeof(SkillCell))
            //{
            //    nw.WriteInt((int)SkillbarType.Skill);//type
            //    nw.WriteInt((cell as SkillCell).GetSkill().id);
            //}
            //else if (cell.GetType() == typeof(ItemCell))
            //{
            //    ItemCell item = cell as ItemCell;
            //    nw.WriteInt((int)SkillbarType.Item);//type
            //    nw.WriteInt(item.GetObjectID());
            //}
            //else if (cell.GetType() == typeof(ArmorCell))
            //{
            //    ArmorCell item = cell as ArmorCell;
            //    nw.WriteInt((int)SkillbarType.Item);//type
            //    nw.WriteInt(item.GetObjectID());
            //}
            //else return;
            //NetworkManager.Send(nw);
        }


        public void SetContent(CellContentInfo contentInfo)
        {
            _contentInfo = contentInfo;
        }


        public void Redraw()
        {
            CellRenderInfo cellRenderInfo =  _conventer.Convert(_contentInfo);

            if(_celllRenderInfo == cellRenderInfo)
            {
                return;
            }
            _celllRenderInfo = cellRenderInfo;

            if (cellRenderInfo == null)
            {
                HideIcon();
            }
            else
            {
                ShowIcon();
                icon.sprite = cellRenderInfo.CreateSprite();
                _countTxt.text = cellRenderInfo.CreateCountTxt();
            }
        }
 

        public void Hide(float time)
        {
           if (_coroutineHide != null) StopCoroutine(_coroutineHide);
          _coroutineHide = StartCoroutine(IEHide(time));
        }
        IEnumerator IEHide(float time)
        {
            float allTime = time;
            _hide.fillAmount = 1.0f;
           _hide.enabled = true;
            while(time > 0.0f)
            {
                _hide.fillAmount = time / allTime;
                yield return 0;
                time -= Time.deltaTime;
            }
            _hide.enabled = false;
        }

        public override void HideIcon()
        {
            base.HideIcon();
            _countTxt.enabled = false;
        }

        public override void ShowIcon()
        {
            base.ShowIcon();
            _countTxt.enabled = true;
        }

        public override void Abort()
        {
        //TODO msg
            //Packet nw = new Packet(Channel.Reliable);
            //nw.WriteType(Types.updateBarCell);
            //nw.WriteByte((byte)index);//Номер ячейки на панели
            //nw.WriteInt((int)SkillbarType.None);//type
            //NetworkManager.Send(nw);
        }

        public void SetIndex(int index)
        {
            this._index = index;
        }
    }
}