using UnityEngine;
using System.Collections.Generic;
public class Player : MonoBehaviour
{
    [Header("Resources")]
    public int food;

    public List<Unit> playerUnits = new List<Unit>();

    public bool IsPlayerUnit(Unit unit)
    {
        return playerUnits.Contains(unit);
    }

    public void GainResource(ResourceType resourceType,int amount)
    {
        switch(resourceType)
        {
            case ResourceType.Food:
                {
                    food += amount;
                    break;
                }
        }
    }
}
