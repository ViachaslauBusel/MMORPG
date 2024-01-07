using Protocol.MSG.Game.Hotbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Cells
{
    public class CellContentInfo
    {
        /// <summary>
        /// Unique ID of item or skill
        /// </summary>
        public int ID { get; private set; }
        public CellContentType Type { get; private set; }

        public CellContentInfo(HotbarCellType cellType, short cellValue)
        {
           Type = cellType switch
           {
                HotbarCellType.Unknown => CellContentType.None,
                HotbarCellType.Item => CellContentType.Item,
                HotbarCellType.Skill => CellContentType.Skill,
                _ => CellContentType.None
            };
            ID = cellValue;
        }
    }
}
