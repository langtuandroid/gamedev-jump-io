using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class TrajectoryManager : MonoBehaviour
{
    public static TrajectoryManager Instance;

    public float carMoveSpeed;
    public GameObject objectToMove;
    public Transform startPoint, targetPosition;
    public bool done;
    public float trajectoryMaxHeight;
    public List<Vector3> points = new();
    public LineRenderer lineRenderer;
    public Rigidbody rb;
    public int counterForGravity;
    public bool inAction;

    private Vector3 _startPosition, _endPosition;
    private bool _gotPoints;
    private int _count;
    private float _timer;
    private float _height;
    [Inject] private GameManager gameManager;
    [Inject] private AudioManager audioManager;
    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    private void Start() => lineRenderer.positionCount = points.Count;

    void Update()
    {
        if (gameManager.trajectoryOn)
        {
            if (Input.GetMouseButton(0) && gameManager.playerRunControl)
            {
                _timer += Time.deltaTime;

                if (!lineRenderer.enabled)
                {
                    lineRenderer.enabled = true;
                }
                Trajectory();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_timer > 0.1f)
                {
                    inAction = true;
                    done = true;
                    lineRenderer.enabled = false;
                    rb.useGravity = false;
                    gameManager.playerRunControl = false;
                    gameManager.CharacterJump();
                    audioManager.PlayMusic(AudioType.Jump);
                    Instantiate(gameManager.jumpEffect, rb.position, gameManager.jumpEffect.transform.rotation);
                }
                else
                {
                    lineRenderer.enabled = false;
                }
                _timer = 0;
                if (counterForGravity > 0)
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
            _startPosition = startPoint.position;
            _endPosition = targetPosition.position;
            _height = trajectoryMaxHeight;
            for (int i = 0; i < points.Count; i++)
            {
                lineRenderer.SetPosition(i, points[i]);
            }
        }

        _gotPoints = true;

    }
    void MoveTowards()
    {
        if (!_gotPoints) return;

        float dis = Vector3.Distance(objectToMove.transform.position, points[_count]);

        if (dis <= 1)
        {
            if (_count < points.Count)
            {
                _count++;
            }

            if (_count == points.Count)
            {
                PointsCounterReset();
                gameManager.playerRunControl = true;
                return;
            }

            if (_count == (points.Count - 6))
            {
                StartCoroutine(PlayCharacterLandAnimation());
            }

            if (_count == (points.Count - 4))
            {
                rb.useGravity = true;
            }
        }
        if (_count <= points.Count - 1)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, points[_count], carMoveSpeed * Time.deltaTime);
        }
    }

    void OnDrawLine()
    {
        if (inAction) return;

        Debug.DrawLine(_startPosition, _endPosition);
        float count = 20;
        points.Clear();
        for (int i = 0; i < count + 1; i++)
        {
            Vector3 point = SampleParabola(_startPosition, _endPosition, _height, i / count);
            points.Add(point);
        }
    }

    private Vector3 SampleParabola(Vector3 start, Vector3 end, float height, float t)
    {
        Vector3 travelDirection = end - start;
        Vector3 result = start + t * travelDirection;
        result.y = Mathf.Sin(t * Mathf.PI) * height;
        return result;
    }

    private IEnumerator PlayCharacterLandAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        gameManager.CharacterLand();
    }

    public void PointsCounterReset()
    {
        done = false;
        _count = 0;
        inAction = false;
        rb.useGravity = true;
    }
}
