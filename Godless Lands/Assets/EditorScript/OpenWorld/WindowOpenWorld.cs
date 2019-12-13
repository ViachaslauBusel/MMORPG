#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor
{
    //
    public class WindowOpenWorld : EditorWindow
    {
        private static int activeTools = -1;//Текущий активный инструмент(кнопка)
        public static WindowOpenWorld window;

        [MenuItem("Window/TerrainEditor")]
        public static void ShowWindow()
        {
            WindowOpenWorld window = EditorWindow.GetWindow<WindowOpenWorld>(false, "TerrainEditor");
            window.minSize = new Vector2(200.0f, 100.0f);
           
        }

        private void OnEnable()
        {
            window = this;
            // Add (or re-add) the delegate.
            SceneView.onSceneGUIDelegate += this.OnSceneGUI;
            WindowSetting.Load();//Загрузка скрипта хранящего префабы-списки
            WindowActiveTools.Load();
        }
        private void OnDisable()
        {
            SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
            DestroySceneGUI();
        }

        private void OnGUI()//Отрисовка меню редактора
        {

            if(activeTools != WindowActiveTools.Active())//Если поменялся инструмент, удалить используемые ресурсы на сцене
            {
                DestroySceneGUI();
                activeTools = WindowActiveTools.active; 
            }

            switch (activeTools)
            {
                case 0://Редактирование отключено
                    WorldLoader.Destroy();
                    return;
                case 1:// Отрисовать инструменты для Редактирование террейна/     
                    WindowTerrain.Draw(WindowSetting.Map, WorldLoader.Map);
                    break;
                case 2://Инструменты для Редактирование монстров                 
                   if(WorldLoader.IsHaveMap)
                      WindowMonsterActiveTools.Draw();
                    break;
                case 3://Отрисовать инструменты для редактирование обьектов
                    WindowObject.Draw(WindowSetting.Map, WorldLoader.Map);
                    break;
                case 4://Экпорт на сервер
                        WindowExport.Draw(WindowSetting.Map);
                    break;
                case 5://Настройки редактора карты
                    WindowSetting.Draw();
                    break;
                case 6://Инструменты для Редактирование ресурсов   
                    if (WorldLoader.IsHaveMap)
                        WindowResourcesAcriveTools.Draw();
                    break;
                case 7://Инструменты для Редактирование станков   
                    if (WorldLoader.IsHaveMap)
                        WindowMachineActiveTools.Draw();
                    break;
                case 8://Инструменты для Редактирование NPC                 
                    if (WorldLoader.IsHaveMap)
                        WindowNPCActiveTools.Draw();
                    break;
            }

            
        }

       
        public static void ReloadMap()
        {
            WorldLoader.Reload();
        }
        private void OnSceneGUI(SceneView sceneView)//Отрисовка инструментов на сцене
        {
           // OnGUI();
            if (WorldLoader.IsMap)
            {

                WorldLoader.Update();
            }
            else return;

            switch (activeTools)
            {
                case 0://Редактирование отключено
                    break;
                case 1:// Отрисовать инструменты для Редактирование террейна/  
                    TerrainSceneGUI.SceneGUI(sceneView.camera);
                    break;
                case 2://Инструменты для Редактирование монстров
                    if (WindowMonsterActiveTools.MontersDraw)
                    { MonsterSceneGUI.SceneGUI(sceneView.camera); }
        
                        MonsterVisibleSceneGUI.SceneGUI(WorldLoader.Map);
                    break;
                case 3://Отрисовать инструменты для редактирование обьектов
                    WindowOpenWorld.window.Repaint();
                    break;
                case 4://Экпорт на сервер

                    break;
                case 6:
                    if (WindowResourcesAcriveTools.ResourcesDraw)
                    { ResourcesSceneGUI.SceneGUI(sceneView.camera); }

                    ResourcesVisibleSceneGUI.SceneGUI(WorldLoader.Map);
                    break;
                case 7:
               /*     if (WindowMachineActiveTools.ResourcesDraw)
                    { ResourcesSceneGUI.SceneGUI(sceneView.camera); }*/

                    MachineVisibleSceneGUI.SceneGUI(WorldLoader.Map);
                    break;
                case 8://Инструменты для Редактирование NPC
                    if (WindowNPCActiveTools.NPCDraw)//Если включен режим рисование или добовление нпц
                    { SceneNPCBrush.SceneGUI(sceneView.camera); }

                    NPCVisibleSceneGUI.SceneGUI(WorldLoader.Map);//Отрисовка НПЦ ывокруг
                    break;
            }
            
         
        }

        private void DestroySceneGUI()//Очистка сцены
        {
            TerrainSceneGUI.Destroy();
            MonsterSceneGUI.Destroy();
            SceneNPCBrush.Destroy();
            NPCVisibleSceneGUI.Destroy();
            ResourcesSceneGUI.Destroy(); 
            MonsterVisibleSceneGUI.Destroy();
            ResourcesVisibleSceneGUI.Destroy();
            MachineVisibleSceneGUI.Destroy();
        }

        private void OnDestroy()
        {

            SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
            DestroySceneGUI();
            WorldLoader.Destroy();
        }

 
    }
    
}
#endif