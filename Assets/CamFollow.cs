using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public enum State
    { 
        Idle,
        Ready,
        Tracking,
    }

    private State state {
        set {
            switch (value)
            {
                case State.Idle:
                    break;
                case State.Ready:
                    break;
                case State.Tracking: //줌아웃
                    break;
            }
        }
    }

    private Transform target;

    public float smoothTime = 0.2f;
    private Vector3 lastMovingVelocity;
    private Vector3 targetPosition;

    private Camera cam;
    private float targtetZoomSize = 5f;

    private const float roundReadyZoomSize = 14.5f;
    private const float readyShotZoomSize = 5f;
    private const float trackingZoomSize = 10f;

    private float lastZoomSpeed;
    private void Awake()
    {
        state = State.Idle;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
