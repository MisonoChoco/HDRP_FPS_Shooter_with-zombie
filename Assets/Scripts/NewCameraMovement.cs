using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform orientation;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
    }
}