#if UNITY_EDITOR
using MonsterRedactor;
using OpenWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OpenWorldEditor
{
    public class MonstersLoader : MonoBehaviour
    {
        private Transform trackingObj;
        private Vector4 border;
        private float _areaVisible = 300.0f;
        private float blockSize = 70.0f;
        private List<MonsterDrawGizmos> _monsters;

        public List<MonsterDrawGizmos> monsters { get { return _monsters; } }

        public void Start()
        {
            trackingObj = GetComponent<MapLoader>().m_trackingObj;
            _monsters = new List<MonsterDrawGizmos>();
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
            foreach (MonsterDrawGizmos obj in _monsters) DestroyImmediate(obj.gameObject);
            _monsters.Clear();
        }

        public void CalculeteVisibleMonster()
        {
      
            Destroy();
            foreach(WorldMonster monster in WindowSetting.WorldMonsterList.worldMonsters)
            {
                if (Vector3.Distance(trackingObj.position, monster.point) < _areaVisible)
                {
                    GameObject prefabMonster = WindowSetting.monstersList.GetPrefab(monster.id);
                    if(prefabMonster != null)
                    {
                        GameObject instantiateMonster = Instantiate(prefabMonster);
                        instantiateMonster.transform.position = monster.point;
                        MonsterDrawGizmos monsterDraw = instantiateMonster.AddComponent<MonsterDrawGizmos>();
                        monsterDraw.radius = monster.radius;
                        monsterDraw.worldMonster = monster;
                        _monsters.Add(monsterDraw);
                    }
                }
            }
        }
    }
}
#endif