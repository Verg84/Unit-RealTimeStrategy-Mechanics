using UnityEngine;
using System.Collections.Generic;
public class Player : MonoBehaviour
{
    public List<Unit> playerUnits = new List<Unit>();

    public bool IsPlayerUnit(Unit unit)
    {
        return playerUnits.Contains(unit);
    }
}
