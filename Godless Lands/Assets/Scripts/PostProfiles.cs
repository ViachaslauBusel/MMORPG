using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PostProfiles : MonoBehaviour
{
    private int _profile;
    private GameObject[] profiles;


    private void Awake()
    {
        
        profiles = new GameObject[transform.childCount];
        for (int i = 0; i < profiles.Length; i++)
        {
            profiles[i] = transform.GetChild(i).gameObject;
        }
        Profile = PlayerPrefs.GetInt("PostProcessProfile", 1);
    }

    public string[] GetProfiles()
    {
        string[] names = new string[profiles.Length];
        for (int i = 0; i < profiles.Length; i++)
        {
            names[i] = profiles[i].name;
        }
        return names;
    }

    public int Profile
    {
        get { return _profile; }
        set
        {
            _profile = value;
            OffAll();
             profiles[value].SetActive(true);
            PlayerPrefs.SetInt("PostProcessProfile", value);
        }
    }

    public void OffAll()
    {
        foreach(GameObject volume in profiles)
        {
            volume.SetActive(false);
        }
    }
}
