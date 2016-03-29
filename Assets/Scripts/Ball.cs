using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour {

    Rigidbody2D body;
    Transform myTranform;

    [SerializeField] float speed = 2f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        myTranform = transform;

        body.velocity = new Vector2(-speed, -speed);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Vector2 vel = Vector2.Reflect(body.velocity, -coll.contacts[0].normal);
        body.velocity = vel;
    }

}
