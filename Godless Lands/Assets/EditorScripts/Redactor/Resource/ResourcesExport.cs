#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Resource
{
    public class ResourcesExport
    {
        public static void Export(WorldResourcesList worldResourcesList, ResourceList resourceList)
        {
            using (BinaryWriter stream_out = new BinaryWriter(File.Open(@"Export/resources.dat", FileMode.Create)))
            {
                foreach (WorldFabric worldFabric in worldResourcesList.worldResources)
                {
                    Fabric fabric = resourceList.GetFabric(worldFabric.id);
                    stream_out.Write(worldFabric.id);//ID
                    stream_out.Write(worldFabric.point.x);
                    stream_out.Write(worldFabric.point.y);
                    stream_out.Write(worldFabric.point.z);
                    stream_out.Write(worldFabric.radius);

                    //Spawn Time
                    stream_out.Write((int)(fabric.startSpawn * 1000));
                    stream_out.Write((int)(fabric.timeSpawn * 1000));

                    stream_out.Write((int)fabric.profession);
                    stream_out.Write(fabric.exp);
                    stream_out.Write(fabric.stamina);

                    //Drop
                    stream_out.Write(fabric.drops.Count);
                    foreach(Drop drop in fabric.drops)
                    {
                        stream_out.Write(drop.itemID);
                        stream_out.Write(drop.chance);
                    }
                }
            }
        }
    }
}
#endif