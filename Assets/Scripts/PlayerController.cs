using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float dashSpeed = 10f;
    private float horizontalInput;
    private float verticalInput;
    private float xBound = 18f;
    private float zBound = 9.5f;
    private float dashCooldown = 2f;
    private bool canDash = true;

    private SpawnManager spawnManager;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerMovement();
    }

    //Moves the player
    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        //playerRb.AddForce(Vector3.right * speed * horizontalInput);
        //playerRb.AddForce(Vector3.forward * speed * verticalInput);

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            Dash();
        }
    }

    void ConstrainPlayerMovement()
    {
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }

        if(transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
        else if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            collision.gameObject.GetComponent<MoveDown>().speed = 0;
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            spawnManager.isGameOver = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cotton"))
        {
            Debug.Log("Collided with cotton");
        }
        else if (other.gameObject.CompareTag("Powerup"))
        {
            Debug.Log("Collided with Powerup");
        }
        Destroy(other.gameObject);
    }


    void Dash()
    {
        Vector3 dashDir = new Vector3(horizontalInput, 0, verticalInput);
        playerRb.AddForce(dashDir.normalized * dashSpeed, ForceMode.Impulse);
        StartCoroutine(DashCooldownCoroutine());
    }

    IEnumerator DashCooldownCoroutine()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
