﻿using SkillsRedactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cells
{
    public class CellRenderInfo
    {
        public Texture2D Icon { get; set; }
        public bool IsDrawCount { get; set; }
        public string Count { get; set; }

        public CellRenderInfo(Texture2D icon, bool isDrawCount, string count)
        {
            Icon = icon;
            IsDrawCount = isDrawCount;
            Count = count;
        }

        internal static CellRenderInfo CreateBySkill(Skill skill)
        {
           return new CellRenderInfo(skill.icon, false, "");
        }

        internal Sprite CreateSprite()
        {
           return Sprite.Create(Icon, new Rect(0, 0, Icon.width, Icon.height), Vector2.zero);
        }

        internal string CreateCountTxt()
        {
           return Count;
        }
    }
}