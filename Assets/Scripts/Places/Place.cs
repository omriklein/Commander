using System.Collections;
using UnityEngine;

public class Place : MonoBehaviour
{

    public const float MAX_HP = 100; // the maximum HP ?int?
    public float health; // the current HP ?int?
    public bool canAttack = false; // can this place attack players and soldiers?
    public string placeName; // the name of the place this.gameobject.name

    public team team = team.noTeam; // witch team does this place belong to

    // Use this for initialization
    void Start()
    {
        health = MAX_HP; // set the health points
        placeName = this.gameObject.name; // set the name
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            killSelf();
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet") // if bullet hits the place
        {
            health -= collision.transform.GetComponent<Bullet>().damage; // damage the place
            collision.transform.GetComponent<Bullet>().destroyBullet(); // kill the bullet
        }
    }

    /// <summary>
    /// kills the object
    /// </summary>
    public void killSelf()
    {
        //play animation
        Debug.Log(this.gameObject.name + " is finally destroied :( ");
        GameObject.Destroy(this.gameObject);
    }

}
