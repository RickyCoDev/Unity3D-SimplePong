using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour {

    Rigidbody2D body;
    Transform myTranform;

    [SerializeField] float speed = 2f;

    float startDir = 1;
    GameManager gm;
    void Start()
    {
        gm = References.gameManager;
        body = GetComponent<Rigidbody2D>();
        myTranform = transform;
        body.velocity = new Vector2(startDir* speed, startDir*speed);

        gm.Ev_BlueScored += ResetBallPos;
        gm.Ev_RedScored += ResetBallPos;
        gm.Ev_BallReset += RestartBall;
        gm.Ev_Win += Kill;
    }

    void OnDisable()
    {
        gm.Ev_BlueScored -= ResetBallPos;
        gm.Ev_RedScored -= ResetBallPos;
        gm.Ev_BallReset -= RestartBall;
        gm.Ev_Win -= Kill;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Vector2 vel = Vector2.Reflect(body.velocity, -coll.contacts[0].normal);
        body.velocity = vel;
    }

    void RestartBall()
    {
        myTranform.position = new Vector3(0, 0, 0);
        body.velocity = new Vector2(startDir * speed, startDir * speed);
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

    void Kill(string s)
    {
        Destroy(this.gameObject);
    }
}
