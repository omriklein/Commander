using UnityEngine;
using System.Collections;

public enum team
{
    team1,
    team2,
    noTeam
}

[RequireComponent(typeof(Rigidbody))]
public class Soldier : MonoBehaviour
{ 
    //turn to 'private'
    public const float MAX_HP = 100; // the maximum HP
    public float health; // the current HP
    public bool isAttack; // if this soldier is attacking
    public float speed = 0.1f; // the walking speed

    public team myTeam;

    public Vector3 testPoint;
    private GameObject targetObject; // protected???

    // Use this for initialization
    protected virtual void Start()
    {
        health = MAX_HP;
        isAttack = false; // turn to private
        targetObject = null; // turn to private
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (targetObject != null)
            walkTo(targetObject.transform.position);
    }

    /// <summary>
    /// go to a target object
    /// </summary>
    /// <param name="targetObject">target GameObject</param>
    public void goTo(GameObject targetObject)
    {
        print("going....");
        this.targetObject = targetObject;
    }

    /// <summary>
    /// walks 1 speed towards a target
    /// </summary>
    /// <param name="target">vecter3</param>
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
            // give a new algorithem for soldiers how gets stuck
            this.transform.Translate(Vector3.right * speed * 10);
        }
    }
}
