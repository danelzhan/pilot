using System;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NetworkUI : NetworkBehaviour
{
    [SerializeField] private Button m_HostButton;
    [SerializeField] private Button m_JoinButton;
    [SerializeField] private TextMeshProUGUI m_PlayerCountText;
    [SerializeField] private TextMeshProUGUI m_PlayerStatesText;

    public static Action<ulong, string> OnStateChanged;

    private Dictionary<ulong, string> mPlayerStates = new Dictionary<ulong, string>();

    private NetworkVariable<int> mPlayerCount = new NetworkVariable<int>(0);


    void Start()
    {
        m_HostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        }
        );
        m_JoinButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        }
        );

        NetworkManager.Singleton.OnClientConnectedCallback += OnPlayerJoined;
        mPlayerCount.OnValueChanged += ValueChanged;
        m_PlayerStatesText.text = "States: ";
        OnStateChanged += ChangePlayerState;
    }

    private void Update()
    {

    }
  
    private void ValueChanged (int previous, int current)
    {
        m_PlayerCountText.text = "Players: " + mPlayerCount.Value.ToString();
    }

    private void OnPlayerJoined(ulong clientId)
    {
        Debug.Log("Player with ClientID " + clientId + " joined the game.");

        // Host/Server-specific logic
        if (NetworkManager.Singleton.IsServer)
        {
            // Perform server-side actions
            mPlayerCount.Value = NetworkManager.Singleton.ConnectedClients.Count;
        }

        // Client-specific logic
        if (NetworkManager.Singleton.IsClient)
        {
            // Perform client-side actions if needed
        }
    }

    private void ChangePlayerState(ulong playerId, string stateString)
    {
        //ChangePlayerStateRpc(playerId, stateString);
        ChangePlayerStateRpc();
    }


    [Rpc(SendTo.Everyone)]
    public void ChangePlayerStateRpc()
    {
        /*
        Debug.Log(playerId);
        if (!mPlayerStates.ContainsKey(playerId))
        {
            mPlayerStates.Add(playerId, "");
        }
        mPlayerStates[playerId] = stateString;
        m_PlayerStatesText.text = "States: ";
        foreach (KeyValuePair<ulong, string> pair in mPlayerStates)
        {
            m_PlayerStatesText.text = m_PlayerStatesText.text + "\n" + pair.Key.ToString() +"\n" +  pair.Value.ToString();
        }
        */
    }
}
