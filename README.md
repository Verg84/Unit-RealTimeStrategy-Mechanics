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


