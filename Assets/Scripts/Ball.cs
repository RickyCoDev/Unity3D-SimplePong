using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour {

    Rigidbody2D body;
    Transform myTranform;

    [SerializeField] float speed = 2f;

    float startDir = 1;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        myTranform = transform;
        body.velocity = new Vector2(startDir* speed, startDir*speed);
        References.gameManager.Ev_BlueScored += ResetBallPos;
        References.gameManager.Ev_RedScored += ResetBallPos;
    }

    void OnDisable()
    {
        References.gameManager.Ev_BlueScored -= ResetBallPos;
        References.gameManager.Ev_RedScored -= ResetBallPos;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Vector2 vel = Vector2.Reflect(body.velocity, -coll.contacts[0].normal);
        body.velocity = vel;
    }


    public void ResetBallPos()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        myTranform.position = new Vector3(0, 0, 0);
    }
}
