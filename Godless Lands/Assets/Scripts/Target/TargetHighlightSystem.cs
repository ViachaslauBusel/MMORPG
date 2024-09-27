using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units.Registry;
using UnityEngine;
using Zenject;

namespace Target
{
    public class TargetHighlightSystem : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _highlightPrefab;
        private GameObject _highlight;
        private TargetObjectProvider _targetObjectProvider;

        [Inject]
        public void Construct(TargetObjectProvider targetObjectProvider)
        {
            _targetObjectProvider = targetObjectProvider;
            _targetObjectProvider.OnTargetObjectChanged += OnTargetObjectChanged;
        }

        private void Awake()
        {
            _highlight = Instantiate(_highlightPrefab, transform);
            _highlight.SetActive(false);
        }

        private void OnTargetObjectChanged(IUnitVisualObject @object)
        {
            Debug.Log($"Target object changed:{@object}");
            if(@object == null)
            {
                _highlight.SetActive(false);
                _highlight.transform.SetParent(transform, false);
            }
            else
            {
                _highlight.SetActive(true);
                _highlight.transform.SetParent(@object.Transform, false);
            }
        }

        private void OnDestroy()
        {
            _targetObjectProvider.OnTargetObjectChanged -= OnTargetObjectChanged;
        }
    }
}
