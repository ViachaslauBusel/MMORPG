#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Redactor;

namespace SkillsRedactor
{
    public class CreateSkillsAsset
    {
        [MenuItem("Assets/Create/SkillsList")]
        public static void Create()
        {
            CreateAsset.Create<SkillsList>("SkillsList");
        }

      
    }
}
#endif