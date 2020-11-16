using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityStandardAssets.Water;


namespace OpenWorld
{
    public class MapLoader : MonoBehaviour
    {

        
        public Transform trackingObj;
        public Map map;
        private int _areaVisible;
        private int _basemapDistance = 50;
        private int _quality;
        private float _detailDistance;
        private float _detailDensity;
      //  public int areaDestroy = 3;
        private GameObject[,] terrainMap;
        private Vector4 border;
        private Material waterMaterial;
        public bool Ready { get; private set; } = false;

        private void Awake()
        {
            Application.backgroundLoadingPriority = ThreadPriority.Low;
            //QualitySettings.masterTextureLimit

            areaVisible = PlayerPrefs.GetInt("areaVisible", 4);
            _basemapDistance = PlayerPrefs.GetInt("basemapDistance", 50);
            Qulity = PlayerPrefs.GetInt("QualityLevel", 1);
            _detailDistance = PlayerPrefs.GetFloat("DetailDistance", 80.0f);
            _detailDensity = PlayerPrefs.GetFloat("DetailDensity", 1.0f);


           
           
        }

        private void Start()
        {
            Ready = true;
            enabled = false;
        }
        public float DetailDistance
        {
            set
            {
                PlayerPrefs.SetFloat("DetailDistance", value);
                _detailDistance = value;
            }
            get
            {
                return _detailDistance;
            }
        }
        public float DetailDensity
        {
            set
            {
                PlayerPrefs.SetFloat("DetailDensity", value);
                _detailDensity = value;
            }
            get
            {
                return _detailDensity;
            }
        }
        public int Qulity
        {
            set
            {
                if (value < 0) value = 0;
                if (value >= QualitySettings.names.Length) value = QualitySettings.names.Length;
                QualitySettings.SetQualityLevel(value);
                PlayerPrefs.SetInt("QualityLevel", value);
                _quality = value;
            }
            get
            {
                return _quality;
            }
        }
        public int basemapDistance
        {
            set
            {
                  PlayerPrefs.SetInt("basemapDistance", value);
                     _basemapDistance = value;
            }
            get
            {
                return _basemapDistance;
            }
        }
        public int areaVisible
        {
            set
            {
                if (value > 7) value = 7;
                if (value < 2) value = 2;
                PlayerPrefs.SetInt("areaVisible", value);
                _areaVisible = value;
            }
            get
            {
               return _areaVisible;
            }
        }

        public bool isDone {
            get {
                if (terrainMap == null) return false;
                int tileCount = terrainMap.GetLength(0) * terrainMap.GetLength(1);//Всего тайлов
                int tileDone = GetTileDone();//Уже загруженные тайлы
                return tileCount == tileDone;
            } }
        public float progress {  get {
                if (terrainMap == null) return 0.0f;
                int tileCount = terrainMap.GetLength(0) * terrainMap.GetLength(1);//Всего тайлов
                int tileDone = GetTileDone();//Уже загруженные тайлы
                return tileDone / (float)tileCount;
            } }

        private int GetTileDone()
        {
            int tileDone = 0;//Уже загруженные тайлы
            for (int x = 0; x < terrainMap.GetLength(0); x++)
            {
                for (int y = 0; y < terrainMap.GetLength(1); y++)
                {
                    if (terrainMap[x, y] == null || !terrainMap[x, y].name.Contains("Load"))
                    {
                        tileDone++;
                    }
                }
            } 
            return tileDone;
        }


        public GameObject[,] TerrainMap
        {
            get  {   return terrainMap; }
        }

        public void DestroyMap()
        {
            enabled = false;
            if (terrainMap == null) return;
            for (int x = 0; x < terrainMap.GetLength(0); x++)
            {
                for (int y = 0; y < terrainMap.GetLength(1); y++)
                {
                    if (terrainMap[x, y] != null)
                    {
                        Destroy(terrainMap[x, y]);
                    }
                }
            }
            terrainMap = null;
            Resources.UnloadUnusedAssets();
        }

