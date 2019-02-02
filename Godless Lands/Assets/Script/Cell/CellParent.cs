using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cells
{
    public class CellParent : MonoBehaviour
    {
        public GraphicRaycaster[] graphicRaycasters;
        public  Transform parentTransform;

       /* private void Start()
        {
            List<GraphicRaycaster> raycasters = new List<GraphicRaycaster>();
            raycasters.Add(GetComponent<GraphicRaycaster>());
            if(graphicRaycasters != null)
            {
                foreach(GraphicRaycaster graphic in graphicRaycasters)
                {
                    if(graphic != null)
                    {
                        raycasters.Add(graphic);
                    }
                }
            }
            graphicRaycasters = raycasters.ToArray();
            thisTransform = transform;
        }*/

        public GraphicRaycaster[] raycasters { get { return graphicRaycasters; } }
        public Transform parent { get { return parentTransform; } }
    }
}