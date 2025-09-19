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
}
