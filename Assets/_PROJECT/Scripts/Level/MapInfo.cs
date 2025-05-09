using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    public Node startNode;                         //start node
    public Node targetNode;                        //goal node ai need to reach
    public List<Node> allNode = new List<Node>();  //all node in the level
    public AIBehaviour AI;                         //the ai itself

    void Awake()
    {
        AI.enabled = false;
    }

    void Start()
    {
        MapInitialize();
    }

    private void MapInitialize()
    {
        //setup node
        AStarManager.instance._allNode = allNode;
        //assign target node to AI
        AI.targetNode = targetNode;
        //assign start node to AI
        AI.currentNode = startNode;
        //enable AI
        AI.enabled = true;

    }
}
