using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputAction.CallbackContext))]
public class PlayerTableMovement : MonoBehaviour
{
	private static int _playerIndex;

    [NonSerialized] public Vector2 GoalRotation;
    
    [SerializeField] private InputActionReference movement; 
    
    [SerializeField] private float movementSpeed;
    // [SerializeField] private TMP_Dropdown dropdown;

    private InputAction m_movementAction;
    private Vector2[] m_transformationMatrix;
    private Vector2 m_rawRotation;
    
    private void Awake()
    {
        m_movementAction = movement.action;

        GetComponent<PlayerInput>().onActionTriggered += OnAction;
        
        switch ((Position)_playerIndex)
        {
            case Position.LeftDown:
                m_transformationMatrix = new Vector2[]
                {
                    new (-1, -1),
                    new (-1, -1),
                };
                break;
            
            case Position.LeftUp:
                m_transformationMatrix = new Vector2[]
                {
                    new (1, -1),
                    new (-1, -1),
                };
                break;
            
            case Position.RightDown:
                m_transformationMatrix = new Vector2[]
                {
                    new (-1, 1),
                    new (1, 1),
                };
                break;
            
            case Position.RightUp:
                m_transformationMatrix = new Vector2[]
                {
                    new (1, 1),
                    new (1, -1),
                };
                break;
        }
        
        _playerIndex++;

        // dropdown.onValueChanged.AddListener(delegate (int index)
        // {
        //     m_position = (Position)index;
        // });
    }

    private void FixedUpdate()
    {
        //Vector2 rawRotation = m_movementAction.ReadValue<Vector2>();
        // Quaternion goalRotation = Quaternion.Euler(maxRotation * rotation.y, 0, maxRotation * -rotation.x);
        //
        // table.MoveRotation(Quaternion.Lerp(table.rotation, goalRotation, rotationSpeed*Time.deltaTime));
        
        Vector2 rotation = Vector2.zero;
        rotation.x = Vector2.Dot(m_rawRotation,  m_transformationMatrix[0])/2;
        rotation.y = Vector2.Dot(m_rawRotation, m_transformationMatrix[1])/2;
        
        /*switch (m_position)
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
        }*/
        
        GoalRotation = Vector2.Lerp(GoalRotation, rotation, movementSpeed);
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        InputAction action = context.action;
        if (action.Equals(m_movementAction))
        {
            if(context.performed)
                m_rawRotation = action.ReadValue<Vector2>();
            else if(context.canceled)
                m_rawRotation = Vector2.zero;
        }
    }
    
    private enum Position
    {
        LeftUp,
        LeftDown,
        RightUp,
        RightDown,
    }
}
