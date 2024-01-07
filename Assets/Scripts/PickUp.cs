using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject myHands; //reference to your hands/the position where you want your object to go
    private bool _canPickUp; //a bool to see if you can or cant pick up the item
    private GameObject _objectIwantToPickUp; // the gameobject onwhich you collided with

    private bool _hasItem; // a bool to see if you have an item in your hand
    // Start is called before the first frame update
    void Start()
    {
        _canPickUp = false;    //setting both to false
        _hasItem = false;
    }
 
    // Update is called once per frame
    void Update()
    {
        if(_canPickUp) // if you enter thecollider of the objecct
        {
            if (Input.GetKeyDown(KeyCode.E))  // can be e or any key
            {
                _hasItem = true;
                _objectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
                _objectIwantToPickUp.transform.position = myHands.transform.position; // sets the position of the object to your hand position
                _objectIwantToPickUp.transform.parent = myHands.transform; //makes the object become a child of the parent so that it moves with the hands
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && _hasItem) // if you have an item and get the key to remove the object, again can be any key
        {
            _hasItem = false;
            _objectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again
         
            _objectIwantToPickUp.transform.parent = null; // make the object no be a child of the hands
        }
    }
    private void OnTriggerEnter(Collider other) // to see when the player enters the collider
    {
        if(other.gameObject.CompareTag("Movable")) //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            _canPickUp = true;  //set the pick up bool to true
            _objectIwantToPickUp = other.gameObject; //set the gameobject you collided with to one you can reference
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _canPickUp = false; //when you leave the collider set the canpickup bool to false
    }
}