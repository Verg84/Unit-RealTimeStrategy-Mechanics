using UnityEngine;

public class UnitCommands : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject selectionMarkerPrefab;

    private Camera cam;
    UnitSelection unitSelection;

    

    private void Awake()
    {
        cam = Camera.main;
        unitSelection = GetComponent<UnitSelection>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && unitSelection.ContainSelectedUnits())
        {
            Debug.Log("RMB pressed");

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Unit[] selectedUnits = unitSelection.GetSelectedUnits();
            Debug.Log($"{selectedUnits.Length} units selected");
            if(Physics.Raycast(ray,out hit,200,layerMask))
            {
                Debug.Log("Ground hit");
                if (hit.collider.CompareTag("Ground"))
                    MoveUnitsToPoisition(selectedUnits,hit.point);
                CreateSelectionMarker(hit.point);
            }

        }
    }

    void MoveUnitsToPoisition(Unit[] units,Vector3 movePosition)
    {
        Vector3[] destinations = UnitMovement.GetUnitGroupDestinations(movePosition, units.Length, 5);
        for (int unit = 0; unit < units.Length; unit++)
            units[unit].MoveToPosition(destinations[unit]);
    }

    void CreateSelectionMarker(Vector3 position)
    {
        Instantiate(selectionMarkerPrefab,position + new Vector3(0, 0.01f, 0), Quaternion.identity);
    }
}
