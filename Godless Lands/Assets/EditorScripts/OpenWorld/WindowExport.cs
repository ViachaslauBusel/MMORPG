#if UNITY_EDITOR
using Assets.EditorScripts;
using MonsterRedactor;
using NPCRedactor;
using OpenWorld;
using Protocol.Data.SpawnPoints;
using Resource;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace OpenWorldEditor {
    public class WindowExport
    {
        private static Map map;
        public static void Draw(Map _map)
        {
            if (_map == null) return;
            map = _map;

            if (GUILayout.Button("Export Spawn Point"))
            {
                float maxProgress = map.mapSize * map.mapSize;
                float totalProgress = 0.0f;

                string folder;
                List<Vector3> spawnPoint = new List<Vector3>();
                for (int x = 0; x < map.mapSize; x++)
                {
                    EditorUtility.DisplayProgressBar("OpenWorld", "Export Spawn Point", totalProgress / maxProgress);
                    for (int y = 0; y < map.mapSize; y++)
                    {

                        totalProgress = (x * map.mapSize) + y;

                        folder = map.mapName + "/KMObject_" + x + '_' + y;
                        spawnPoint.AddRange(GetPoint(folder));
                    }
                }
                EditorUtility.ClearProgressBar();

                Export(spawnPoint);

                Resources.UnloadUnusedAssets();
            }



            if (GUILayout.Button("Export monsters.dat"))
            {
                if (WindowSetting.WorldMonsterList == null || WindowSetting.monstersList == null)
                {
                    EditorUtility.DisplayDialog("Export monsters.dat", "Ошибка экспорта", "ok");
                    return;
                }
                MonsterExport.Export(WindowSetting.WorldMonsterList, WindowSetting.monstersList);
                EditorUtility.DisplayDialog("Export monsters.dat", "Экспорт выполнен", "ok");
            }
            if (GUILayout.Button("Export npc.dat"))
            {
                if (WindowSetting.WorldNPCList == null || WindowSetting.NPCList == null)
                {
                    EditorUtility.DisplayDialog("Export npc.dat", "Ошибка экспорта", "ok");
                    return;
                }
                NPCExport.Export(WindowSetting.WorldNPCList, WindowSetting.NPCList);
                EditorUtility.DisplayDialog("Export npc.dat", "Экспорт выполнен", "ok");
            }
            if (GUILayout.Button("Export miningStones.dat"))
            {
                if (WindowSetting.WorldResourcesList == null || WindowSetting.ResourcesList == null)
                {
                    EditorUtility.DisplayDialog("Export miningStones.dat", "Ошибка экспорта", "ok");
                    return;
                }
                ResourcesExport.Export(WindowSetting.WorldResourcesList, WindowSetting.ResourcesList);
                EditorUtility.DisplayDialog("Export monsters.dat", "Экспорт выполнен", "ok");
            }
            if (GUILayout.Button("Export terrain.dat"))
            {
                if (WindowSetting.WorldMonsterList == null || WindowSetting.monstersList == null)
                {
                    EditorUtility.DisplayDialog("Export terrain.dat", "Ошибка экспорта", "ok");
                    return;
                }
                TerrainExport.Export(WindowSetting.Map);
                EditorUtility.DisplayDialog("Export terrain.dat", "Экспорт выполнен", "ok");
            }

            if (GUILayout.Button("Export machines.dat"))
            {
                if (WindowSetting.MachineList == null)
                {
                    EditorUtility.DisplayDialog("Export machines.dat", "Ошибка экспорта", "ok");
                    return;
                }
                MachinesExport.Export(WindowSetting.MachineList);
                EditorUtility.DisplayDialog("Export machines.dat", "Экспорт выполнен", "ok");
            }

            /*  if (GUILayout.Button("fix TerrainData"))
              {
                  float maxProgress = map.mapSize * map.mapSize;
                  float totalProgress = 0.0f;

                  string folder;
                  for (int x = 0; x < map.mapSize; x++)
                  {
                      EditorUtility.DisplayProgressBar("OpenWorld", "fix terrain", totalProgress / maxProgress);
                      for (int y = 0; y < map.mapSize; y++)
                      {

                          totalProgress = (x * map.mapSize) + y;

                          folder = map.mapName + "/KMBlock_" + x + '_' + y;
                          Fix(folder);
                      }
                  }
                  EditorUtility.ClearProgressBar();
              }*/

            if (GUILayout.Button("Remove missing Tree/Grass"))
            {
                float maxProgress = map.mapSize * map.mapSize;
                float totalProgress = 0.0f;

                string folder;
                for (int x = 0; x < map.mapSize; x++)
                {
                    EditorUtility.DisplayProgressBar("OpenWorld", "fix terrain", totalProgress / maxProgress);
                    for (int y = 0; y < map.mapSize; y++)
                    {

                        totalProgress = (x * map.mapSize) + y;

                        folder = map.mapName + "/KMBlock_" + x + '_' + y;
                    //    Remove(folder);
                        Fix(folder);
                    }
                }
                EditorUtility.ClearProgressBar();
            }
        }

        private static void Fix(string folder)
        {
            for (int x = 0; x < map.blocksCount; x++)
            {
                for (int y = 0; y < map.blocksCount; y++)
                {
                    string path = folder + "/TRBlock_" + x + '_' + y;
                    MapElement mapElement = Resources.Load<MapElement>(path);

                    if (mapElement != null)
                    {
                        mapElement.terrainData.wavingGrassSpeed = 0.2f;
                        mapElement.terrainData.wavingGrassAmount = 0.31f;
                        mapElement.terrainData.wavingGrassStrength = 0.3f;
                        //  mapElement.terrainData.baseMapResolution = 64;
                        //  mapElement.terrainData.SetDetailResolution(128, 32);
                        AssetDatabase.Refresh();
                        EditorUtility.SetDirty(mapElement);
                        AssetDatabase.SaveAssets();
                        Resources.UnloadAsset(mapElement);
                    }


                }
            }
        }

        private static void Remove(string folder)
        {
            for (int x = 0; x < map.blocksCount; x++)
            {
                for (int y = 0; y < map.blocksCount; y++)
                {
                    string path = folder + "/TRBlock_" + x + '_' + y;
                    MapElement mapElement = Resources.Load<MapElement>(path);

                    if (mapElement != null)
                    {
                        TreePrototype[] tree =  mapElement.terrainData.treePrototypes;
                        List<TreePrototype> treesSave = new List<TreePrototype>();
                        foreach(TreePrototype treePrototype in tree)
                        {
                            if (treePrototype.prefab != null)
                            {
                                treesSave.Add(treePrototype);
                            }
                        }
                        TreePrototype[] prototype = treesSave.ToArray();
                        mapElement.terrainData.treePrototypes = prototype;

                        TreeInstance[] treeInstance = mapElement.terrainData.treeInstances;
                        List<TreeInstance> listTrees = new List<TreeInstance>();
                        foreach(TreeInstance instance in treeInstance)
                        {
                            if(instance.prototypeIndex > 0 || instance.prototypeIndex <  prototype.Length)
                            {
                                listTrees.Add(instance);
                            }
                        }
                        mapElement.terrainData.treeInstances = listTrees.ToArray();


                       DetailPrototype[] details = mapElement.terrainData.detailPrototypes;
                        List<DetailPrototype> detailsSave = new List<DetailPrototype>();
                        foreach (DetailPrototype detailPrototype in details)
                        {
                            if (detailPrototype.prototype != null)
                            {
                                detailsSave.Add(detailPrototype);
                            }
                        }
                        DetailPrototype[] detailPrototypes = detailsSave.ToArray();
                        mapElement.terrainData.detailPrototypes = detailPrototypes;

                     //   mapElement.terrainData.GetDetailLayer


                       mapElement.terrainData.RefreshPrototypes();
                        AssetDatabase.Refresh();
                        EditorUtility.SetDirty(mapElement);
                        AssetDatabase.SaveAssets();
                        Resources.UnloadAsset(mapElement);
                    }


                }
            }
        }

        private static void Export(List<Vector3> spawnPoint)
        {
            List<RespawnPointData>  spawnPoints = new List<RespawnPointData>();
            foreach (Vector3 point in spawnPoint)
            {
                RespawnPointData data = new RespawnPointData();
                data.Position = new System.Numerics.Vector3(point.x, point.y, point.z);
                spawnPoints.Add(data);
            }
            ExportHelper.WriteToFile("spawnPoints", spawnPoints);
        }
        private static List<Vector3> GetPoint(string folder)
        {
            List<Vector3> spawnPoint = new List<Vector3>();
            for (int x = 0; x < map.blocksCount; x++)
            {
                for (int y = 0; y < map.blocksCount; y++)
                {
                  string path = folder + "/TRObject_" + x + '_' + y;
                    ObjectElement objectElement = Resources.Load<ObjectElement>(path);

                    if (objectElement != null && objectElement.mapObjects != null)
                    {
                        for (int i = 0; i < objectElement.mapObjects.Count; i++)
                        {
                            MapObject mapObject = objectElement.mapObjects[i];
                            if (mapObject == null || mapObject.prefab == null) continue;
                            SpawnPoint _point = mapObject.prefab.GetComponent<SpawnPoint>();
                            if (_point != null)
                            {
                               
                                mapObject.prefab.transform.position = mapObject.postion;
                                mapObject.prefab.transform.rotation = mapObject.orientation;
                                mapObject.prefab.transform.localScale = mapObject.scale;
                                Vector3 vector = _point.GetPoint();
                                spawnPoint.Add(vector);
                                Debug.Log("spawn : " + vector);
                               
                            }
                        }
                    }


                }
            }
                    return spawnPoint;
        }
    }
}
#endif