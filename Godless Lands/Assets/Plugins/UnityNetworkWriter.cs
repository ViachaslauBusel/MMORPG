using RUCP;
using UnityEngine;

public static class UnityNetworkWriter
{


    public static void write(this NetworkWriter nw, Vector3 vec)
    {
        nw.write(vec.x);
        nw.write(vec.y);
        nw.write(vec.z);
    }

    public static void write(this NetworkWriter nw, Quaternion quat)
    {
        nw.write(quat.w);
        nw.write(quat.x);
        nw.write(quat.y);
        nw.write(quat.z);
    }

    public static Vector3 ReadVec3(this NetworkWriter nw)
    {
        float x = nw.ReadFloat();
        float y = nw.ReadFloat();
        float z = nw.ReadFloat();
        return new Vector3(x, y, z);
    }

    public static Quaternion ReadQuat(this NetworkWriter nw)
    {
        float w = nw.ReadFloat();
        float x = nw.ReadFloat();
        float y = nw.ReadFloat();
        float z = nw.ReadFloat();

        return new Quaternion(x, y, z, w);
    }


}
