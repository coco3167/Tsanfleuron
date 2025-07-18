using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTableMovement : MonoBehaviour
{
    [NonSerialized] public Vector2 GoalRotation;
    
    [SerializeField] private InputActionReference movement; 
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private TMP_Dropdown dropdown;

    private InputAction m_movementAction;
    private Position m_position;
    
    private void Awake()
    {
        m_movementAction = movement.action;
        dropdown.onValueChanged.AddListener(delegate (int index)
        {
            m_position = (Position)index;
        });
    }

    private void FixedUpdate()
    {
        Vector2 rawRotation = m_movementAction.ReadValue<Vector2>();
        // Quaternion goalRotation = Quaternion.Euler(maxRotation * rotation.y, 0, maxRotation * -rotation.x);
        //
        // table.MoveRotation(Quaternion.Lerp(table.rotation, goalRotation, rotationSpeed*Time.deltaTime));
        
        Vector2 rotation = Vector2.zero;
        switch (m_position)
        {
            case Position.LeftDown:
                rotation.x = -rawRotation.x/2 - rawRotation.y/2;
                rotation.y = -rawRotation.x/2 + rawRotation.y/2;
                break;
            
            case Position.LeftUp:
                rotation.x = rawRotation.x/2 - rawRotation.y/2;
                rotation.y = -rawRotation.x/2 - rawRotation.y/2;
                break;
            
            case Position.RightDown:
                rotation.x = -rawRotation.x/2 + rawRotation.y/2;
                rotation.y = rawRotation.x/2 + rawRotation.y/2;
                break;
            
            case Position.RightUp:
                rotation.x = rawRotation.x/2 + rawRotation.y/2;
                rotation.y = rawRotation.x/2 - rawRotation.y/2;
                break;
        }
        
        GoalRotation = Vector2.Lerp(GoalRotation, rotation, movementSpeed);
    }
    
    private enum Position
    {
        LeftUp,
        LeftDown,
        RightUp,
        RightDown,
    }
}
