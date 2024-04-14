using Helpers;
using Protocol.MSG.Game.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Test.DrawPoints
{
    public class PointsRenderer
    {
        private DrawPointsModel _drawPointsModel;
        private List<GameObject> _pointObjects = new List<GameObject>();

        public PointsRenderer(DrawPointsModel drawPointsModel)
        {
            _drawPointsModel = drawPointsModel;
            _drawPointsModel.OnPointsChanged += OnPointsChanged;
        }

        private void OnPointsChanged()
        {
            DestroyAllPoints();

            foreach (var point in _drawPointsModel.Points)
            {
                var pointObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                pointObject.transform.position = point.Position.ToUnity();
                pointObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                pointObject.GetComponent<Renderer>().material.color = GetColor(point.Color);
                GameObject.Destroy(pointObject.GetComponent<SphereCollider>());
                _pointObjects.Add(pointObject);
            }
        }

        private Color GetColor(PointColor color) => color switch
        {
            PointColor.Red => Color.red,
            PointColor.Green => Color.green,
            PointColor.Blue => Color.blue,
            _ => Color.white
        };

        private void DestroyAllPoints()
        {
            foreach (var pointObject in _pointObjects)
            {
                GameObject.Destroy(pointObject);
            }
            _pointObjects.Clear();
        }
    }
}
