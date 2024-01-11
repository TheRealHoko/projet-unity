using UnityEngine;
using UnityEngine.Serialization;

public class ShadowSkillsObject : MonoBehaviour
{
    public GameObject orb;
    public Camera playerCamera;
    private bool _isPickedUp;
    private GameObject _objectIwantToPickUp;
    private int _shootCount;
    private bool _shootState;
    private float _shootSpeed;
    private Vector3 _shootDirection;
    private Vector3 _orbOriginalPosition;
    private float _returnProgress;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _isPickedUp = false;
        _shootCount = 3;
        _shootState = false;
        _shootSpeed = 10f;
        _shootDirection = playerCamera.transform.forward;
        _orbOriginalPosition = orb.transform.position;
        _returnProgress = 0f;
    }
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _shootState = true;
            _shootDirection = playerCamera.transform.forward;
            _returnProgress = 0f;
            orb.transform.parent = null;
            if (_isPickedUp)
            {
                _objectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false;
                _objectIwantToPickUp.transform.parent = null;
                _isPickedUp = false;
            }
        }

        if (orb.transform.parent == null)
        {
            if (_shootState && !_isPickedUp)
            {
                orb.transform.position += _shootDirection * (Time.deltaTime * _shootSpeed);
                if (Vector3.Distance(orb.transform.position, _orbOriginalPosition) >= 20f)
                {
                    _shootState = false;
                    _returnProgress = 0f;
                }
            }
            else
            {
                if (_returnProgress < 1.0f)
                {
                    _returnProgress += Time.deltaTime / _shootSpeed;
                    orb.transform.position = Vector3.Lerp(orb.transform.position, _orbOriginalPosition, _returnProgress);
                }
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Ontrigger called");
        if (other.gameObject.CompareTag("Movable") && !_isPickedUp)
        {
            _isPickedUp = true;
            _shootState = false;
            _returnProgress = 0f;
            _objectIwantToPickUp = other.gameObject;
            _objectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;
            _objectIwantToPickUp.transform.position = orb.transform.position;
            _objectIwantToPickUp.transform.parent = orb.transform;
        }
    }
}