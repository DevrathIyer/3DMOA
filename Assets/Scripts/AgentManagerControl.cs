using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using UnityEngine.Networking;

public class Datapoint : MonoBehaviour
{
    public string experimentID;
    public string clicks;
    public string question;
    public string key;
    public string type;
    public string worker;
    public string agents;
    public string pos;
    public bool testpoint;
}

public class AgentManagerControl : MonoBehaviour
{
    public static AgentManagerControl instance = null;

    const int prac = 5;
    const int trials = 30;
    int trial = 0;
    public int n;
    public int testFlag = 0;
    bool coroutineFlag = true;
    public int target = 0;
    public int nClicks = 0;
    public bool OpenMenu = false;
    public bool clicked = false;

    public List<int> clicks = new List<int>();

    public static int frames = 600;
    GameObject[] agents;
    public GameObject agent;
    public GameObject newAgent = null;
    public GameObject camera;

    public GameObject player;

    public static float agentSpeed = 2f;

    public GameObject Instruction;
    GameObject surface;
    StreamWriter writer;

    public GameObject Scene1;
    public string[] colors = new string[] {"gray", "white", "red", "blue", "green", "yellow", "orange", "pink", "purple", "brown", "cyan", "maroon", "lavender", "navy", "lime", "beige" };
    Material[] mats = new Material[16];

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime((float).5);
        while (!Input.GetMouseButtonDown(0));

        testFlag = 0;
        Debug.Log("Pressed primary button.");
        Instruction.GetComponent<TextMeshPro>().text = "BLEH";
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        writer = new StreamWriter(File.Open("data.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite));

        string headers = "Question,Practice,Target,nClicks,Clicks";
        n = PlayerPrefs.GetInt("AgentCount");
        agents = new GameObject[n];
        for (int i = 0; i < n; i++)
        {
            headers += ",Agent " + i + " X,Agent " + i + " Y";
        }
        writer.WriteLine(headers);

        //Instruction = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        for (int i = 0; i < 16; i++)
        {
            mats[i] = (Material)Resources.Load("Material/Colors/" + colors[i]);
        }

        agentSpeed = PlayerPrefs.GetFloat("AgentSpeed",1f);
        if (agentSpeed < 0.1)
        {
            PlayerPrefs.SetFloat("AgentSpeed", 1f);
            agentSpeed = PlayerPrefs.GetFloat("AgentSpeed", 1f);
        }
        for (int i = 0; i < n; i++)
        {
            newAgent = Instantiate(agent, new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f)), Quaternion.identity);
            newAgent.GetComponent<AgentControl>().id = i;
            newAgent.GetComponent<AgentControl>().color = mats[i];
            newAgent.GetComponent<NavMeshAgent>().speed = agentSpeed;
            newAgent.transform.parent = Scene1.transform;
            agents[i] = newAgent;

            surface = newAgent.transform.GetChild(1).gameObject;
            surface.GetComponent<SkinnedMeshRenderer>().material = mats[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        OpenMenu = camera.GetComponent<CameraControl>().OpenMenu;
        if(!OpenMenu)
        {
            if (frames != 0)
            {
                Instruction.GetComponent<TextMeshPro>().text = "";
                frames -= 1;
            }
            if (frames == 0)
            {

                if (testFlag == 0)
                {
                    target = (int)Random.Range(0, n);
                    Instruction.GetComponent<TextMeshPro>().text = $"FIND THE {colors[target].ToUpper()} AGENT";
                    nClicks = 0;
                    clicks = new List<int>();
                    testFlag = 1;
                    trial++;
                }
                if (testFlag == 2)
                {
                    if (clicked)
                    {
                        Datapoint point = new Datapoint();

                        string click_data = "";
                        foreach (var click in clicks)
                        {
                            click_data += click + ",";
                        }
                        click_data = click_data.Substring(0, click_data.Length - 1);

                        string agent_data = "";
                        foreach (var iteragent in agents)
                        {
                            agent_data += iteragent.transform.position.x + "," + iteragent.transform.position.z + ",";
                        }
                        agent_data = agent_data.Substring(0, agent_data.Length - 1);

                        point.experimentID = PlayerPrefs.GetString("ExperimentID").Trim();
                        point.experimentID = point.experimentID.Substring(0, point.experimentID.Length - 1);
                        point.clicks = click_data;
                        point.question = trial.ToString().Trim();
                        point.key = PlayerPrefs.GetString("Key").Trim();
                        point.type = ((trial > prac) ? "1" : "0");
                        point.worker = PlayerPrefs.GetString("Worker").Trim();
                        point.pos = (player.transform.position.x + "," + player.transform.position.y);
                        point.agents = agent_data;
                        point.testpoint = false;
                        Debug.Log(JsonUtility.ToJson(point));
                        Debug.Log(point.experimentID.Equals("HelloWorld"));
                        Debug.Log("HelloWorld".Length);

                        UnityWebRequest www = UnityWebRequest.Put("https://searchbwh.herokuapp.com/data/submit", JsonUtility.ToJson(point));

                        www.SetRequestHeader("Content-type", "application/json");
                        UnityWebRequestAsyncOperation request = www.SendWebRequest();

                        frames = 30 * Random.Range(10, 30);
                        testFlag = 0;
                        clicked = false;
                        if (trial == trials)
                        {
                            testFlag = 4;
                            writer.Close();
                        }
                    }
                    else
                    {
                        Instruction.GetComponent<TextMeshPro>().text = $"Trial {trial}/{trials} complete in {nClicks} " + ((nClicks > 1) ? "clicks" : "click") + "! Right Click to Continue...";
                        if (Input.GetMouseButton(1))
                        {
                            clicked = true;
                        }
                    }

                }
            }
        }
    }
}
