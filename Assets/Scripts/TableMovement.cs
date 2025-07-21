using System.Collections.Generic;
using UnityEngine;

public class TableMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody table;
    
    [Header("Rotation parameters")]
    [SerializeField] private Vector2 maxMovement;
    [SerializeField] private float speedMovement;
    
    private List<PlayerTableMovement> m_players = new();

    private void FixedUpdate()
    {
        if(m_players.Count <= 0)
            return;
        
        Vector2 goalRotation = Vector2.zero;

        foreach (PlayerTableMovement player in m_players)
        {
            goalRotation += player.GoalRotation;
            //Debug.Log(player.GetInstanceID());
        }

        goalRotation /= m_players.Count;
        Quaternion goalQuaternionRotation = Quaternion.Euler(maxMovement.y * goalRotation.y, 0, maxMovement.x * goalRotation.x);
        
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
