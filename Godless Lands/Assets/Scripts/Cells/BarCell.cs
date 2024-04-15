using Protocol.MSG.Game.Hotbar;
using Skills;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

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

        private void Awake()
        {
            _countTxt = transform.Find("Count").GetComponent<Text>();
            Init();
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

        public void SetToken(InputAction input, string name)
        {
            _input = input;
            _help.text = name;
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

            if (cell is BarCell barCell)//Поменять местами содержимое ячейки
            {
                MSG_HOTBAT_SWAMP_CELLS_CS swamp_request = new MSG_HOTBAT_SWAMP_CELLS_CS();
                swamp_request.FromCellIndex = (byte)_index;
                swamp_request.ToCellIndex = (byte)barCell._index;
                _networkManager.Client.Send(swamp_request);
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
            msg.CellValue = (short)cell.GetItemUID();
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
                Hide();
            }
            else
            {
                Show();
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

        public override void Hide()
        {
            base.Hide();
            _countTxt.enabled = false;
        }

        public override void Show()
        {
            base.Show();
            _countTxt.enabled = true;
        }

        public override void Abort()
        {
            MSG_HOTBAR_SET_CELL_VALUE_CS msg = new MSG_HOTBAR_SET_CELL_VALUE_CS();
            msg.CellIndex = (byte)_index;
            msg.CellType = HotbarCellType.Unknown;
            msg.CellValue = 0;
            _networkManager.Client.Send(msg);
        }

        public void SetIndex(int index)
        {
            _index = index;
        }
    }
}