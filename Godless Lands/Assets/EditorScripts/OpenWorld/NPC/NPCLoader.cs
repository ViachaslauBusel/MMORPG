#if UNITY_EDITOR
using NPCRedactor;
using OpenWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorldEditor
{
    /// <summary>
    /// Вешаеться на карту вместе с MaoLoader и подгружает НПЦ вокруг цели
    /// </summary>
    public class NPCLoader : MonoBehaviour
    {
        private Transform trackingObj;//Цель вокруг которой подгружаются НПЦ
        private Vector4 border;
        private float _areaVisible = 300.0f;
        private float blockSize = 70.0f;
        private List<NPCDraw> npcVisible;//Загруженные нпц

        public List<NPCDraw> npcs { get { return npcVisible; } }

        public void Start()
        {
            trackingObj = GetComponent<MapLoader>().m_trackingObj;
            npcVisible = new List<NPCDraw>();
            CalculateBorder();
            CalculeteVisibleNPC();
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
                CalculeteVisibleNPC();
            }
            else if (trackingObj.position.x > border.y)//right
            {
                //   Debug.Log("right");

                //   Debug.Log("right: " + trackingObj.position);
                CalculateBorderX();
                CalculeteVisibleNPC();
            }
            else if (trackingObj.position.z < border.x)//up
            {
                //  Debug.Log("down");

                //     Debug.Log("down: " + trackingObj.position);
                CalculateBorderY();

                CalculeteVisibleNPC();

            }
            else if (trackingObj.position.z > border.z)//down
            {
                //    Debug.Log("up");

                CalculateBorderY();
                CalculeteVisibleNPC();
            }

        }

        public void Destroy()
        {
            foreach (NPCDraw obj in npcVisible) DestroyImmediate(obj.gameObject);
            npcVisible.Clear();
        }

        public void CalculeteVisibleNPC()
        {

            Destroy();
            foreach (WorldNPC npc in WindowSetting.WorldNPCList.worldNPC)
            {
                if (Vector3.Distance(trackingObj.position, npc.point) < _areaVisible)
                {
                    //GameObject prefabNPC = WindowSetting.NPCList.GetPrefab(npc.id);
                    //if (prefabNPC != null)
                    //{
                    //    GameObject instantiateNPC = Instantiate(prefabNPC);
                    //    instantiateNPC.transform.position = npc.point;
                    //    instantiateNPC.transform.rotation = Quaternion.Euler(0.0f, npc.radius, 0.0f);
                    //   NPCDraw npcDraw = instantiateNPC.AddComponent<NPCDraw>();

                    //    npcDraw.worldNPC = npc;
                    //    npcVisible.Add(npcDraw);
                    //}
                }
            }
        }
    }
}
#endif