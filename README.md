# Moving Unit(s) to Resources

## **Unit.cs**
Create a method for gathering resources with parameters the _ResourceSource_ and a _Vector3_ resource location.
### public void GatherResource(ResourceSource resource,Vector3 resourcePosition)

For now inside the function, just move the Unit to the resource location, calling the _MovetoPosition()_ method.
```
MoveToPosition(resourcePosition);
```

## **UnitMovement.cs**
Include two static methods, one to compute an array of destinations and another for computing  
destination to a resource for a single Unit.
### public static Vector3[] UnitGroupResourceDestination(Vector3 resourcePosition,int numUnits)
### public static Vector3 UnitResourceDestination(Vector3 resourcePosition)

## **UnitCommands.cs**
Add a method to move Units to resource and call the Unit's gather resource function
### void UnitsGatherResource(Unit[] units,ResourceSource resourceSource)
First check how many Units are selected and call the relevant functions
```
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
```
## void CreateSelectionMarker(Vector3 position,bool large)
This is the updated edition that generates the location indicator prefab, scaling the object in case a "Resource" tag is detected.
```
GameObject marker=Instantiate(selectionMarkerPrefab,position + new Vector3(0, 0.01f, 0), Quaternion.identity);
if (large)
  marker.transform.localScale = Vector3.one * 3;
```

## The **void Update()** method
On listening for the right button input and casting a ray, add code for detecting Resource objects:
```
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
```
