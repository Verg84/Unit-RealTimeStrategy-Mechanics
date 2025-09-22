# BOX SELECTION
Use a canvas image as the box selection object, controlled by the **UnitMovement** class as a RectTransform reference.
## <ins>UniMovement.cs<ins>
```
public RectTransform boxSelector;
private Vector2 startPosition;
```
### void UpdateSelectionBox(Vector2 curPosition)
A method that computes the dimensions of the box as long as the left mouse button is pressed.  
Set  the box selector object active
```
if (!boxSelector.gameObject.activeInHierarchy)
  boxSelector.gameObject.SetActive(true);
```
Compute box dimensions based on starting position of the mouse and current position:
```
float width = curPosition.x - startPosition.x;
float height = curPosition.y - startPosition.y;
```
Update the box using **RectTransform.sizeDelta** and **RectTransform.anchoredPosition** properties
```
boxSelector.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
boxSelector.anchoredPosition = startPosition + new Vector2(width / 2, height / 2);
```

### void ReleaseSelectionBox()
This function called on releasing the left mouse button.
Deactivate the box and calculate max and min screen coordinates of the box
```
boxSelector.gameObject.SetActive(false);
Vector2 min = boxSelector.anchoredPosition - boxSelector.sizeDelta / 2;
Vector2 max = boxSelector.anchoredPosition + boxSelector.sizeDelta / 2;
```
For each unit object that belong to player find its screen coordinates
```
 Vector3 screenPos = cam.WorldToScreenPoint(unit.transform.position);
```
If the Unit's screen coordinates are covered by the box, add it to the selected units and toggle the Unit selection indicator
```
 foreach(Unit unit in player.playerUnits)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(unit.transform.position);
            if(screenPos.x>min.x && screenPos.x<max.x && screenPos.y>min.y && screenPos.y<max.y)
            {
                selectedUnits.Add(unit);
                unit.ToggleUnitSelectionVisual(true);
            }
        }
```
### Update() method ###
Detect mouse input using the methods:
+ **Input.GetMouseButtonDown(0)**
+ **Input.GetMouseButton(0)**
+ **Input.GetMouseButtonUp(0)**

```
private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;

            ToggleUnitVisuals(false);
            selectedUnits = new List<Unit>();

            SelectUnit(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }

        if(Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionBox();
        }
    }
```