        public void LoadMap(Vector3 vector)
        {
            print("Load map in " + vector);
            trackingObj.position = vector;
            LoadMap();
        }
        public void LoadMap()
        {
            RenderSettings.fog = true;
            RenderSettings.fogMode = FogMode.Linear;
            switch (_areaVisible)
            {
                case 2:
                    RenderSettings.fogStartDistance = 150;
                    RenderSettings.fogEndDistance = 230;
                    break;
                case 3:
                    RenderSettings.fogStartDistance = 142;
                    RenderSettings.fogEndDistance = 246;
                    break;
                case 4:
                    RenderSettings.fogStartDistance = 217.9f;
                    RenderSettings.fogEndDistance = 311.6f;
                    break;
                case 5:
                    RenderSettings.fogStartDistance = 243.8f;
                    RenderSettings.fogEndDistance = 364.4f;
                    break;
                case 6:
                    RenderSettings.fogStartDistance = 317.8f;
                    RenderSettings.fogEndDistance = 435.2f;
                    break;
                case 7:
                    RenderSettings.fogStartDistance = 464.3f;
                    RenderSettings.fogEndDistance = 569.01f;
                    break;

            }
     //  RenderSettings.fogStartDistance = (_areaVisible * 100) - 215.0f;
            // RenderSettings.fogEndDistance = (_areaVisible  * 100) - 65.0f;

            waterMaterial = GetComponent<WaterBase>().sharedMaterial;
          //  if (areaVisible > areaDestroy) areaDestroy = areaVisible;
            terrainMap = new GameObject[areaVisible * 2 + 1, areaVisible * 2 + 1];

            CalculateBorder();

            Vector3 startPosition = trackingObj.position;
            if (startPosition.x < 0.0f) startPosition.x = 0.0f;
            if (startPosition.x > map.mapSize * 1000.0f) startPosition.x = map.mapSize * 1000.0f;
            if (startPosition.z < 0.0f) startPosition.z = 0.0f;
            if (startPosition.z > map.mapSize * 1000.0f) startPosition.z = map.mapSize * 1000.0f;

            int xKM = (int)(startPosition.x / 1000.0f);
            int yKM = (int)(startPosition.z / 1000.0f);
            int xTR = (int)((startPosition.x % 1000.0f) / map.blockSize);
            int yTR = (int)((startPosition.z % 1000.0f) / map.blockSize);

            if (xKM < 0) xKM = 0;
            if (xKM >= map.mapSize) xKM = map.mapSize - 1;

            if (yKM < 0) yKM = 0;
            if (yKM >= map.mapSize) yKM = map.mapSize - 1;

            if (xTR < 0) xTR = 0;
            if (xTR >= map.blocksCount) xTR = map.blocksCount - 1;

            if (yTR < 0) yTR = 0;
            if (yTR >= map.blocksCount) yTR = map.blocksCount - 1;

         

            for (int x=0; x<terrainMap.GetLength(0); x++)
            {
                for (int y = 0; y < terrainMap.GetLength(1); y++)
                {
                   CreateTerrain(xKM, yKM, xTR, yTR, x, y);
                }
            }
            Resources.UnloadUnusedAssets();
            enabled = true;
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
            border.w = (trackingObj.position.x - (trackingObj.position.x % map.blockSize)) - map.blockSize * 0.1f;//Left
            border.y = (trackingObj.position.x - (trackingObj.position.x % map.blockSize)) + map.blockSize + map.blockSize * 0.1f;//Right
        }
        private void CalculateBorderY()
        {
         //   Debug.Log("Border Y");
            border.x = (trackingObj.position.z - (trackingObj.position.z % map.blockSize)) - map.blockSize * 0.1f;//Down
            border.z = (trackingObj.position.z - (trackingObj.position.z % map.blockSize)) + map.blockSize + map.blockSize * 0.1f;//Up
        }

        private void Update()
        {
            ChangeBlock();
        }

