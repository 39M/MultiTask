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
            if (currentTouch.phase == TouchPhase.Ended || currentTouch.position.x <= baseGame.screenStartX ||
                currentTouch.position.x >= baseGame.screenEndX)
            {
                touching = false;
            }
            else
            {
                moveDirection = currentTouch.position - touchBeginPosition;
                moveDirection.x = Mathf.Clamp(moveDirection.x, -195, 195) / 13;
                moveDirection.y = Mathf.Clamp(moveDirection.y, -195, 195) / 13;
            }
        }
    }

    public virtual void FixedUpdate()
    {
        if (baseGame && baseGame.gameover)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        //Vector3 currentPos = transform.position;
        //speed = 5 * Time.deltaTime;
        speed = 15f;

        if (touching)
        {
            //rb.AddForce(moveDirection);
            var moveTo = (Camera.main.ScreenToWorldPoint(currentTouch.position) - transform.position);
            moveTo.x = Mathf.Clamp(moveTo.x, -2, 2) * 7.5f;
            moveTo.y = Mathf.Clamp(moveTo.y, -2, 2) * 7.5f;
            rb.AddForce(moveTo);
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
