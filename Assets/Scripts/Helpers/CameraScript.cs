using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public float speed = 5f;

    public float borderUp;
    public float borderDown;
    public float borderRight;
    public float borderLeft;

    public float zoomOutLimit;
    public float zoomInLimit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        { move(); }
    }

    /// <summary>
    /// move this object with arrows
    /// </summary>
    private void move()
    {
        if (Input.GetKey("up") && this.transform.position.z <= borderUp)
            this.transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (Input.GetKey("down") && this.transform.position.z >= borderDown)
            this.transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (Input.GetKey("right") && this.transform.position.x <= borderRight)
            this.transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (Input.GetKey("left") && this.transform.position.x >= borderLeft)
            this.transform.Translate(Vector3.left * speed * Time.deltaTime);

        if ((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && this.transform.position.y >= zoomInLimit)
        {
            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) && this.transform.position.y <= zoomOutLimit)
                this.transform.Translate(Vector3.back * speed * Time.deltaTime);
            else
                this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
