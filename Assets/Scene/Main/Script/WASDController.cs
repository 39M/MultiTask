using UnityEngine;

public class WASDController : BaseController
{
    public Rigidbody2D rb;
    public float speed;
    //float scale = 0.25f;
    bool touching = false;
    Touch currentTouch;
    int currentTouchID;
    Vector2 touchBeginPosition;
    Vector2 moveDirection;
    //bool touchMoveUp = false, touchMoveDown = false, touchMoveLeft = false, touchMoveRight = false;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        if (!touching)
        {
            if (TouchAnyWhere(TouchPhase.Began))
            {
                foreach (var touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        currentTouchID = touch.fingerId;
                        touchBeginPosition = touch.position;
                        break;
                    }
                }
                touching = true;
            }
        }

        if (touching)
        {
            currentTouch = Input.GetTouch(currentTouchID);
            if (currentTouch.phase == TouchPhase.Ended)
            {
                touching = false;
                //touchMoveUp = touchMoveDown = touchMoveLeft = touchMoveRight = false;
            }
            else
            {
                moveDirection = currentTouch.position - touchBeginPosition;
                moveDirection.x = Mathf.Clamp(moveDirection.x, -195, 195) / 13;
                moveDirection.y = Mathf.Clamp(moveDirection.y, -195, 195) / 13;

                //touchMoveUp = currentTouch.position.y > touchBeginPosition.y;
                //touchMoveDown = currentTouch.position.y < touchBeginPosition.y;
                //touchMoveLeft = currentTouch.position.x < touchBeginPosition.x;
                //touchMoveRight = currentTouch.position.x > touchBeginPosition.x;
            }
        }
    }

    //void OnGUI()
    //{
    //    GUIStyle s = new GUIStyle();
    //    s.fontSize = 32;
    //    GUILayout.Label(currentTouch.position.ToString(), s);
    //    GUILayout.Label(touchBeginPosition.ToString(), s);
    //}

    public virtual void FixedUpdate()
    {
        if (gameController && gameController.gameover)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        //Vector3 currentPos = transform.position;
        //speed = 5 * Time.deltaTime;
        speed = 15f;

        if (touching)
        {
            rb.AddForce(moveDirection);
        }

        if (Input.GetKey(keyUp))
        {
            //if (currentPos.y + speed + scale < gameController.endY)
            //transform.Translate(0, speed, 0);
            rb.AddForce(new Vector2(0, speed));
        }

        if (Input.GetKey(keyLeft))
        {
            //if (currentPos.x - speed - scale > gameController.startX)
            //transform.Translate(-speed, 0, 0);
            rb.AddForce(new Vector2(-speed, 0));
        }

        if (Input.GetKey(keyDown))
        {
            //if (currentPos.y - speed - scale > gameController.startY)
            //transform.Translate(0, -speed, 0);
            rb.AddForce(new Vector2(0, -speed));
        }

        if (Input.GetKey(keyRight))
        {
            //if (currentPos.x + speed + scale < gameController.endX)
            //transform.Translate(speed, 0, 0);
            rb.AddForce(new Vector2(speed, 0));
        }
    }
}
