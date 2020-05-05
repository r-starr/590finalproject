using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{
    GameObject model;

    Animator animator;

    CharacterController characterController;

    public float speed = 3.0f;
    public float rotationSpeed = 180;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;

    private Vector3 rotation;

    void Awake() {
        TextAsset shapeFile = new TextAsset(PlayerPrefs.GetString("shapeFile"));
        print(shapeFile.text);
        GameObject.Find("f_avg").GetComponent<SMPLBlendshapes>().shapeParmsJSON = shapeFile;
    }

    // Start is called before the first frame update
    void Start()
    {
        model = this.gameObject;
        animator = model.GetComponent<Animator>();
        characterController = model.GetComponent<CharacterController>();
        animator.SetBool("isWalking", false);
    }

    // Update is called once per frame
    void Update()
    {

        moveDirection = new Vector3(0, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime);
        moveDirection = this.transform.TransformDirection(moveDirection);

        if(moveDirection == Vector3.zero) {
            rotation = Vector3.zero;
        }
        else {
            rotation = new Vector3(0, Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime, 0);
        }

        if(moveDirection == Vector3.zero && animator.GetBool("isWalking")) {
                animator.SetBool("isWalking", false);
            }
        if(moveDirection != Vector3.zero && !animator.GetBool("isWalking")) {
                animator.SetBool("isWalking", true);
        }

        characterController.Move(moveDirection * speed);
        model.transform.Rotate(rotation);

    }
}
