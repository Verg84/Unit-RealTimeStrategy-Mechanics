# Unit Movement and Location Indicator

## <ins>SelectionMarker.cs<ins>
Component attached to the Location Indicator Game Object to destroy the game object after time passed.
```
public float lifetime = 1.0f;
private void Start()
{
  Destroy(gameObject, lifetime);
}
```

## <ins>UnitSelection.cs<ins>
### public bool ContainSelectedUnits()
Check if a Unit has already been selected
```
return selectedUnits.Count > 0 ? true : false;    
```
### public Unit[] GetSelectedUnits()
Return an array of the selected objects
```
return selectedUnits.ToArray();
```

## <ins>Unit.cs<ins>
Initial setup Moving a Unit using **NavMeshAgent** component. 
```
using UnityEngine.AI;
...
private NavMeshAgent agent;
private void Awake()
{
  agent = GetComponent<NavMeshAgent>();
};
```
### public void MoveToPosition(Vector3 position)
Use the **NavMeshAgent** properties to move to position
```
agent.isStopped = false;
agent.SetDestination(position);
```

## <ins>UnitMovement.cs</ins>
Contains **static** functions for the movement of Units in different formations
### public static Vector3[] GetUnitGroupDestinations(Vector3 movePos,int numUnits,float unitOffset)
```
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
```
Static methods can be called directly from another class e.g:  
*UnitMovement.GetUnitGroupDestinations(...);* 

## <ins>UnitCommands.cs</ins>
Detect input from right mouse button to move a Unit to position using raycasting.  
The variables are required
```
public LayerMask layerMask
public GameObject selectionIndicatorPrefab
private Camera cam
```
Also a reference to the **UnitSelection** class, to call functions regarding Units already selected
```
UnitSelection unitSelection;
```
### void MoveUnitsToPoisition(Unit[] units,Vector3 movePosition)
Called on _Input.GetMouseButton(1)_ , calculate the destinations each selected unit
```
Vector3[] destinations = UnitMovement.GetUnitGroupDestinations(movePosition, units.Length, 5);
```
Loop over the selected objects to call their **public** _MoveToPosition(Vector3 position)_ method:
```
for (int unit = 0; unit < units.Length; unit++)
            units[unit].MoveToPosition(destinations[unit]);
```
### void CreateSelectionMarker(Vector3 position)
Instantiate the location indicator prefab game object at given 3D location
```
Instantiate(selectionMarkerPrefab,position + new Vector3(0, 0.01f, 0), Quaternion.identity);
```

### void Update()
On ecah franme check for mouse right input(_Input.GetMouseButton(1)_)
```
if(Input.GetMouseButtonDown(1) && unitSelection.ContainSelectedUnits())
```
If RMB is pressed **and** there are Units selected, create a ray based on mouse position
```
Ray ray = cam.ScreenPointToRay(Input.mousePosition);
RaycastHit hit;
```
Create a new array with the selected Units using the _UnitSelection_ class and cast the ray based on layer mask "Ground"
```
Unit[] selectedUnits = unitSelection.GetSelectedUnits();
if(Physics.Raycast(ray,out hit,100,layerMask))
```
Finally if the ray find any collider,under the "Ground" tag mask 
```
if (hit.collider.CompareTag("Ground"))
```
... use the location at intersection 3D point (_hit.point_ ) and call the  
function to instantiate the location indicator object and the Unit's movement command method:
```
MoveUnitsToPoisition(selectedUnits,hit.point);
CreateSelectionMarker(hit.point);
```







