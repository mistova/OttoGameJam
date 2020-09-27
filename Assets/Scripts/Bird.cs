using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float birdSpeed;

    private Transform cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        float speedMult = UnityEngine.Random.Range(-1f, -0.5f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(birdSpeed * speedMult, UnityEngine.Random.Range(-0.05f, 0.05f));
    }

    void Update()
    {
        if (cam.position.x - transform.position.x > 10)
            Destroy(gameObject);
    }
}
