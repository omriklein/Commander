using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

enum things
{
    solider,
    tank,
    airplain,
    all,
    nothing
}

public class GameManager : MonoBehaviour
{
    public List<GameObject> soldiers; //???
    public List<GameObject> tanks; //???

    private Stack<string> previosLines = new Stack<string>();

    public GameObject soldierPrefub;
    public GameObject tankPrefub;

    private InputField input; // the input field
    private string target;

    // Use this for initialization
    void Start()
    {
        //soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        //tanks = GameObject.FindGameObjectsWithTag("Tank");
        input = GameObject.Find("InputField").GetComponent<InputField>();

        for (int i = 0; i < 3; i++)
        {
            soldiers.Add(GameObject.Instantiate(soldierPrefub, new Vector3(-10f + i * 3f, 0.5f, -9f), new Quaternion(0, 0, 0, 0)));
            soldiers[i].name = "Soldier" + i;
            soldiers[i].GetComponentInChildren<TextMesh>().text = "Soldier " + i;
            tanks.Add(GameObject.Instantiate(tankPrefub, new Vector3(3f + i * 5f, 0.5f, -9f), new Quaternion(0, 0, 0, 0)));
            tanks[i].name = "Tank" + i;
            tanks[i].GetComponentInChildren<TextMesh>().text = "Tank " + i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        { input.DeactivateInputField(); }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        { input.ActivateInputField(); }
        input.Select(); // set the cursor to the input field

        // on submit input field
        if (Input.GetKeyDown(KeyCode.Return))
        {
            addLineToStack(input.text);
            interpator(input.text);
            //S1,S2,S3 becon => soldiers 1 2 and 3 goto becon
            //split(' ') => split(',')
            input.text = ""; // zeroize the input field text
            input.ActivateInputField(); // activate the input field
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (previosLines.Count != 0)
                input.text = previosLines.Pop();
            else
                print("there are no previos lines");
        }
    }

    private void interpator(string text)
    {
        if (text.Length == 0)
        { print("Error..."); return; }

        string[] arr = text.Split(' '); // splits the soldiers,tank ect from place
        string[] moving = arr[0].Split(',');

        if (arr.Length != 2)
        { print("you need 2 words and a space"); }

        target = arr[arr.Length - 1];
        things thing;

        foreach (string str in moving) // each element separated by ','
        {
            if (str == "All")
            {
                for (int j = 0; j < soldiers.Count; j++)
                {
                    move(things.solider, j, target);
                }
                for (int j = 0; j < tanks.Count; j++)
                {
                    move(things.tank, j, target);
                }
                break;
            }
            else
            {
                switch (str[0])
                {
                    case 'S': thing = things.solider; break;
                    case 'T': thing = things.tank; break;
                    case 'A': thing = things.airplain; break;
                    default: thing = things.nothing; break;
                }
                if (str.Substring(1).Contains("-"))
                {
                    if (str.Substring(1).Split('-').Length == 2)
                    {
                        for (int i = int.Parse(str.Substring(1).Split('-')[0]); i <= int.Parse(str.Substring(1).Split('-')[1]); i++)
                        {
                            move(thing, i, target); // what? whice number? to where?
                        }
                    }
                    else
                    {
                        print("can't do " + str);
                        break;
                    }

                    print("in progress...");
                }
                else
                {
                    move(thing, int.Parse(str.Substring(1)), target); // what? whice number? to where?
                }
            }
        }
    }

    /// <summary>
    /// moves "something" at "number" to "place"
    /// </summary>
    /// <param name="text"></param>
    /// <param name="number"></param>
    private void move(things thing, int number, string place)
    {
        try
        {
            switch (thing)
            {
                case things.solider:
                    soldiers[number].GetComponent<Soldier>().goTo(GameObject.Find(place));
                    break;
                case things.tank:
                    print(tanks[number].name);
                    tanks[number].GetComponent<Soldier>().goTo(GameObject.Find(place));
                    break;
                case things.airplain:
                    soldiers[number].GetComponent<Soldier>().goTo(GameObject.Find(place));
                    break;
            }
        }
        catch { print("Error is found... cannot walk"); }
    }

    /// <summary>
    /// add a line to previosLines stack
    /// </summary>
    /// <param name="line">String</param>
    private void addLineToStack(string line)
    {
        if (line != null)
        {
            this.previosLines.Push(line);
        }
        // remove lines after 30 or 50 lines so save place
    }
}
