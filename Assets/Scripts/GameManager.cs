using UnityEngine;
using UnityEngine.UI;
using System.Collections;

enum things
{
    solider,
    tank,
    airplain,
    nothing
}

public class GameManager : MonoBehaviour
{
    public GameObject[] soldiers;

    public GameObject target = null;
    private InputField input; // the input field

    // Use this for initialization
    void Start()
    {
        soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        input = GameObject.Find("InputField").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        input.Select(); // set the cursor to the input field

        // get all the soldiers in the game
        foreach (GameObject s in soldiers)
        {
            //s.GetComponent<Soldier>().walkTo(target.transform.position);
        }

        // on submit input field
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //print(input.text); // print the text
            interpator(input.text);
            // S9becon = 9 soldiers to becon
            input.text = ""; // zeroize the input field text
            input.ActivateInputField(); // activate the input field
        }
    }

    private void interpator(string text)
    {
        things thing;
        int number = 0;

        if (text.Length == 0)
        { print("Error..."); return; }

        switch (text[0])
        {
            case 'S': thing = things.solider; break;
            case 'T': thing = things.tank; break;
            case 'A': thing = things.airplain; break;
            default: thing = things.nothing; break;
        }

        text = text.Substring(1);
        while (text.Length > 0 && text[0] >= '0' && text[0] <= '9')
        {
            number = number * 10 + ((int)text[0] - 48); // '0' is 48 in unicode
            text = text.Substring(1);
        }

        // sund "number" of "thing" to "place" ~ "text"
        print(thing + ", " + number + ", " + text);

        try
        {
            for (int i = 0; i < number; i++)
            {
                soldiers[i].GetComponent<Soldier>().targetObject = GameObject.Find(text);
            }
        }
        catch { print("Error is found... cannot walk"); }
    }
}
