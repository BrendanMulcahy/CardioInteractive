using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float speed = 5;
    private Vector3 velocity;
    private bool hasEntered = false;

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
        transform.position = new Vector3(transform.position.x + velocity.x * Time.deltaTime,
                                         transform.position.y + velocity.y * Time.deltaTime,
                                         0f);
    }
}
