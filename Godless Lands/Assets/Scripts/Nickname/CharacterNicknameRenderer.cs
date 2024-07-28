using MCamera;
using Network;
using Network.Replication;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Nickname
{
    public class CharacterNicknameRenderer : NicknameRenderer
    {
        private SessionManagmentService _sessionManagmentService;
        private UnitNameHandler _unitNameHandler;

        [Inject]
        private void Construct(SessionManagmentService sessionManagmentService)
        {
            _sessionManagmentService = sessionManagmentService;
        }

        private void Awake()
        {
            _unitNameHandler = GetComponentInParent<UnitNameHandler>();
           
            var objInfo = GetComponentInParent<NetworkComponentsProvider>();

            //IF this is our character, we don't need to render nickname
            if(objInfo.NetworkGameObjectID == _sessionManagmentService.CharacterObjectID)
            {
                gameObject.SetActive(false);
                return;
            }

            if(_unitNameHandler == null)
            {
                Debug.LogError("UnitNameHandler not found");
                return;
            }

            _unitNameHandler.OnNameChanged += SetNickname;
            SetNickname(_unitNameHandler.UnitName);
        }

        private void OnDestroy()
        {
            if (_unitNameHandler != null)
            {
                _unitNameHandler.OnNameChanged -= SetNickname;
            }
        }
    }
}