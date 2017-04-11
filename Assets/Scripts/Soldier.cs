using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
public class Soldier : MonoBehaviour
{ 
    //turn to 'private'
    public const float MAX_HP = 100; // the maximum HP
    public float health; // the current HP
    public bool isAttack; // if this soldier is attacking
    public float speed = 0.1f; // the walking speed

    public Vector3 testPoint;
    public GameObject targetObject;

    // Use this for initialization
    void Start()
    {
        health = MAX_HP;
        isAttack = false; // turn to private
        targetObject = null; // turn to private
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject != null)
            walkTo(targetObject.transform.position);
    }

    public void walkTo(Vector3 target)
    {
        if (this.transform.position != target)
        {
            this.transform.LookAt(target);
            this.transform.Translate(Vector3.forward * Mathf.Clamp(Vector3.Distance(this.transform.position, target), 0, speed));
        }
        else
        {
            this.targetObject = null;
        }
    }

    private void walkTo2(Vector3 target)
    {
        if (this.transform.position != target)
        {
            this.transform.LookAt(testPoint);
            if (Vector3.Distance(this.transform.position, target) >= speed)
                this.transform.Translate(Vector3.forward * speed);
            else
                this.transform.position = target;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(targetObject != null && collision.gameObject.name==targetObject.name)
        {
            targetObject = null;
        }
        else if (collision.gameObject.tag == "Soldier")
        {
            this.transform.Translate(Vector3.right * speed);
        }
    }
}
