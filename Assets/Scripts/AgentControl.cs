using UnityEngine;
using UnityEngine.AI;

public class AgentControl : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 goal;
    public NavMeshAgent agent;
    public int id;
    bool locked = false;
    bool pauseLocked = false;
    bool notReset = false;
    bool testing_once = false;
    Material black, highlight;
    public Material color;

    Animator anim;

    void Start()
    {
        black = (Material)Resources.Load("Material/Colors/black");
        highlight = (Material)Resources.Load("Material/Colors/highlight");

        anim = GetComponent<Animator>();
        anim.SetBool("walking", true);

        goal = new Vector3(Random.Range(-10.0f, 10.0f), 0f, Random.Range(-10.0f, 10.0f));
        agent.SetDestination(goal);
    }

    // Update is called once per frame
    void Update()
    {
        if (AgentManagerControl.instance.OpenMenu)
        {
            anim.enabled = false;
            pauseLocked = true;
            notReset = true;
        }
        else
        {
            if(notReset)
            {
                anim.enabled = true;
                pauseLocked = false;
                notReset = false; 
            }
        }
        if (AgentManagerControl.instance.testFlag !=0)
        {
            if(!testing_once)
            {
                testing_once = true;
                anim.SetBool("walking", false);
                agent.SetDestination(agent.transform.position);
                this.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material = black;
            }
        }
        else
        {
            if(testing_once)
            {
                    testing_once = false;
                    locked = false;
                    anim.SetBool("walking", true);
                    this.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material = color;
            }
            agent.destination = goal;
            if (Mathf.Abs(agent.remainingDistance - agent.radius) < agent.radius)
            {
                goal = new Vector3(Random.Range(-10.0f, 10.0f), 0f, Random.Range(-10.0f, 10.0f));

                agent.SetDestination(goal);
            }
        }

    }
    void OnMouseEnter()
    {
        if(AgentManagerControl.instance.testFlag == 1 && !locked && !pauseLocked)
            this.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material = highlight;
    }
    void OnMouseExit()
    {
        if (AgentManagerControl.instance.testFlag == 1 && !locked && !pauseLocked)
            this.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material = black;
    }
    private void OnMouseDown()
    {
        if(!locked && !AgentManagerControl.instance.OpenMenu)
        {
            AgentManagerControl.instance.nClicks++;
            AgentManagerControl.instance.clicks.Add(id);
            if (AgentManagerControl.instance.target == id)
            {
                AgentManagerControl.instance.testFlag = 2;
            }
            else
            {
                this.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material = color;
            }
            locked = true;
        }
    }
}
