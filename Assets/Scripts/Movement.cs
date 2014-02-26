using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float speed = 5;
    public float accel = 5;
    private Vector3 velocity;
    private bool hasEntered = false;
    private Rect toDraw;

	// Use this for initialization
	void Start () {
        velocity = new Vector3();
        velocity.x = Random.Range(-1f, 1f);
        velocity.y = Random.Range(-1f, 1f);
        velocity = velocity.normalized * speed;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateMovement();
	}

    void OnGUI()
    {

    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        //Debug.Log("Collision with " + collisionInfo.gameObject.name);
        Vector3 collisionNormal = collisionInfo.contacts[0].normal;
        Vector3 parallelToWall = Vector3.Cross(collisionNormal, Vector3.forward);
        Vector3 perpComponent = collisionNormal * Vector3.Dot(velocity, collisionNormal) * -1;
        Vector3 paraComponent = parallelToWall.normalized * Vector3.Dot(velocity, parallelToWall);
        velocity = new Vector3(perpComponent.x + paraComponent.x, perpComponent.y + paraComponent.y, 0f);
        velocity = velocity.normalized * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger with " + other.gameObject.name);
        if (other.gameObject == SceneManager.Instance.mainArea && !hasEntered)
        {
            hasEntered = true;
        }
        else if (hasEntered && (other.gameObject == SceneManager.Instance.Gate1 ||
                                other.gameObject == SceneManager.Instance.Gate2 ||
                                other.gameObject == SceneManager.Instance.Gate3))
        {
            velocity = new Vector3(velocity.x, velocity.y * -1, 0);
        }
        else if (other.gameObject == SceneManager.Instance.destroyer)
        {
            Destroy(this.gameObject);
        }
    }

    private void UpdateMovement()
    {
        Vector3 acceleration = GetAcceleration();
        velocity += acceleration * accel * Time.deltaTime;

        transform.position = new Vector3(transform.position.x + velocity.x * Time.deltaTime,
                                         transform.position.y + velocity.y * Time.deltaTime,
                                         0f);
    }

    private Vector3 GetAcceleration()
    {
        float x = 0;
        float y = 0;
        float w = Screen.width / SceneManager.Instance.gridY;
        float h = Screen.height / SceneManager.Instance.gridX;
        int index = 0;

        for (int i = 0; i < SceneManager.Instance.gridX; i++)
        {
            for (int j = 0; j < SceneManager.Instance.gridY; j++)
            {
                Vector3 temp = Camera.main.WorldToScreenPoint(transform.position);
                if (new Rect(x, y, w, h).Contains(new Vector3(temp.x, Screen.height - temp.y, 0))) {
                    toDraw = new Rect(x, y, w, h);
                    return new Vector3(SceneManager.Instance.vectorfield[index].x - SceneManager.Instance.vectorStart[index].x, -(SceneManager.Instance.vectorfield[index].y - SceneManager.Instance.vectorStart[index].y), 0).normalized;
                }
                x += w;
                index++;
            }

            x = 0;
            y += h;
        }

        return new Vector3(0, 0, 0);
    }
}
