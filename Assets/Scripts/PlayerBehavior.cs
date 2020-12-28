using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public bool demoKinematicMovement = false;

    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    private CapsuleCollider _col;
    private GameBehavior _gameManager;

    private bool doJump = false;
    private bool doShoot = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        if (demoKinematicMovement)
        {
            // NOTE: with default rigidbody settings, player will sometimes spin in place
            // to fix, increase angular drag on rigidbody from 0.05 to, like, 100
            MoveKinematically();
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            doJump = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            doShoot = true;
        }

    }

    private void FixedUpdate()
    {
        if (demoKinematicMovement)
        {
            return;
        }

        // NOTE: doing input checking in FixedUpdate can result in missed input
        // instead do the input check in Update and set a flag, then check and reset the flag here
        // alternatively, check Input.GetKey rather than GetKeyDown, but user will still experience input lag
        // and you'll be splitting up your input checking into two places, and you STILL might miss input if user releases quickly
        // Author makes acknowledges this on p.221
        //if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        if (doJump)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            doJump = false;    
        }

        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);

        //if (Input.GetMouseButtonDown(0))
        if (doShoot)
        {
            // NOTE: original code adds a world space offset of 1 unit in the x direction, so as you turn, origin of bullet moves wrt player
            //GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;
            GameObject newBullet = Instantiate(bullet, this.transform.position + this.transform.right, this.transform.rotation) as GameObject;
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.forward * bulletSpeed;
            doShoot = false;
        }
    }

    void MoveKinematically()
    {
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.HP -= 1;
        }
    }
}
