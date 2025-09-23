# Resource Game Object

## **LookAtCamera.cs**
Game object always facing the camera. This script can be made active also during editing using _[ExecuteInEditMode]_ above.
```
[ExecuteInEditMode]
public class LookAtCamera : MonoBehaviour
{
    Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        transform.eulerAngles = cam.transform.eulerAngles;
    }
}
```

## **ResourceSource.cs**
Declare enumerator to hold different resource types:
```
public enum ResourceType { Food }
```
Inside the class we declare the variables
+ _public ResourceType resourceType_
+ _public int quantity_ , the maximum amount of this resource to be gathered

Setup Events:
```
using UnityEngine.Events;
...
public UnityEvent onQuantiryChange
```

### public void GatherResource(int amount,Player player)
Controls the amount to gather by the player, based on the resources left
Update the remaining resources:
```
quantity -= amount;
int amountToGive = amount;
```
If there are no more resources to gather
```
if (quantity < 0)
  amountToGive = amount + quantity;  
```
Destroy the resource object
```
if (quantity <= 0)
  Destroy(gameObject);
```
Call the event whenever each time an amount of the resource is gathered
```
if (onQuantityChange != null)
  onQuantityChange.Invoke();
```

## **ResourceSourceUI.cs**
At this point this class controls the UI Canvas Images component of the Resource object.
To access properties of Canvas Text object:
```
using TMPro;
```
Include the variables:
+ _public GameObject resourcePanel_ ... reference to the Canvas Resource panel.
+ _public TextMeshProUGUI quantiryText_ ... holds the Text GUI object.
+ _public ResourceSource resourceSource_ ... to access the values of the class for displaying

### private void OnMouseEnter()
Activate the Resource Panel when hovering over with the mouse
```
resourcePanel.SetActive(true);
```
### private void OnMouseExit()
Deactivate the Resource Panel
```
resourcePanel.SetActive(false);
```
### public void OnResourceQuantityChange()
This is the method to call the onResourceChangeEvent
```
qunatityText.text = resourceSource.quantity.ToString();
```
