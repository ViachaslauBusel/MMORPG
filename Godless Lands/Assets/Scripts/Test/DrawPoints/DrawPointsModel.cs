using Protocol.MSG.Game.Test;
using System;
using System.Collections.Generic;

namespace Test.DrawPoints
{
    public class DrawPointsModel
    {
        private List<Point> _points = new List<Point>();


        public event Action OnPointsChanged;

        public IReadOnlyCollection<Point> Points => _points;

        public void UpdatePoints(List<Point> points)
        {
            _points.Clear();
            _points = points;
            OnPointsChanged?.Invoke();
        }

        public void ClearPoints()
        {
            _points.Clear();
            OnPointsChanged?.Invoke();
        }
    }
}