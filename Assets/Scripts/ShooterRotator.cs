using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotator : MonoBehaviour
{
    private enum RotateState
    { 
        Idle,
        Vertical,
        Horizontal,
        Ready,
    }

    private RotateState state = RotateState.Idle;
    public float vertialRotateSpeed = 360f;
    public float horizontalRotateSpeed = 360f;

    public BallShooter ballShooter;

    // Start is called before the first frame update
    void Start()
    {
        ballShooter.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case RotateState.Idle:
                if (Input.GetButtonDown("Fire1"))
                {
                    state = RotateState.Horizontal;
                }
                break;
            case RotateState.Horizontal:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(0, horizontalRotateSpeed * Time.deltaTime, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Vertical;
                }
                break;
            case RotateState.Vertical:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(-vertialRotateSpeed * Time.deltaTime, 0, 0));
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Ready;
                    ballShooter.enabled = true;
                }
                break;
            case RotateState.Ready:
                break;
        }
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity; 
        state = RotateState.Idle;
        ballShooter.enabled = false;
    }
}
