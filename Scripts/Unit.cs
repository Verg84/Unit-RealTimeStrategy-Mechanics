using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject unitSelectionVisual;

    public void ToggleUnitSelectionVisual(bool selection)
    {
        unitSelectionVisual.SetActive(selection);
    }
}
