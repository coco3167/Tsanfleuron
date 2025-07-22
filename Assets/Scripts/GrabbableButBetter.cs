using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableButBetter : MonoBehaviour
{
    [NonSerialized] public Vector2 UsableMovement;
    
    [SerializeField] private float maxDistance;
    
    private Transform m_interactorTransform;
    private Vector3 m_awakePos, m_interactorStartPosition;

    private bool m_isBeingGrabbed;

    private void Awake()
    {
        m_awakePos = transform.position;
    }

    public void OnStartSelect(SelectEnterEventArgs args)
    {
        m_isBeingGrabbed = true;
        m_interactorTransform = args.interactorObject.transform;
        m_interactorStartPosition = m_interactorTransform.position;
    }

    public void OnEndSelect(SelectExitEventArgs args)
    {
        m_isBeingGrabbed = false;

        transform.position = m_awakePos;
    }

    private void FixedUpdate()
    {
        if (!m_isBeingGrabbed)
        {
            transform.position = m_awakePos;
            UsableMovement = Vector2.zero;
            return;
        }

        Vector3 interactorMovement = -m_interactorStartPosition + m_interactorTransform.position;

        interactorMovement = interactorMovement.normalized * Math.Min(interactorMovement.magnitude, maxDistance);

        Vector3 tempMovement = transform.TransformVector(interactorMovement);
        UsableMovement = new Vector2(tempMovement.z, tempMovement.y);
        

        transform.position = m_awakePos + interactorMovement;
    }
    
    // En gros l'idée mastah c'est de transformer la différence de déplacement de l'objet original en Vector2 (plan x et y, nique le Z)
    // L'idée c'est de reprendre le fonctionnement de PlayerTableMovement et l'appliquer là à ce Vector2 et ensuite zou direction la table
    // C'est la table, c'est la table, c'est la table, c'est la table
    // Merci Dora l'exploratrice que ferais-je sans toi
}
