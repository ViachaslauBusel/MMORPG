#if UNITY_EDITOR
using OpenWorld;
using Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorldEditor
{
    public class ResourcesLoader : MonoBehaviour
    {
        private Transform trackingObj;
        private Vector4 border;
        private float _areaVisible = 300.0f;
        private float blockSize = 70.0f;
        private List<ResourcesDrawGizmos> _resources;

        public List<ResourcesDrawGizmos> Resources { get { return _resources; } }

        public void Start()
        {
            trackingObj = GetComponent<MapLoader>().trackingObj;
            _resources = new List<ResourcesDrawGizmos>();
            CalculateBorder();
            CalculeteVisibleMonster();
        }

        private void CalculateBorder()
        {
            border = new Vector4();
            CalculateBorderX();
            CalculateBorderY();

        }
        private void CalculateBorderX()
        {
            //    Debug.Log("Border X");
            border.w = (trackingObj.position.x - (trackingObj.position.x % blockSize)) - blockSize * 0.1f;//Left
            border.y = (trackingObj.position.x - (trackingObj.position.x % blockSize)) + blockSize + blockSize * 0.1f;//Right
        }
        private void CalculateBorderY()
        {
            //   Debug.Log("Border Y");
            border.x = (trackingObj.position.z - (trackingObj.position.z % blockSize)) - blockSize * 0.1f;//Down
            border.z = (trackingObj.position.z - (trackingObj.position.z % blockSize)) + blockSize + blockSize * 0.1f;//Up
        }

        public void UpdateGUI()
        {
            ChangeBlock();

        }

        public void ChangeBlock()
        {

            if (trackingObj.position.x < border.w)//left
            {
                //     Debug.Log("left");

                //    Debug.Log("left: "+ trackingObj.position);
                CalculateBorderX();
                CalculeteVisibleMonster();
            }
            else if (trackingObj.position.x > border.y)//right
            {
                //   Debug.Log("right");

                //   Debug.Log("right: " + trackingObj.position);
                CalculateBorderX();
                CalculeteVisibleMonster();
            }
            else if (trackingObj.position.z < border.x)//up
            {
                //  Debug.Log("down");

                //     Debug.Log("down: " + trackingObj.position);
                CalculateBorderY();

                CalculeteVisibleMonster();

            }
            else if (trackingObj.position.z > border.z)//down
            {
                //    Debug.Log("up");

                CalculateBorderY();
                CalculeteVisibleMonster();
            }

        }

        public void Destroy()
        {
            foreach (ResourcesDrawGizmos obj in _resources) DestroyImmediate(obj.gameObject);
            _resources.Clear();
        }

        public void CalculeteVisibleMonster()
        {

            Destroy();
            foreach (WorldFabric fabric in WindowSetting.WorldResourcesList.worldResources)
            {
                if (Vector3.Distance(trackingObj.position, fabric.point) < _areaVisible)
                {
                    GameObject prefabResource = WindowSetting.ResourcesList.GetPrefab(fabric.id);
                    if (prefabResource != null)
                    {
                        GameObject instantiateResource = Instantiate(prefabResource);
                        instantiateResource.transform.position = fabric.point;
                        ResourcesDrawGizmos resourcesDraw = instantiateResource.AddComponent<ResourcesDrawGizmos>();
                        resourcesDraw.radius = fabric.radius;
                        resourcesDraw.worldFabric = fabric;
                        _resources.Add(resourcesDraw);
                    }
                }
            }
        }
    }
}
#endif