using UnityEngine;
using System.Collections;

/// <summary>
/// team number 1, 2 or not at any team
/// </summary>
public enum team
{
    team1,
    team2,
    noTeam
}

/////TODO make soldier attack another 

/// <summary>
/// A basic soldier class 
/// a soldier is something that the player controll (stuff on the map)
/// </summary>
[RequireComponent(typeof(Rigidbody))] // any soldier require a rigidbody component

public class Soldier : MonoBehaviour
{
    //turn to 'private'
    /// <summary>
    /// the maximum HP
    /// </summary>
    public const float MAX_HP = 100;

    public float health; /// the current HP
    public bool isAttack; // if this soldier is attacking?????????
    public float attackPower = 3; // the attack power of the soldier
    public float speed = 0.1f; // the walking speed

    public float distance = 50f; // the "safe" distance from the target he is doing to

    public team myTeam; // this sildier's team

    private GameObject targetObject; // protected??? to where the soldier is walking

    // Use this for initialization
    protected virtual void Start()
    {
        health = MAX_HP; // a soldier health begins full
        isAttack = false; // turn to private
        targetObject = null; // turn to private
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (targetObject != null) // if the soldier has a traget
            walkTo(targetObject.transform.position); // walk to that target
        if (health <= 0) // if the soldier is dead (ho health)
            killSelf(); // kill this soldier 
        //TODO maybe to this in the desrtactor ^^ (kill self function)
    }

    public void setName(string name)
    {
        this.transform.name = name;
        this.transform.GetComponentInChildren<TextMesh>().text = name;
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
        if (/*this.transform.position != target*/Vector3.Distance(this.transform.position, target) >= distance)
        {
            this.transform.LookAt(target);
            this.transform.Translate(Vector3.forward * Mathf.Clamp(Vector3.Distance(this.transform.position, target), 0, speed));
        }
        else
        {
            this.transform.LookAt(target);
            this.targetObject = null;
        }
    }

    private void walkTo2(Vector3 target)
    {
        if (this.transform.position != target)
        {
            this.transform.LookAt(target);
            if (Vector3.Distance(this.transform.position, target) >= speed)
                this.transform.Translate(Vector3.forward * speed);
            else
                this.transform.position = target;
        }
    }

    /// <summary>
    /// attack a place or soldier
    /// </summary>
    /// <param name="attackTarget">the target you want to attack</param>
    public virtual void attack(GameObject attackTarget)
    {
        //animation + delay
        print("attack");
        if (attackTarget.GetComponent<Place>() != null)
        {
            //attacking a place
            attackTarget.GetComponent<Place>().health -= attackPower;
        }

        if (attackTarget.GetComponent<Soldier>() != null)
        {
            //attacking a soldier
            attackTarget.GetComponent<Soldier>().health -= attackPower;
        }
    }

    /// <summary>
    /// kills the object
    /// </summary>
    public void killSelf()
    {
        //play animation
        Debug.Log(this.gameObject.name + " is finally dead :( ");
        GameObject.Destroy(this.gameObject);
    }

    /// <summary>
    /// when you enter some trigger
    /// </summary>
    public void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.GetComponent<Soldier>() != null && other.gameObject.GetComponent<Soldier>().myTeam != this.myTeam) ||
            (other.gameObject.GetComponent<Place>() != null && other.gameObject.GetComponent<Place>().team != this.myTeam))
        {
            attack(other.gameObject);
        }
    }

    public void OnCollisionStay(Collision other)
    {
        if ((other.gameObject.GetComponent<Soldier>() != null && other.gameObject.GetComponent<Soldier>().myTeam != this.myTeam) ||
            (other.gameObject.GetComponent<Place>() != null && other.gameObject.GetComponent<Place>().team != this.myTeam))
        {
            attack(other.gameObject);
        }
    }

    /*private void OnCollisionEnter(Collision collision)
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
    }*/
}
