using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 6f;
    public float leftBound = -12f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
