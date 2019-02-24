#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorld
{
    public class ObjectLoader : MonoBehaviour
    {
        protected Transform trackingObj;
        private Vector4 border;
        protected float _areaVisible = 300.0f;
        private float blockSize = 70.0f;
        private List<ObjectDrawGizmos> _resources;

        public List<ObjectDrawGizmos> Resources { get { return _resources; } }

        public void Start()
        {
            trackingObj = GetComponent<MapLoader>().trackingObj;
            _resources = new List<ObjectDrawGizmos>();
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
            foreach (ObjectDrawGizmos obj in _resources) DestroyImmediate(obj.gameObject);
            _resources.Clear();
        }

        public virtual void CalculeteVisibleMonster()
        {

            Destroy();
            // CreatePrefab(prefabResource, machine.position, machine.rotation, machine.scale, machine.radius);

        }

        public void CreatePrefab(GameObject prefabResource, Vector3 position, Vector3 rotation, Vector3 scale, float radius, int id)
        {
            if (prefabResource == null) return;
         
                GameObject instantiateResource = Instantiate(prefabResource, position, Quaternion.Euler(rotation));
                instantiateResource.transform.localScale = scale;
                ObjectDrawGizmos resourcesDraw = instantiateResource.AddComponent<ObjectDrawGizmos>();
                resourcesDraw.radius = radius;
                resourcesDraw.id = id;
                _resources.Add(resourcesDraw);
        }
    }
}
#endif