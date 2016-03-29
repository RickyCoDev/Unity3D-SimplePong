using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

    [SerializeField] string AxisButtons;
    [SerializeField] float speed = 7f;
    [SerializeField] LayerMask BorderMask;

    Rigidbody2D body;
    Transform myTransform;

    bool CanGoUp = true;
    bool CanGoDown = true;

    bool GetInput = true;

    const string TopBorderTag = "BTop";
    const string DownBorderTag = "BDown";

    float MaxY = 4.3f;

    enum Direction { up=1, down=-1 };

    Direction dir = Direction.down;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        myTransform = transform;
        References.gameManager.Ev_Win += ToggleInput;
    }

    void OnDisable()
    {
        References.gameManager.Ev_Win -= ToggleInput;
    }

    void InputHandler()
    {
        float y = Input.GetAxis(AxisButtons) * speed;
        //calculate direction up/down
        if (y != 0) // if is not moving don't changfe direction
            dir = (y > 0) ? Direction.up : Direction.down;

        CheckForMapEnd();
        ApplyMove(y);
    }

    //applies the motion to the body
    void ApplyMove(float sp)
    {
        if( (!CanGoUp && dir == Direction.down) || (!CanGoDown && dir == Direction.up) || (CanGoDown && CanGoUp) )
        body.velocity = new Vector2(0, sp);
    }

    // prevent strange positions
    void ValidatePos()
    {
        if (Mathf.Abs(myTransform.position.y) > MaxY)
        {
            Vector2 pos = myTransform.position;
            pos.y = (int)dir * MaxY;
            myTransform.position = pos;
        }
    }

    //checl the player position
    void CheckForMapEnd()
    {
        RaycastHit2D hit = Physics2D.Raycast(myTransform.position, Vector2.up * (int)dir , 0.7f, BorderMask);
        if (hit)
        {
            if (hit.transform.tag == TopBorderTag)
                CanGoUp = false;
            if (hit.transform.tag == DownBorderTag)
                CanGoDown = false;

            body.velocity = Vector2.zero;
            ValidatePos();
        }
        else
        {
            CanGoDown = true;
            CanGoUp = true;
        }
        Debug.DrawRay(myTransform.position, Vector2.up * (int)dir, Color.yellow, 1);
    }

	// Update is called once per frame
	void Update ()
    {
        if(GetInput)
        InputHandler();
	}

    void ToggleInput(string s)
    {
        GetInput = false;
    }
}
