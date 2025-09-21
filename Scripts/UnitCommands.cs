using UnityEngine;
using UnityEngine.Timeline;

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
            if(Physics.Raycast(ray,out hit,100,layerMask))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    MoveUnitsToPoisition(selectedUnits, hit.point);
                    CreateSelectionMarker(hit.point, false);
                }
                else if(hit.collider.CompareTag("Resource"))
                {
                    CreateSelectionMarker(hit.point, true);
                    UnitsGatherResource(selectedUnits, hit.collider.GetComponent<ResourceSource>());
                }
                    
            }

        }
    }

    void MoveUnitsToPoisition(Unit[] units,Vector3 movePosition)
    {
        Vector3[] destinations = UnitMovement.GetUnitGroupDestinations(movePosition, units.Length, 5);
        for (int unit = 0; unit < units.Length; unit++)
            units[unit].MoveToPosition(destinations[unit]);
    }

    void UnitsGatherResource(Unit[] units,ResourceSource resourceSource)
    {
        if (units.Length == 1)
            units[0].GatherResource(resourceSource,UnitMovement.UnitResourceDestination(resourceSource.transform.position));
        else
        {
            Vector3[] destinations = UnitMovement.UnitGroupResourceDestination(resourceSource.transform.position, units.Length);
            for(int x=0;x<units.Length;x++)
            {
                units[x].GatherResource(resourceSource, resourceSource.transform.position);
            }
        }
    }

    void CreateSelectionMarker(Vector3 position,bool large)
    {
        GameObject marker=Instantiate(selectionMarkerPrefab,position + new Vector3(0, 0.01f, 0), Quaternion.identity);

        if (large)
            marker.transform.localScale = Vector3.one * 3;
    }

    
}
