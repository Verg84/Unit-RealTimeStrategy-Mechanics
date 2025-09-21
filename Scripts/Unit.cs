using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public enum UnitState { Idle, Move, MoveToResource, Gather }

public class Unit : MonoBehaviour
{
    public UnitState state;
    public int gatherAmount;
    public float gatherRate;
    private float lastGatherTime;

    private ResourceSource currentResourceSource;

    //Events
    public class StateChangeEvent : UnityEvent<UnitState> { }
    public StateChangeEvent onStateChange;
    public Player player;

    public GameObject unitSelectionVisual;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        
        switch(state)
        {
            case UnitState.Move:
                {
                    MoveStateUpdate();
                    break;
                }
            case UnitState.MoveToResource:
               {
                    MoveToResourceStateUpdate();
                    break;
               }
            case UnitState.Gather:
                {
                    GatherStateUpdate();
                    break;
                }

                

        }
    }

    void SetState(UnitState toState)
    {
        
        state = toState;
        if (onStateChange != null)
            onStateChange.Invoke(state);
        if(toState==UnitState.Idle)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }
    }

    void MoveStateUpdate() 
    {

        if (Vector3.Distance(transform.position,agent.destination)==0.0f)
        {
            SetState(UnitState.Idle);
        }

    }
    void MoveToResourceStateUpdate()
    {
        if (currentResourceSource==null)
        {
            SetState(UnitState.Idle);
            return;
        }
        if (Vector3.Distance(transform.position, agent.destination) == 0.0f)
            SetState(UnitState.Gather);
    }
    void GatherStateUpdate()
    {
        if (currentResourceSource==null)
        {
            SetState(UnitState.Idle);
            return;
        }
        LookAt(currentResourceSource.transform.position);
        if(Time.time-lastGatherTime>gatherRate)
        {
            lastGatherTime = Time.time;
            currentResourceSource.GatherResource(gatherAmount, player);
        }
    }

    void LookAt(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
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
        currentResourceSource = resource;
        SetState(UnitState.MoveToResource);
        agent.isStopped = false;
        agent.SetDestination(resourcePosition);
    }
}
