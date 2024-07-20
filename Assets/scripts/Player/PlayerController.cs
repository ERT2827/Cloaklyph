using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10;

    Vector2 move;

    GameObject playerModel;

    Vector3 lastLook = new Vector3(0, 0, 1);

    [SerializeField] private Rigidbody arbees;

    [Header("Dodge")]
    [SerializeField] private float dodgeDuration;
    [SerializeField] private float dodgeDistance;    bool dodging = false;

    Vector3 dodgeStart;
    Vector3 dodgeEnd;
    Vector3 dodgeDifference;

    [Header("Technical nessecities")]

    [SerializeField] private LayerMask dodgeCollideLayers;
    
    // Start is called before the first frame update
    void Start()
    {
        playerModel = transform.GetChild(0).gameObject;
        arbees = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        move = edgeGuard();
        
        

        Debug.Log("M " + move);

        // Debug.Log(move);

        arbees.velocity = new Vector3(0, arbees.velocity.y, 0);

        //Dodging code

        if(!dodging && Input.GetButton("Fire1") && move.magnitude > 0){
            // StartCoroutine(dodgeFunct());
            Debug.Log("bleepus");
        }
    }

    private void FixedUpdate() {
        if(!dodging)
        {
            movePlayer();
        }

        if(dodging){
            // Debug.Log(dodgeDifference + " " + Time.deltaTime + " " + dodgeDuration);
            // Debug.Log(dodgeDifference * Time.deltaTime * dodgeDuration);

            transform.position += dodgeDifference * Time.deltaTime * dodgeDuration;
        }
    }

    void movePlayer(){
        Vector3 movement = new Vector3(-move.y, 0, move.x);

        if(movement.magnitude > 0){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            lastLook = movement;
        }else{
            transform.rotation = Quaternion.LookRotation(lastLook);
        }


        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision other) {
        
        if(other.gameObject.layer == 7){
            dodging = false;
            Debug.Log("Hit " + other.gameObject.layer);
        }
    }

    Vector2 edgeGuard(){

        Vector2 M = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // Stop walking
        var dir = transform.TransformDirection (Vector3.down);
        
        // Up
        if(!Physics.Raycast (transform.position - new Vector3(0f, 0f, 1f), dir, 1) && M.x < 0){
            Debug.Log("Up");
            M.x = -0.01f;
        // Down
        }else if(!Physics.Raycast (transform.position - new Vector3(0f, 0f, -1f), dir, 1) && M.x > 0){
            Debug.Log("Down");
            M.x = 0.01f;
        // Left
        }
        
        if(!Physics.Raycast (transform.position - new Vector3(-1f, 0f, 0f), dir, 1) && M.y < 0){
            Debug.Log("Left");
            M.y = -0.01f;
        // Right
        }else if(!Physics.Raycast (transform.position - new Vector3(1f, 0f, 0f), dir, 1) && M.y > 0){
            Debug.Log("Right");
            M.y = 0.01f;
        }

        return(M);
    }


    IEnumerator dodgeFunct(){
        float xDif = -Input.GetAxisRaw("Vertical") * dodgeDistance;
        float yDif = Input.GetAxisRaw("Horizontal") * dodgeDistance;
        

        dodgeStart = new Vector3(transform.position.x, 0, transform.position.y);
        dodgeEnd = new Vector3(transform.position.x + xDif, 2, transform.position.z + yDif);
        dodgeDifference = dodgeEnd - dodgeStart;

        Debug.Log("Axes are " + Input.GetAxisRaw("Horizontal") + " " + Input.GetAxisRaw("Vertical"));

        Debug.Log(dodgeStart + " " + dodgeEnd + " " + dodgeDifference);

        dodging = true;

        yield return new WaitForSeconds(dodgeDuration);

        dodging = false;
    }

    // IEnumerator dodgeCoolDown(){
    //     dodgeReady = 
    // }


    // old dodge
    // dodging = true;
    //     dodgeDir = new Vector3(-move.y, 0, move.x);

    //     yield return new WaitForSeconds(dodgeDuration);

    //     dodging = false;
}

