// Floater v0.0.2
// by Donovan Keith
//
// [MIT License](https://opensource.org/licenses/MIT)

using System;
using UnityEngine;
using System.Collections;
 
// Makes objects float up & down while gently spinning.
public class WaterFloat : MonoBehaviour {
    // User Inputs
    public float ydegreesPerSecond = 3.0f;
    public float xdegreesPerSecond = 3.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    private bool flipX = false;
    private int lastX = -30;
 
    // Position Storage Variables
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
 
    // Use this for initialization
    void Start () {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }
     
    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(Time.deltaTime * xdegreesPerSecond, Time.deltaTime * ydegreesPerSecond, 0f), Space.World);
 
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}