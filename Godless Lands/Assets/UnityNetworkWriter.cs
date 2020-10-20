using RUCP.Packets;
using UnityEngine;

public static class UnityNetworkWriter
{


    public static void write(this Packet nw, Vector3 vec)
    {
        nw.WriteFloat(vec.x);
        nw.WriteFloat(vec.y);
        nw.WriteFloat(vec.z);
    }

    public static void write(this Packet nw, Quaternion quat)
    {
        nw.WriteFloat(quat.w);
        nw.WriteFloat(quat.x);
        nw.WriteFloat(quat.y);
        nw.WriteFloat(quat.z);
    }

    public static Vector3 ReadVector3(this Packet nw)
    {
        float x = nw.ReadFloat();
        float y = nw.ReadFloat();
        float z = nw.ReadFloat();
        return new Vector3(x, y, z);
    }

    public static Quaternion ReadQuat(this Packet nw)
    {
        float w = nw.ReadFloat();
        float x = nw.ReadFloat();
        float y = nw.ReadFloat();
        float z = nw.ReadFloat();

        return new Quaternion(x, y, z, w);
    }


}
