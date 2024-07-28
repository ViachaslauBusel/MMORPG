using MCamera;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Nickname
{
    public class NicknameRenderer : MonoBehaviour
    {
        [SerializeField]
        private Text _text;
        private CameraController _camera;

        [Inject]
        private void Construct(CameraController camera)
        {
            _camera = camera;
        }


        protected void SetNickname(string nickname)
        {
            _text.text = nickname;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _camera.Camera.transform.position);
        }
    }
}