        public void ChangeBlock()
        {
#if UNITY_EDITOR
            if (trackingObj.position.x < 0.0f) return;
            if (trackingObj.position.x > map.mapSize * 1000.0f) return;
            if (trackingObj.position.z < 0.0f) return;
            if (trackingObj.position.z > map.mapSize * 1000.0f) return;
#endif

            if (trackingObj.position.x < border.w)//left
            {
                //      Debug.Log("left");
                
            //    Debug.Log("left: "+ trackingObj.position);
                CalculateBorderX();

               
                for (int x= terrainMap.GetLength(0)-1; x>=0; x--)
                {
                    for(int y=0; y<terrainMap.GetLength(1); y++)
                    {
                        if (x == terrainMap.GetLength(0) - 1) DestroyImmediate(terrainMap[x, y]);
                        if (x != 0) terrainMap[x, y] = terrainMap[x - 1, y];
                        else CreateTerrain(x, y, Orientation.Left);
                    }
                }
            //    Resources.UnloadUnusedAssets();
            }
            else if(trackingObj.position.x > border.y)//right
            {
                //    Debug.Log("right");
               
             //   Debug.Log("right: " + trackingObj.position);
                CalculateBorderX();
                

                for (int x = 0; x < terrainMap.GetLength(0); x++)
                { 
                    for (int y = 0; y < terrainMap.GetLength(1); y++)
                    {
                        if (x == 0) DestroyImmediate(terrainMap[x, y]);
                        if (x < terrainMap.GetLength(0) - 1) terrainMap[x, y] = terrainMap[x + 1, y];
                        else CreateTerrain(x, y, Orientation.Right);
                    }
                }
              //  Resources.UnloadUnusedAssets();
            }
            else if(trackingObj.position.z < border.x)//up
            {
                //  Debug.Log("down");
               
           //     Debug.Log("down: " + trackingObj.position);
                CalculateBorderY();

                
                for (int y = terrainMap.GetLength(1) - 1; y >= 0; y--)
                {
                    for (int x = 0; x < terrainMap.GetLength(0); x++)
                    {
                        if (y == terrainMap.GetLength(1) - 1) DestroyImmediate(terrainMap[x, y]);
                        if (y != 0) terrainMap[x, y] = terrainMap[x, y-1];
                        else CreateTerrain(x, y, Orientation.Up);
                    }
                }
            //    Resources.UnloadUnusedAssets();
            }
            else if(trackingObj.position.z > border.z)//down
            {
                //    Debug.Log("up");
               
                CalculateBorderY();

                for (int y = 0; y < terrainMap.GetLength(1); y++)
                {
                    for (int x = 0; x < terrainMap.GetLength(0); x++)
                    {
                        if (y == 0) DestroyImmediate(terrainMap[x, y]);
                        if (y < terrainMap.GetLength(1) - 1) terrainMap[x, y] = terrainMap[x , y+1];
                        else CreateTerrain(x, y, Orientation.Down);
                    }
                }
             //   Resources.UnloadUnusedAssets();
            }

        }

