using UnityEngine;

public class TableMovement : MonoBehaviour
{
    [SerializeField] private PlayerTableMovement[] players;
    [SerializeField] private Rigidbody table;
    
    [Header("Rotation parameters")]
    [SerializeField] private Vector2 maxMovement;
    [SerializeField] private float speedMovement;

    private void FixedUpdate()
    {
        Vector2 goalRotation = Vector2.zero;

        foreach (PlayerTableMovement player in players)
        {
            goalRotation += player.GoalRotation;
        }

        goalRotation /= players.Length;
        Quaternion goalQuaternionRotation = Quaternion.Euler(maxMovement.y * goalRotation.y, 0, maxMovement.x * goalRotation.x);
        
        table.MoveRotation(Quaternion.Lerp(table.rotation, goalQuaternionRotation, speedMovement * Time.deltaTime));
    }
}
