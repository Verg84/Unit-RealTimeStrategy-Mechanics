using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public GameObject unitSelectionVisual;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void ToggleUnitSelectionVisual(bool selection)
    {
        unitSelectionVisual.SetActive(selection);
    }

    public void MoveToPosition(Vector3 position)
    {
        agent.isStopped = false;
        agent.SetDestination(position);
    }

    public void GatherResource(ResourceSource resource,Vector3 resourcePosition)
    {
        MoveToPosition(resourcePosition);
    }
}
