using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float zoomSpeed;

    public float minZoomDist;
    public float maxZoomDistance;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Move();
        Zoom();
    }

    private void Move()
    {
        // get keyborad input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //compute direction vector
        Vector3 direction = Vector3.forward * z + Vector3.right * x;

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void Zoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float dist = Vector3.Distance(transform.position, cam.transform.position);

        if (dist < minZoomDist && scrollInput > 0.0f)
            return;
        if (dist > maxZoomDistance && scrollInput < 0.0f)
            return;
        cam.transform.position += cam.transform.forward * scrollInput * zoomSpeed;
    }

    public void FocusOnPoisiton(Vector3 position)
    {
        transform.position = position;
    }
}
