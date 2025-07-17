using System;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    [DisallowMultipleComponent]
    public class NetworkManagerHUD_VR : MonoBehaviour
    {
        [SerializeField] private NetworkManager manager;
        [SerializeField] private GameObject hudGameObj, baseOrigin;
        [SerializeField] private Button create, join;
        [SerializeField] private TMP_InputField inputField;

        private void Awake()
        {
            create.onClick.AddListener(delegate { manager.StartHost(); });
            join.onClick.AddListener(delegate { manager.StartClient(); });
            inputField.onEndEdit.AddListener(delegate { manager.networkAddress = inputField.text; });

            inputField.text = inputField.text;
        }

        private void Update()
        {
            if (NetworkClient.isConnected || NetworkServer.active)
            {
                hudGameObj.SetActive(false);
                baseOrigin.SetActive(false);
            }
        }
    }
}