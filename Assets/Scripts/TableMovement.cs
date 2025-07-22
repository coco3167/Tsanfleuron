using System;
using System.Collections.Generic;
using UnityEngine;

public class TableMovement : MonoBehaviour
{
    [SerializeField] private bool isVR;
    [SerializeField] private GrabbableButBetter[] grabbableBut;
    [SerializeField] private Rigidbody table;
    
    [Header("Rotation parameters")]
    [SerializeField] private Vector2 maxMovement;
    [SerializeField] private float speedMovement;
    
    private List<PlayerTableMovement> m_players = new();

    [NonSerialized] public Vector2 GoalRotation;

    private void FixedUpdate()
    {
        

        
        GoalRotation = Vector2.zero;

        if (isVR)
        {
            foreach (GrabbableButBetter grabbable in grabbableBut)
            {
                GoalRotation += grabbable.UsableMovement;
                Debug.Log(grabbable.UsableMovement);
            }
            GoalRotation /= grabbableBut.Length;
        }
        else
        {
            if(m_players.Count <= 0)
                return;
            foreach (PlayerTableMovement player in m_players)
            {
                GoalRotation += player.GoalRotation;
                //Debug.Log(player.GetInstanceID());
            }
            GoalRotation /= m_players.Count;
        }

        Quaternion goalQuaternionRotation = Quaternion.Euler(maxMovement.y * GoalRotation.y, 0, maxMovement.x * GoalRotation.x);
        
        table.MoveRotation(Quaternion.Lerp(table.rotation, goalQuaternionRotation, speedMovement * Time.deltaTime));
    }

    public void AddPlayer(PlayerTableMovement player)
    {
        m_players.Add(player);
    }

    public void RemovePlayer(PlayerTableMovement player)
    {
        m_players.Remove(player);
    }
}
