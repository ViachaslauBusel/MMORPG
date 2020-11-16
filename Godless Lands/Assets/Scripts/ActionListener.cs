using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionListener : MonoBehaviour
{
    private static List<React> actions = new List<React>();

    public Text F;
    private Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    public static void Add(React action)
    {
        actions.Add(action);
    }
    public static void Remove(React action)
    {
        actions.Remove(action);
    }

    private void Update()
    {
        foreach (React action in actions)
        {
            if (Vector3.Distance(player.position, action.position) < action.distance)
            {
                Vector3 toObject = action.position - player.position;
                toObject.y = 0.0f;
                if (Vector3.Angle(player.forward, toObject) < 40.0f)
                {
           
                    if (Input.GetButtonDown("Action"))
                    {
                        action.Use();
                    }
                    F.enabled = true;
                    return;
                }
            }

        }
        F.enabled = false;
    }
}
