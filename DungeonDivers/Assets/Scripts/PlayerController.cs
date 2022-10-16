
using System.Runtime.InteropServices;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

public class PlayerController : MonoBehaviour
{   
    public float moveSpeed;
    private Vector2 moveInput;
    public Rigidbody2D theRB;
    public Transform gunArm;

    private Camera theCamera;

    public Animator theAnimator;

    // Start is called before the first frame update
    void Start()
    {   
        // Set Camera at start of game, because setting it every frame at 60FPS is too resource intensive 
        theCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
       moveInput.Normalize();


        theRB.velocity = moveInput * moveSpeed; 

        // get mouse position 
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = theCamera.WorldToScreenPoint(transform.localPosition);


        if(mousePosition.x < screenPoint.x) {
            transform.localScale = new Vector3(-1f,1f, 1f);
            gunArm.localScale = new Vector3(-1f,-1f,-1f);
        }else
        {
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }
        
        // Rotate the gun arm
        Vector2 offSet = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float theAngle = Mathf.Atan2(offSet.y, offSet.x) * Mathf.Rad2Deg;
        gunArm.rotation =Quaternion.Euler(0,0,theAngle);


        if(moveInput != Vector2.zero){
            // case sensitive use same spelling as in unity
            theAnimator.SetBool("isMoving",true);
        }else{
            theAnimator.SetBool("isMoving", false);
        }
    }
}
