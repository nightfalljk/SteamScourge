using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CustomInput : ScriptableObject
{
    public bool controllerLocked = false;
    public Vector3 moveDirection => new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    public bool isMoving => moveDirection != Vector3.zero && !controllerLocked;
    public bool isSprinting => isMoving && Input.GetKey(KeyCode.LeftShift);
    public bool isJumping => Input.GetKeyDown(KeyCode.Space) && !controllerLocked;
    public bool isInteracting => Input.GetKeyDown("e") && !controllerLocked;

}
