using UnityEngine;

public class JIBlock : MonoBehaviour
{
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    [SerializeField] private bool blockZaxis;
    
    private int _counter;
    private Rigidbody _rb;

    private float _upForce;
    private float _frontForce;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _upForce = Random.Range(minValue, maxValue);
        _frontForce = Random.Range(minValue, maxValue);
    }

    private void OnCollisionEnter(Collision other)
    {
        if((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("OtherPlayer")) && _counter == 0)
        {
            if(!blockZaxis)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                _rb.AddForce(new Vector3(0, _upForce, _frontForce), ForceMode.Impulse);
                _counter++;
            }
            else
            {
                _rb.AddForce(new Vector3(0, _upForce, 0), ForceMode.Impulse);
                _counter++;
            }
        }
    }
}
