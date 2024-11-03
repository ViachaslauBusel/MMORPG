using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Network.Object.Visualization.Entities.Characters
{
    public class BodyPartData : MonoBehaviour
    {
        [SerializeField]
        private string _rootBone;
        [SerializeField]
        private string[] _bones;

        public string RootBone => _rootBone;
        public IReadOnlyCollection<string> Bones => _bones;

        public void Bake()
        {
            var renderer = GetComponent<SkinnedMeshRenderer>();
            if (renderer == null)
            {
                Debug.LogError("BodyPartData: Bake: SkinnedMeshRenderer not found.");
                return;
            }

            _rootBone = renderer.rootBone.name;
            _bones = renderer.bones.Select(bone => bone.name).ToArray();
        }
    }
}
