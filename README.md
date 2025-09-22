# UNIT SELECTION
## <ins>Player.cs</ins><br/>
At this point the Player class contains a public List of Units belonging to player and a boolean method for searching Units.
```
public List<Unit> playerUnits = new List<Unit>();

public bool IsPlayerUnit(Unit unit)
{
    return playerUnits.Contains(unit);
}
```

## <ins>Unit.cs</ins><br/>
Create reference variable for unit selection indicator:
```
public GameObject unitSelectionVisual;
```
Define method to set on/off the selection indicator GameObject
```
public void ToggleUnitSelectionVisual(bool selection)
{
    unitSelectionVisual.SetActive(selection);
}
```
## <ins>UnitSelection.cs</ins><br/>
### Variables
+ **```private List<Unit> selectedUnits = new List<Unit>()```**
    Contains all the selected units that belong to player.
  
 + **```private Player player```**
   Referecnce to Player class that contains public List with all Units belonging to player.

+ **```public LayerMask unitLayerMask```**
  Layer mask used in raycasting, in order to detect specific game objects.

+ **```private Camera cam```**
  The main camera. At most used to detect the screen coordinates of mouse and generate a ray.

### Methods
+ **``` void ToggleUnitVisuals(bool selection)```**  
  Called to (de)activate the Unit selection indicator. Loop over all selected units and calls the Unit's relative method.
  ```
  foreach(Unit unit in selectedUnits)
  {
    unit.ToggleUnitSelectionVisual(selection);
  }
  ```
+ **``` void SelectUnit(Vector2 screenPos)```**
  Generate a ray given 2D screen coordinates and cast it against a layer mask.
  ```
  Ray ray = cam.ScreenPointToRay(screenPos);
  RaycastHit hit;
  if (Physics.Raycast(ray,out hit,200,unitLayerMask))
  ```
  Create a reference to the found Unit game object's class.
  ```
  Unit unit = hit.collider.GetComponent<Unit>();
  ```
  Check if Unit belong to player. If that's true add it to the List of selected objects and toggle the unit selection indicator.
  ```
  if(player.IsPlayerUnit(unit))
  {
    selectedUnits.Add(unit);
    unit.ToggleUnitSelectionVisual(true);
  }
  ```

  ### The Update() method
  Here we check for input from mouse , read the mouse position in screen coordinates
  and call the function to generate the ray.
  ```
  private void Update()
  {
    if(Input.GetMouseButtonDown(0))
    {
        ToggleUnitVisuals(false);
        selectedUnits = new List<Unit>();

        SelectUnit(Input.mousePosition);
    }
  }
  ``` 
