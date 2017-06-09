using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nameScript : MonoBehaviour
{
    //add life count

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Allways looking down - you can see the writing clearly
        this.transform.LookAt(new Vector3(this.transform.position.x, this.transform.position.y - 10f, this.transform.position.z));
    }
}
