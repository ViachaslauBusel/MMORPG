using RUCP;
using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TestRay : MonoBehaviour
{
    [SerializeField] Transform target;
    public bool activeted = false;
    public float timeSend = 2.0f;
    private float timer;
    private Queue<Vector3> hitPoints;


    [Inject]
    private void Construct(NetworkManager networkManager)
    {
        this.networkManager = networkManager;
       // networkManager.RegisterHandler(Types.TestRay, VoidTestRay);
    }

    private void Awake()
    {
        
        hitPoints = new Queue<Vector3>();
    }

    private void VoidTestRay(Packet nw)
    {
        while (nw.AvailableBytesForReading > 0)
        {
            if (nw.ReadBool())
            {
                hitPoints.Enqueue(nw.ReadVector3());
            }
            else
            {
                print("dont hit");
            }

            if (hitPoints.Count > 1100) hitPoints.Dequeue();
        }
    }

    private void Start()
    {
        timer = timeSend;
    }

    private void Update()
    {
        if (Input.GetButton("Jump") && isSend)
        {
            isSend = false;
            print("Send tile");
            StartCoroutine(send());
        }
    }
     bool isSend = true;
    private NetworkManager networkManager;

    private IEnumerator send()
    {
    //TODO msg
        //Packet nw = new Packet(Channel.Reliable);
        //nw.WriteType(Types.TestRay);
        //int startX = (int)(target.position.x) - 5;
        //int startZ = (int)(target.position.z) - 5;
        //for (int y = 0; y < 10; y++)
        //{
        //    for (int x = 0; x < 10; x++)
        //    {

        //        Vector3 vector = new Vector3(startX + x, target.position.y, startZ + y);
        //        nw.WriteVector3(vector);
        //    }
        //}
        //NetworkManager.Send(nw);
        yield return new WaitForSeconds(1.0f);
        isSend = true;
       
    }

    private void OnDrawGizmos()
    {
    //   print("draw f");
    /*    foreach(Vector3 hit in hitPoints)
        {
        //    print("Draw: " + hit);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hit, 0.2f);
        }*/
    }

    private void OnDestroy()
    {

       // networkManager?.UnregisterHandler(Types.TestRay);
    }
}
