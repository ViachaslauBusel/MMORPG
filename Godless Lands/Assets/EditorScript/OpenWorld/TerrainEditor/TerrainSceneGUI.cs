#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorld
{
   
    //Отрисовка кистей редактора террейна
    public class TerrainSceneGUI 
    {
        private static ProjectorBrush instantBrush;


        public static void SceneGUI(Camera _camera)
        {

            if (WindowTerrain.IfActiveHeightmapBrushes() || WindowTerrain.IfActiveAlphamapBrushes())//Если активированы инструменты используищее кисть
            {
                if (_camera == null) { Debug.Log("_camera == null"); return; }

                RaycastHit hit;
                Vector2 position = Event.current.mousePosition;
                position.y = _camera.pixelHeight - position.y;
                Ray ray = _camera.ScreenPointToRay(position);
                int layerMask = 1 << LayerMask.NameToLayer("Terrain");

                if (Physics.Raycast(ray, out hit, 1000.0f, layerMask))
                {
                    Vector3 hit_pos;
                    float size_cell;
                    Vector2Int number_cell;


                    GetHitPostion(hit, out hit_pos, out size_cell, out number_cell);



                    if (instantBrush == null)  //Создание кисти
                    {
                        GameObject pref_brush = AssetDatabase.LoadAssetAtPath<GameObject>("Brush"); 
                        GameObject _obj = GameObject.Instantiate(pref_brush);
                        instantBrush = _obj.GetComponent<ProjectorBrush>();
                    }
                    instantBrush.DrawBrush(hit_pos, size_cell);
                    instantBrush.gameObject.SetActive(true);


                    if ((Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseDrag) && Event.current.button == 0)
                    {
                        if (WindowTerrain.IfActiveHeightmapBrushes()) TerrainEditor.Raise(hit.transform.gameObject.GetComponentInParent<TerrainInfo>(), number_cell, instantBrush.brush.brush_float);
                    }

                }
                else
                {
                    if (instantBrush != null) instantBrush.gameObject.SetActive(false);
                }
            }
        }

        private static void GetHitPostion(RaycastHit hit, out Vector3 hit_pos, out float size_cell, out Vector2Int number_cell)
        {
            hit_pos = hit.transform.position;

            TerrainData terrainData = hit.transform.GetComponent<Terrain>().terrainData;

            size_cell = 0.0f;
            if (WindowTerrain.IfActiveHeightmapBrushes()) size_cell = terrainData.size.x / (terrainData.heightmapResolution - 1);
            else if (WindowTerrain.IfActiveAlphamapBrushes()) size_cell = terrainData.size.x / (terrainData.alphamapResolution);
            number_cell = Vector2Int.zero;
            number_cell.x = (int)((hit.point.x - hit.transform.position.x) / size_cell);
            number_cell.y = (int)((hit.point.z - hit.transform.position.z) / size_cell);
            if (WindowTerrain.IfActiveHeightmapBrushes())
            {
                if (((hit.point.x - hit.transform.position.x) % size_cell) > size_cell / 2.0f) number_cell.x++;
                if (((hit.point.z - hit.transform.position.z) % size_cell) > size_cell / 2.0f) number_cell.y++;
            }

            hit_pos += new Vector3(number_cell.x * size_cell, 525.0f, number_cell.y * size_cell);
            if (WindowTerrain.IfActiveAlphamapBrushes())
            {
                hit_pos += new Vector3(0.5f * size_cell, 0.0f, 0.5f * size_cell);
            }

        }

        public static void Destroy()
        {
            if (instantBrush != null) GameObject.DestroyImmediate(instantBrush.gameObject);
        }
    }
}
#endif