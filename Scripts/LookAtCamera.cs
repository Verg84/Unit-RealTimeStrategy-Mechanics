using UnityEngine;

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
