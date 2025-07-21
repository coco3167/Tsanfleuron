using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PlayerTableMovement))]
public class VRPlayer : MonoBehaviour
{
    [SerializeField] private TableMovement table;

    private PlayerTableMovement m_playerTableMovement;
    
    private void Awake()
    {
        m_playerTableMovement = GetComponent<PlayerTableMovement>();
    }

    private void FixedUpdate()
    {
        m_playerTableMovement.GoalRotation = Random.insideUnitCircle;
    }
}
