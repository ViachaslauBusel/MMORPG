using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Network.Object.Visualization.Entities.Characters
{
    public class Skeleton : MonoBehaviour
    {
        [SerializeField]
        private Transform _rootBone;

        public Transform RootBone => _rootBone;

        public Transform[] GetBones(IReadOnlyCollection<string> boneNames)
        {
            return boneNames.Select(boneName => GetBone(boneName)).ToArray();
        }

        private Transform GetBone(string boneName)
        {
            if (_rootBone.name == boneName) return _rootBone;

            var bone = FindBoneRecursive(_rootBone, boneName);
            if (bone == null)
            {
                Debug.LogError($"Bone {boneName} not found");
            }

            return bone;
        }

        private Transform FindBoneRecursive(Transform current, string boneName)
        {
            if (current.name == boneName) return current;

            foreach (Transform child in current)
            {
                var found = FindBoneRecursive(child, boneName);
                if (found != null) return found;
            }

            return null;
        }
    }
}
