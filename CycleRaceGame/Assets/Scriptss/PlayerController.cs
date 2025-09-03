using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    [SerializeField] private int speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;

    private int lineToMove = 1;
    public float lineDistance = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMove < 2)
                lineToMove++;
        }

        if (SwipeController.swipeUp)
        {
            if (controller.isGrounded)
                Jump();
        }

            if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);

    }

    private void Jump()
    {
        dir.y = jumpForce;
    }

    public void ChangeSpeed(int delta)
    {
        speed += delta;

        if (speed < 1)
            speed = 1;
    }


    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    public int GetSpeed()
    {
        return speed;
    }

}