        private void CreateTerrain(int x, int y, Orientation orientation)
        {
           
            if (terrainMap[x, y] == null) { return; }//Если прогружен край карты
            TerrainInfo terrainInfo = terrainMap[x, y].GetComponent<TerrainInfo>();
            if (terrainInfo == null) return;
            Vector2Int KMTile = new Vector2Int(terrainInfo.xKM, terrainInfo.yKM);
            Vector2Int TRTile = new Vector2Int(terrainInfo.xTR, terrainInfo.yTR);

            terrainMap[x, y] = null;



            switch (orientation)
            {
                case Orientation.Left:
                    TRTile.x--;
                    if(TRTile.x < 0)
                    {
                        KMTile.x--;
                        TRTile.x += map.blocksCount;
                    }
                    break;
                case Orientation.Right:
                    TRTile.x++;
                    if (TRTile.x >= map.blocksCount)
                    {
                        KMTile.x++;
                        TRTile.x -= map.blocksCount;
                 
                    }
                    break;
                case Orientation.Up:
                    TRTile.y--;
                    if (TRTile.y < 0)
                    {
                        KMTile.y--;
                        TRTile.y += map.blocksCount;
                    }
                    break;
                case Orientation.Down:
                    TRTile.y++;
                    if (TRTile.y >= map.blocksCount)
                    {
                        KMTile.y++;
                        TRTile.y -= map.blocksCount;
                    }
                    break;
            }
            if (KMTile.x < 0 || KMTile.x >= map.mapSize) {  return; }
            if (KMTile.y < 0 || KMTile.y >= map.mapSize) {  return; }

            GameObject obj = new GameObject("LoadingTile");
            obj.transform.SetParent(transform);
            terrainMap[x, y] = obj;
            IEnumerator enumerator = IECreateTerrain(KMTile.x, KMTile.y, TRTile.x, TRTile.y, obj);
          //  StartCoroutine(enumerator);
            enumerators.Enqueue(enumerator);
            if (loadMap == null)
            {
              loadMap = StartCoroutine(IELoadMap());
            }
        }
        private IEnumerator IELoadMap()
        {
            while(enumerators.Count > 0)
            {
                createTerrain = StartCoroutine(enumerators.Dequeue());
                while(createTerrain != null)
                {
                    yield return 0;
                }
            }
            loadMap = null;
            yield return new WaitForSeconds(1.0f);
            Resources.UnloadUnusedAssets();
        }
        private Coroutine createTerrain;
        private Coroutine loadMap;
        private Queue<IEnumerator> enumerators = new Queue<IEnumerator>();

        private void CreateTerrain(int xKMBlock, int yKMBlock, int xTRBlock, int yTRBlock, int x, int y)
        {
           

            xTRBlock -= (areaVisible - x);
            while (xTRBlock < 0)
            {
                xKMBlock--;
                xTRBlock += map.blocksCount;
            }
            while (xTRBlock >= map.blocksCount)
            {
                xKMBlock++;
                xTRBlock -= map.blocksCount;
            }


            yTRBlock -= (areaVisible - y);
            while (yTRBlock < 0)
            {
                yKMBlock--;
                yTRBlock += map.blocksCount;
            }
            while (yTRBlock >= map.blocksCount)
            {
                yKMBlock++;
                yTRBlock -= map.blocksCount;
            }
            if (xKMBlock < 0 || xKMBlock >= map.mapSize) { return; }
            if (yKMBlock < 0 || yKMBlock >= map.mapSize) { return; }
            GameObject obj = new GameObject("LoadingTile");
            terrainMap[x, y] = obj;
            obj.transform.SetParent(transform);

#if UNITY_EDITOR
            StartCoroutine(IECreateTerrain(xKMBlock, yKMBlock, xTRBlock, yTRBlock, obj, false));
#else
            StartCoroutine(IECreateTerrain(xKMBlock, yKMBlock, xTRBlock, yTRBlock, obj, true));
#endif
        }

