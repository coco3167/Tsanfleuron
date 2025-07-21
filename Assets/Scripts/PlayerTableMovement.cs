using System;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputAction.CallbackContext))]
public class PlayerTableMovement : MonoBehaviour
{

    [NonSerialized] public Vector2 GoalRotation;
    [NonSerialized, ShowInInspector] public Position PlayerPosition;
    
    [SerializeField] private InputActionReference movement; 
    [SerializeField] private float movementSpeed;
    
    [Header("Rotation parameters")]
    [SerializeField] private Vector2 maxMovement;
    [SerializeField] private float speedMovement;
    // [SerializeField] private TMP_Dropdown dropdown;

    private InputAction m_movementAction;
    private Vector2[] m_transformationMatrix = {Vector2.zero, Vector2.zero };
    private Vector2 m_rawRotation;

    private Transform m_playerVisualization;

    
    private void Awake()
    {
        m_movementAction = movement.action;

        GetComponent<PlayerInput>().onActionTriggered += OnAction;
        
        // dropdown.onValueChanged.AddListener(delegate (int index)
        // {
        //     m_position = (Position)index;
        // });
    }
    
    public void Initialize(Position position, Transform playerVisualization)
    {
        PlayerPosition = position;
        m_playerVisualization = playerVisualization;
        
        switch (PlayerPosition)
        {
            case Position.LeftDown:
                m_transformationMatrix = new Vector2[]
                {
                    new (-1, -1),
                    new (-1, 1),
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
        
        Quaternion goalQuaternionRotation = Quaternion.Euler(maxMovement.y * GoalRotation.y, 0, maxMovement.x * GoalRotation.x);
        
        m_playerVisualization.rotation = Quaternion.Lerp(m_playerVisualization.rotation, goalQuaternionRotation, speedMovement * Time.deltaTime);
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        InputAction action = context.action;
        if (action.id.Equals(m_movementAction.id))
        {
            if (context.performed)
            {
                m_rawRotation = action.ReadValue<Vector2>();
            }
            else if (context.canceled)
            {
                m_rawRotation = Vector2.zero;
            }
        }
    }

    
    
    public enum Position
    {
        LeftUp,
        LeftDown,
        RightUp,
        RightDown,
    }
}
