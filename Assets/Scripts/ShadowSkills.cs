using UnityEngine;
using UnityEngine.Serialization;

public class ShadowSkillsObject : MonoBehaviour
{
    private enum ShootState
    {
        Idle,
        Shooting,
        EndShooting
    };
    public GameObject orb;
    public GameObject _orbClone;
    public GameObject _holdObject;
    public Camera playerCamera;
    public GameObject player;
    private bool _isPickedUp;
    private GameObject _objectIwantToPickUp;
    private int _shootCount;
    private ShootState _shootState;
    public float _shootSpeed;
    private Vector3 _shootDirection;
    private Vector3 _orbOriginalLocalPosition;
    private float _returnProgress;
    private Transform _parentCache;
    public float _maxDistance;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _isPickedUp = false;
        _shootCount = 3;
        _shootState = ShootState.Idle;
        _shootDirection = playerCamera.transform.forward;
        _returnProgress = 0f;
        _parentCache = orb.transform.parent;
        _objectIwantToPickUp = null;
    }
 
    void Update()
    {
        if (_shootState == ShootState.Idle)
        {
            orb.transform.RotateAround(player.transform.position, Vector3.up, 90 * Time.deltaTime);
        }
        
        if (Input.GetMouseButtonDown(0) && _shootState == ShootState.Idle && !_isPickedUp)
        {
            _shootState = ShootState.Shooting;
            _shootDirection = playerCamera.transform.forward;
            _returnProgress = 0f;
            orb.transform.parent = null;
        }
        
        if (Input.GetMouseButtonDown(0) && _shootState == ShootState.Idle && _isPickedUp)
        {
            _objectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false;
            _objectIwantToPickUp.transform.parent = null;
            _isPickedUp = false;
        }

        if (_shootState == ShootState.Shooting && !_isPickedUp)
        {
            orb.transform.position += _shootDirection * (Time.deltaTime * _shootSpeed);
            if (Vector3.Distance(orb.transform.position, player.transform.position) >= _maxDistance)
            {
                _shootState = ShootState.EndShooting;
                _returnProgress = 0f;
            }
        }
        
        if (_shootState == ShootState.EndShooting && _returnProgress < 1.0f)
        {
            _returnProgress += Time.deltaTime;
            orb.transform.parent = _parentCache;
            orb.transform.position =
                Vector3.Lerp(orb.transform.position, _orbClone.transform.position, _returnProgress);
            if (_returnProgress >= 1.0f)
            {
                _shootState = ShootState.Idle;
            }
        }

        if (_isPickedUp && _objectIwantToPickUp != null)
        {
            _objectIwantToPickUp.transform.Rotate(0, 6.0f * 10.0f * Time.deltaTime, 0);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Movable") && !_isPickedUp && _shootState != ShootState.Idle)
        {
            _shootState = ShootState.EndShooting;
            _returnProgress = 0f;
            _objectIwantToPickUp = other.gameObject;
            _objectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;
            _objectIwantToPickUp.transform.position = orb.transform.position;
            _objectIwantToPickUp.transform.parent = orb.transform;
            _isPickedUp = true;
        }
        
        if (_isPickedUp)
        {
            _objectIwantToPickUp.transform.parent = _holdObject.transform;
            _objectIwantToPickUp.transform.position = _holdObject.transform.position;
        }
    }
}