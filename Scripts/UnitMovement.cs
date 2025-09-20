using Unity.VisualScripting;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public static Vector3[] GetUnitGroupDestinations(Vector3 movePos,int numUnits,float unitOffset)
    {
        Vector3[] destinations = new Vector3[numUnits];

        //rows and cols
        int rows = Mathf.RoundToInt(Mathf.Sqrt(numUnits));
        int cols = Mathf.CeilToInt((float)numUnits / (float)rows);

        int curRow = 0;
        int curCol = 0;

        float width = ((float)rows - 1) * unitOffset;
        float length = ((float)cols - 1) * unitOffset;
        for (int x = 0; x < numUnits; x++)
        {
            destinations[x] = movePos + (new Vector3(curRow, 0, curCol) * unitOffset) - new Vector3(length / 2, 0, width / 2);
            curCol++;
        if (curCol == rows)
        {
            curCol = 0;
            curRow++;
        }
    }
    return destinations;
    }

    public static Vector3[] UnitGroupResourceDestination(Vector3 resourcePosition,int numUnits)
    {
        Vector3[] destinations = new Vector3[numUnits];
        float offset = 360.0f / (float)numUnits;
        for(int x=0;x<numUnits;x++)
        {
            float angle = offset * x;
            Vector3 destination = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
            destinations[x] =resourcePosition+destination;
        }
        return destinations;
    }

    public static Vector3 UnitResourceDestination(Vector3 resourcePosition)
    {
        float angle = Random.Range(0, 360);
        Vector3 dir = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
        return resourcePosition + dir;
    }
}
