using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    public static class DrawGizmoHelper
    {
        public static void DrawGizmoCircle(Vector3 center, float radius)
        {
            int segments = 36;
            float angle = 0f;
            Vector3 prevPos = Vector3.zero;

            for (int i = 0; i <= segments; i++)
            {
                angle = i * Mathf.PI * 2f / segments;
                Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius) + center;

                if (i > 0)
                {
                    Gizmos.DrawLine(newPos, prevPos);
                }

                prevPos = newPos;
            }
        }

        public static void DrawGizmoCircleStack(Vector3 position, float spawnRadius, int stackHeight)
        {
            for (int i = 0; i < stackHeight; i++)
            {
                DrawGizmoCircle(position + Vector3.up * 0.5f * i, spawnRadius);
            }
        }

        public static void DrawGizmoSquareStack(Vector3 basePosition, float sideLength, int stackHeight)
        {
            for (int i = 0; i < stackHeight; i++)
            {
                Vector3 positionOffset = Vector3.up * 0.5f * i;
                Vector3 cubeSize = new Vector3(sideLength * 2f, 0, sideLength * 2f);
                Gizmos.DrawWireCube(basePosition + positionOffset, cubeSize);
            }
        }
    }
}
