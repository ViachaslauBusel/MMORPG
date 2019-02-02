#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OpenWorld
{
    public class TerrainPoints
    {

        private TerrainInfo terrainInfo;
        private List<Point> points;

        public TerrainPoints(TerrainInfo terrainInfo)
        {
            this.terrainInfo = terrainInfo;
            points = new List<Point>();
        }

        public void AddPoint(Point point)
        {
            points.Add(point);
        }

        public TerrainInfo GetTerrainInfo()
        {
            return terrainInfo;
        }

        public List<Point> GetPoints()
        {
            return points;
        }

        public bool Equals(TerrainInfo terrainInfo)
        {
            return this.terrainInfo.Equals(terrainInfo);
        }
       
    }

    public struct Point
    {
        public int x;
        public int y;
        public float smooth;
        public bool raise;

        public Point(int x, int y, float smooth, bool raise = false)
        {
            this.x = x;
            this.y = y;
            this.smooth = smooth;
            this.raise = raise;
        }
    }

    public static class ListTerrainPoints
    {

        public static void AddPoint(this List<TerrainPoints> listPoints, TerrainInfo terrainInfo, int x, int y, float smooth, bool raise = false)
        {
            foreach (TerrainPoints terrainPoint in listPoints)
            {
                if (terrainPoint.Equals(terrainInfo))
                {
                    terrainPoint.AddPoint(new Point(x, y, smooth, raise));
                    return;
                }
            }

            TerrainPoints _terrainPoint = new TerrainPoints(terrainInfo);
            _terrainPoint.AddPoint(new Point(x, y, smooth, raise));

            listPoints.Add(_terrainPoint);
        }
    }
}
#endif