        private IEnumerator IECreateTerrain(int xKMBlock, int yKMBlock, int xTRBlock, int yTRBlock, GameObject obj, bool async = true)
        {
            if (obj == null) yield break; 
            
            TerrainInfo terrainInfo = obj.AddComponent<TerrainInfo>();
            terrainInfo.xKM = xKMBlock;
            terrainInfo.yKM = yKMBlock;
            terrainInfo.xTR = xTRBlock;
            terrainInfo.yTR = yTRBlock;

            string path = map.mapName + "/KMBlock_" + xKMBlock + '_' + yKMBlock + "/TRBlock_" + xTRBlock + '_' + yTRBlock;

            MapElement mapElement;
            if (async)
            {
               
                ResourceRequest resource = Resources.LoadAsync<MapElement>(path);
                resource.allowSceneActivation = false;
                while (!resource.isDone)
                {
                    yield return 0;
                }
             
                mapElement = resource.asset as MapElement;
            } else
            {
                mapElement = Resources.Load<MapElement>(path);
            }

            if (obj == null)
            {
                Resources.UnloadAsset(mapElement);
                createTerrain = null;
                yield break;
            }

            TerrainData terrainData = mapElement.terrainData;
            
            GameObject terrain_obj = Terrain.CreateTerrainGameObject(terrainData);
            
            Terrain terrain = terrain_obj.GetComponent<Terrain>();
            //SETTING TERRAIN <<<
           // terrain.heightmapPixelError = 1;
            terrain.heightmapPixelError = 5;
            terrain.basemapDistance = _basemapDistance;
            terrain.treeCrossFadeLength = 50.0f;
            terrain.treeBillboardDistance = 100.0f;
            terrain.detailObjectDistance = _detailDistance;
            terrain.detailObjectDensity = _detailDensity;
      
             Vector3 position = new Vector3((xKMBlock*1000.0f)+(xTRBlock * map.blockSize), 0.0f, (yKMBlock * 1000.0f) + (yTRBlock * map.blockSize));
            terrain_obj.transform.position = position;
            terrain_obj.layer = LayerMask.NameToLayer("Terrain");
             

           

                terrain_obj.transform.SetParent(obj.transform);

                //Water
                if(mapElement.waterTile != null)
                {
                    GameObject water = new GameObject("WaterTile");
                    MeshFilter meshFilter = water.AddComponent<MeshFilter>();
                    MeshRenderer meshRenderer = water.AddComponent<MeshRenderer>();
                    WaterTile waterTile = water.AddComponent<WaterTile>();

                    meshFilter.mesh = mapElement.waterTile;
                    meshRenderer.material =waterMaterial;
                    waterTile.reflection = GetComponent<PlanarReflection>();
                    waterTile.waterBase = GetComponent<WaterBase>();
                    water.transform.SetParent(obj.transform);
                    water.transform.position = new Vector3(0.0f, map.waterLevel, 0.0f) + terrain_obj.transform.position;

                }

            //Map Objet
           
            path = map.mapName + "/KMObject_" + xKMBlock + '_' + yKMBlock+ "/TRObject_" + xTRBlock + '_' + yTRBlock;
         //   string fileName = "/TRObject_" + xTRBlock + '_' + yTRBlock;
            ObjectElement objectElement;
            if (async)
            {

              /*  float time = Time.timeSinceLevelLoad;
                float min = 10.0f;
                float max = 0.0f;
                float total = 0.0f;
                int count = 0;*/

                ResourceRequest assetLoadRequest = Resources.LoadAsync<ObjectElement>(path);
                assetLoadRequest.allowSceneActivation = false;
                while (!assetLoadRequest.isDone)
                {
                   
                    yield return 0;
                 /*   float debug = Time.timeSinceLevelLoad - time;
                    time = Time.timeSinceLevelLoad;
                    if (min > debug) min = debug;
                    if (max < debug) max = debug;
                    total += debug;
                    count++;*/
                }
                objectElement = assetLoadRequest.asset as ObjectElement;
             /*   float mid = total / count;
                Debug.Log("Mid Time:" + (mid * 1000.0f) + " min: " + (min * 1000) + " max: " + (max * 1000) + " total: " + (total * 1000) + " calls: " + count);*/

            }
            else
            {
                objectElement = Resources.Load<ObjectElement>(path);
            }
              
            if (obj == null)
            {
                Resources.UnloadAsset(objectElement);
                createTerrain = null;
                yield break;
            }

            if (objectElement != null && objectElement.mapObjects != null)
            {
                for (int i=0; i< objectElement.mapObjects.Count; i++)
                {
                    MapObject mapObject = objectElement.mapObjects[i];
                    if (mapObject == null || mapObject.prefab == null) continue;
                    GameObject _gameObject = Instantiate(mapObject.prefab);
                    _gameObject.transform.SetParent(obj.transform);
                    _gameObject.transform.position = mapObject.postion;
                    _gameObject.transform.rotation = mapObject.orientation;
                    _gameObject.transform.localScale = mapObject.scale;


                  
#if UNITY_EDITOR
                    terrainInfo.ObjectElement = objectElement;
                    terrainInfo.mapObjects.Add(mapObject.GetHashCode(), _gameObject);
#else
                    yield return 0;
#endif
                }
            }

            obj.name = "Tile";
            createTerrain = null;
        }
    }
}