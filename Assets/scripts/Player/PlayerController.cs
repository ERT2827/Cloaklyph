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

    public float dodgeCooldown;
    bool dodgeCoolingDown = false;

    [Header("Technical nessecities")]

    [SerializeField] private LayerMask dodgeCollideLayers;
    private PlayerHealth playerHealth;
    public bool controlsDisabled;
    
    // Start is called before the first frame update
    void Start()
    {
        playerModel = transform.GetChild(0).gameObject;
        arbees = gameObject.GetComponent<Rigidbody>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controlsDisabled){
            return;
        }
        
        move = edgeGuard();
        
        

        // Debug.Log("M " + move);

        // Debug.Log(move);

        arbees.velocity = new Vector3(0, arbees.velocity.y, 0);

        //Dodging code

        if(!dodging && Input.GetButton("Jump") && move.magnitude > 0 && !dodgeCoolingDown){
            StartCoroutine(dodgeFunct());
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
        Vector3 movement = new Vector3(move.x, 0, move.y);

        if(movement.magnitude > 0){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            lastLook = movement;
        }else{
            transform.rotation = Quaternion.LookRotation(lastLook);
        }


        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    Vector2 edgeGuard(){

        Vector2 M = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // Stop walking
        var dir = transform.TransformDirection (Vector3.down);
        
        // Up
        if(!Physics.Raycast (transform.position - new Vector3(0f, 0f, 1.5f), dir, 1) && M.x < 0){
            // Debug.Log("Up");
            M.x = -0.01f;
        // Down
        }else if(!Physics.Raycast (transform.position - new Vector3(0f, 0f, -1.5f), dir, 1) && M.x > 0){
            // Debug.Log("Down");
            M.x = 0.01f;
        // Left
        }
        
        if(!Physics.Raycast (transform.position - new Vector3(-1.5f, 0f, 0f), dir, 1) && M.y < 0){
            // Debug.Log("Left");
            M.y = -0.01f;
        // Right
        }else if(!Physics.Raycast (transform.position - new Vector3(1.5f, 0f, 0f), dir, 1) && M.y > 0){
            // Debug.Log("Right");
            M.y = 0.01f;
        }

        return(M);
    }


    IEnumerator dodgeFunct(){
        float xDif = Input.GetAxisRaw("Horizontal") * dodgeDistance;
        float yDif = Input.GetAxisRaw("Vertical") * dodgeDistance;
        

        dodgeStart = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        dodgeEnd = new Vector3((transform.position.x + xDif), transform.position.y, (transform.position.z + yDif));
        dodgeDifference = dodgeEnd - dodgeStart;

        // Debug.Log("Axes are " + Input.GetAxisRaw("Horizontal") + " " + Input.GetAxisRaw("Vertical"));

        // Debug.Log(dodgeStart + " " + dodgeEnd + " " + dodgeDifference);

        dodging = true;

        playerHealth.invincible = true;

        yield return new WaitForSeconds(dodgeDuration);

        dodging = false;

        playerHealth.invincible = false;

        StartCoroutine(DodgeCoolDown());
    }

    IEnumerator DodgeCoolDown(){
        dodgeCoolingDown = true;

        yield return new WaitForSeconds(dodgeCooldown);

        dodgeCoolingDown = false;
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

