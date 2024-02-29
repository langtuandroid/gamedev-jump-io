using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryScript : MonoBehaviour
{
    public static TrajectoryScript Instance;

    public GameObject objectToMove;  //you can use this also
    public Transform startPoint, targetPosition;
    float height;
    Vector3 startPosition, endPosition;
    public Vector3 before_clamp;
    public bool done;
    public float trajectoryMaxHeight;
    private bool gotPoints;
    public List<Vector3> points = new List<Vector3>(); // change the points value in the inspector to 21 points. 
    public LineRenderer lineRenderer; // dont forget to assign a line renderer
    public float distance = 314.3f;
    public bool afterClick = true, inAction;
    int count = 0;
    public float carMoveSpeed; //move speed of the object
    float timer = 0;
    public Rigidbody rb;
    public float jumpStrength;


    public int counterForGravity;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        lineRenderer.positionCount = points.Count;
    }
    
    void Update()
    {
        if (GameManager.instance.trajectoryOn)
        {
            if (Input.GetMouseButton(0) && GameManager.instance.playerRunControl)
            {
                timer += Time.deltaTime;

                if (!lineRenderer.enabled)
                {
                    lineRenderer.enabled = true;
                }
                Trajectory();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (timer > 0.1f) //Just to make sure all points are registered. Takes about 10ms
                {
                    inAction = true;
                    done = true;
                    lineRenderer.enabled = false;
                    rb.useGravity = false;
                    GameManager.instance.playerRunControl = false;
                    GameManager.instance.CharacterJump();
                    GameManager.instance.JumpSFX();
                    Instantiate(GameManager.instance.jumpEffect, rb.position, GameManager.instance.jumpEffect.transform.rotation);
                }
                else
                {
                    lineRenderer.enabled = false;
                }
                timer = 0;
                if(counterForGravity > 0)
                {
                    rb.useGravity = false;
                }
                counterForGravity++;
            }

            if (points.Count > 0 && done)
            {
                MoveTowards();
            }
        }
    }
    void Trajectory()
    {
        OnDrawLine();
        
        if (startPoint && targetPosition)
        {
            startPosition = startPoint.position;
            endPosition = targetPosition.position;
            height = trajectoryMaxHeight; //Multiply your input sys here
            for (int i = 0; i < points.Count; i++)
            {
                lineRenderer.SetPosition(i, points[i]);
            }
        }

        gotPoints = true;
        
    }
    void MoveTowards()
    {

        if (!gotPoints)
            return;
        float dis = Vector3.Distance(objectToMove.transform.position, points[count]);

        if (dis <= 1)
        {
            if (count < points.Count)
            {
                count++;
            }

            if (count == points.Count)
            {
                //done = false;
                //count = 0;
                //inAction = false;
                PointsCounterReset();
                GameManager.instance.playerRunControl = true;
                return;
            }

            if(count == (points.Count - 6))
            {
                StartCoroutine(PlayCharacterLandAnimation());
            }

            if (count == (points.Count - 4))
            {
                rb.useGravity = true;
            }
        }
        if (count <= points.Count - 1)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, points[count], carMoveSpeed * Time.deltaTime);            
        }
    }

    void OnDrawLine()
    {
        if (inAction)
            return;
        Debug.DrawLine(startPosition, endPosition);
        float count = 20;
        points.Clear();
        Vector3 lastPosition = startPosition;
        for (int i = 0; i < count + 1; i++)
        {
            Vector3 point = SampleParabola(startPosition, endPosition, height, i / count);
            points.Add(point);
            lastPosition = point;
        }
    }

    public Vector3 SampleParabola(Vector3 start, Vector3 end, float height, float t)
    {
        Vector3 travelDirection = end - start;
        Vector3 result = start + t * travelDirection;
        result.y = Mathf.Sin(t * Mathf.PI) * height;
        return result;
    }

    IEnumerator PlayCharacterLandAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        GameManager.instance.CharacterLand();
    }

    public void PointsCounterReset()
    {
        done = false;
        count = 0;
        inAction = false; 
        rb.useGravity = true;
    }
}
