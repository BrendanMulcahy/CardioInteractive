using UnityEngine;
using System.Collections;

public class FirstPersonCamera : MonoBehaviour
{
    public float normalSpeed;
    public float fastSpeed;
    public float rotateSpeed;
    private float speed;

    private float rotationX;
    private float rotationY;
    private Vector3 velocity;


    // Use this for initialization
    void Start()
    {
        speed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle cursor
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Screen.showCursor = !Screen.showCursor;
        }

        // Go faster when shift is held
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            speed = fastSpeed;
        }

        // Slow down when shift is released
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            speed = normalSpeed;
        }

        // Rotate camera
        if (Input.GetMouseButton(1))
        {
            rotationX += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, -90, 90);
        }

        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

        // Move in directions
        if (Input.GetKey(KeyCode.W))
        {
            velocity = transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity = -transform.right * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity = -transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity = transform.right * speed;
        }

        // Stop movement
        if (Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.D))
        {
            velocity = Vector3.zero;
        }

        // Update position based on velocity
        rigidbody.velocity = velocity;
        rigidbody.angularVelocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Entered trigger!");
        //transform.Translate(-currentTranslation);
        //velocity = Vector3.zero;
    }

    void OnCollisionExit(Collision other)
    {
        Debug.Log("Exited trigger!");
    }
}
