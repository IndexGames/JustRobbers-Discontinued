using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
public class PlayerController : MonoBehaviour
{
     public Transform cameraTransform;
     public Transform character;
     public Animator characterAnimator;

     public float normalMovementSpeed = 0.1f;
     public float fastMovementSpeed = .5f;
     public float sneakyMovementSpeed = 0.05f;
     public float movementSmoothTime = 1f;

     public float currentMovementSpeed = 0f;

     public Vector3 zoomAmount;

 
     private Vector3 newZoom;
     private Vector3 newPosition;
     private Quaternion newRotation;


     //Clamp
     public float currentCameraHeight;
     public float minHeight;
     public float maxHeight;
     // Start is called before the first frame update
     void Start()
     {
          newPosition = character.position;
          newRotation = character.rotation;
          newZoom = cameraTransform.localPosition;
     }

     // Update is called once per frame
     void Update()
     {
          HandleInput();
     }

     public void ShakeCamera()
     {

     }

     private void HandleInput()
     {

          if(Input.GetKeyDown(KeyCode.Mouse0))
          {
               characterAnimator.SetTrigger("attack");
          }

          Vector3 rot = character.rotation.eulerAngles;
       

          float xDirection = Input.GetAxis("Horizontal");
          float zDirection = Input.GetAxis("Vertical");

          if (xDirection == 0 && zDirection == 0) return;
          Vector3 moveDirection = new Vector3(xDirection, 0, zDirection);

          moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

          if (moveDirection.magnitude > .1f)
          {
               characterAnimator.SetInteger("movement", 2);
          } else
          {
               characterAnimator.SetInteger("movement", 0);
          }

          if (Input.GetKey(KeyCode.LeftShift))
          {
               characterAnimator.SetInteger("movement", 3);
               currentMovementSpeed = fastMovementSpeed;
          }
          else if (Input.GetKey(KeyCode.LeftControl))
          {
               characterAnimator.SetInteger("movement", 1);
               currentMovementSpeed = sneakyMovementSpeed;
          }
          else
          {
               currentMovementSpeed = normalMovementSpeed;
          }

          transform.position = Vector3.Lerp(transform.position, transform.position += moveDirection * currentMovementSpeed, Time.deltaTime * movementSmoothTime);
          character.rotation = Quaternion.Lerp(character.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * movementSmoothTime);

     }
}
