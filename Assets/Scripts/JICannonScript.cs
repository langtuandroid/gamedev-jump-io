using System.Collections.Generic;
using UnityEngine;

public class JICannonScript : MonoBehaviour
{
    [SerializeField] private GameObject TrajectoryPointPrefeb;
    [SerializeField] private GameObject BallPrefb;

    private GameObject _ball;
    private bool _isPressed, _isBallThrown;
    private float _power = 25;
    private int _numOfTrajectoryPoints = 30;
    private List<GameObject> _trajectoryPoints;
    
    void Start()
    {
        _trajectoryPoints = new List<GameObject>();
        _isPressed = _isBallThrown = false;
        for (int i = 0; i < _numOfTrajectoryPoints; i++)
        {
            GameObject dot = Instantiate(TrajectoryPointPrefeb);
            dot.GetComponent<Renderer>().enabled = false;
            _trajectoryPoints.Insert(i, dot);
        }
    }
    
    void Update()
    {
        if (_isBallThrown)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            _isPressed = true;
            if (!_ball)
                CreateBall();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isPressed = false;
            if (!_isBallThrown)
            {
                ThrowBall();
            }
        }
        if (_isPressed)
        {
            Vector3 vel = GetForceFrom(_ball.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
            SetTrajectoryPoints(transform.position, vel / _ball.GetComponent<Rigidbody>().mass);
        }
    }
    
    private void CreateBall()
    {
        _ball = Instantiate(BallPrefb);
        Vector3 pos = transform.position;
        pos.z = 1;
        _ball.transform.position = pos;
        _ball.SetActive(false);
    }
    
    private void ThrowBall()
    {
        _ball.SetActive(true);
        _ball.GetComponent<Rigidbody>().useGravity = true;
        _ball.GetComponent<Rigidbody>().AddForce(GetForceFrom(_ball.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)), ForceMode.Impulse);
        _isBallThrown = true;
    }
    
    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos) => (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * _power;

    void SetTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    {
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;

        fTime += 0.1f;
        for (int i = 0; i < _numOfTrajectoryPoints; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, 2);
            _trajectoryPoints[i].transform.position = pos;
            //trajectoryPoints[i].renderer.enabled = true;
            _trajectoryPoints[i].GetComponent<Renderer>().enabled = true;
            _trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            fTime += 0.1f;
        }
    }
}
