# Camera Controller

  ## CameraController.cs

  ### **private void Move()**
  Get the keyboard input:
  ```
float x = Input.GetAxis("Horizontal");
float z = Input.GetAxis("Vertical");

  ```
Compute a direction vector:
```
Vector3 direction = Vector3.forward * z + Vector3.right * x
```
Finally update the game object's position:
```
transform.position += direction * moveSpeed * Time.deltaTime;
```

### **private void Zoom()**
Detect the scroll input from the mouse
```
float scrollInput = Input.GetAxis("Mouse ScrollWheel");
```
Compute the distance between the camera and its parent empty game object
```
float dist = Vector3.Distance(transform.position, cam.transform.position);
```
Compute the zooming based on max min zoom values
```
if (dist < minZoomDist && scrollInput > 0.0f)
  return;
if (dist > maxZoomDistance && scrollInput < 0.0f)
  return;
cam.transform.position += cam.transform.forward * scrollInput * zoomSpeed;
```

### **public void FocusOnPoint(Vector3 position)**
Moves the camera to the specified position
```
 transform.position = position;
```

### **Update() method**
```
private void Update()
    {
        Move();
        Zoom();
    }
```
