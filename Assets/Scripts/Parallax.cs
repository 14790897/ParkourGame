using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed = 1f;
    public float resetX = 20f;
    public float startX = 20f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x <= -resetX)
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
    }
}
