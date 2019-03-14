using Monsters;
using OpenWorld;
using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameLoader : MonoBehaviour
{
    private MapLoader mapLoader;
    public Image progressBar;

    private void Awake()
    {

        progressBar.fillAmount = 0.0f;
        DontDestroyOnLoad(gameObject);
        //Отключить физику
        Time.timeScale = 0.0f;
       // Debug.Log("awake");
    }

    public void LoadGame()
    {
        StartCoroutine(IELoadGame());
    }

    public void LoadPoint()
    {
        StartCoroutine(IELoadPoint());
    }

    private IEnumerator IELoadPoint()
    {

        var managers = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<Manager>();

        foreach(Manager manager in managers)
        {
            manager.AllDestroy();
        }

        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.enabled = false;
        player.GetComponent<AnimationSkill>().DeadOff();//Отключить намацию смерти

        mapLoader = GameObject.Find("Map").GetComponent<MapLoader>();
        mapLoader.DestroyMap();
        mapLoader.LoadMap();

        while (!mapLoader.isDone)
        {
            yield return null;
            progressBar.fillAmount =  mapLoader.progress;
        }

        NetworkWriter nw = new NetworkWriter(Channels.Reliable);
        nw.SetTypePack(Types.MapEntrance);
        NetworkManager.Send(nw);

        Time.timeScale = 1.0f;//Включить физику
        player.enabled = true;
        Destroy(gameObject);

    }
    private IEnumerator IELoadGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Map");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
            progressBar.fillAmount = 0.2f * asyncLoad.progress;
        }

        NetworkWriter nw = new NetworkWriter(Channels.Reliable);
        nw.SetTypePack(Types.MapEntrance);
        NetworkManager.Send(nw);
    
    PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();

        while (!player.enabled)
        {
            yield return null;
        }
      //  print(player.transform.position);
        mapLoader = GameObject.Find("Map").GetComponent<MapLoader>();

        mapLoader.LoadMap();

        while (!mapLoader.isDone)
        {
            yield return null;
            progressBar.fillAmount = 0.2f + ( 0.8f * mapLoader.progress);
        }

       

        Time.timeScale = 1.0f;//Включить физику
       
        Destroy(gameObject);

    }

}
