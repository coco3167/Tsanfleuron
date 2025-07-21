using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerManager : MonoBehaviour
{
    private static Dictionary<PlayerTableMovement.Position, bool> _playerPositions = new();
    
    [SerializeField] private TableMovement table;
    [SerializeField] private GameObject[] playerVisualization;

    private PlayerInputManager m_playerInputManager;

    private void Awake()
    {
        m_playerInputManager = GetComponent<PlayerInputManager>();
        m_playerInputManager.onPlayerJoined += OnPlayerJoined;
        m_playerInputManager.onPlayerLeft += OnPlayerLeft;

        for (int loop = 0; loop < Enum.GetNames(typeof(PlayerTableMovement.Position)).Length; loop++)
        {
            _playerPositions.Add((PlayerTableMovement.Position)loop, false);
        }
        
        foreach (GameObject gameObj in playerVisualization)
        {
            gameObj.SetActive(false);
        }
    }

    private void OnPlayerJoined(PlayerInput input)
    {
        PlayerTableMovement player = input.GetComponent<PlayerTableMovement>();

        PlayerTableMovement.Position position = _playerPositions.First(x => !x.Value).Key;
        GameObject gameObj = playerVisualization[(int)position];
        
        player.Initialize(position, gameObj.transform);
        gameObj.SetActive(true);
        
        _playerPositions[position] = true;
        table.AddPlayer(player);
    }

    private void OnPlayerLeft(PlayerInput input)
    {
        PlayerTableMovement player = input.GetComponent<PlayerTableMovement>();

        _playerPositions[player.PlayerPosition] = false;
        
        playerVisualization[(int)player.PlayerPosition].SetActive(true);

        table.RemovePlayer(player);
    }
}
