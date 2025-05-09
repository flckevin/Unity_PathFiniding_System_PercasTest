using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System;
public class AStarMapGenerator : OdinEditorWindow
{
    
    /// <summary>
    /// function to open custom window on unity
    /// </summary>
    [MenuItem("PercasTest/MapGenerator")]
    private static void OpenWindow()
    {
        GetWindow<AStarMapGenerator>().Show();
    }

    [Header("AStar_Properties")]
    public TextAsset jsonLevel; //json file to read
    public Vector2Int offSet;      //offset position for the whole map so it can be center at a poisiton as user like
    
    //======================== PRIVATE VAR ========================
    
     // ===> DATA
    private Node _nodePrefab => Resources.Load<Node>("Node");   //getting node prefab from resource folder
    private GameObject _nodeParent;                             //parent of all nodes so it will have a cleaner visual on hierachy
    private List<Node> _nodeList = new List<Node>();            //a list containing all the generated nodes
    private MapInfo _mapInfo;                                   //info of the map
    
    // ===> JSON
    private RootJson _jsonData;                                 //json data file for tiles posiiton
    
    //======================== PRIVATE VAR ========================

    /// <summary>
    /// function to start generating map
    /// </summary>
    [Button("GENERATE")]
    private void GenerateMap()
    {
        //generate map sequences
        Setup();
        ReadJson();
        CreateNodes();
        CreateConnection();
    }

    /// <summary>
    /// setup function
    /// </summary>
    private void Setup()
    {
        //if there are node parent then destroy it
        if(_nodeParent != null) DestroyImmediate(_nodeParent);
        //creating new gameobject of node parent
        _nodeParent = new GameObject("Node_Parent");
        //add map info into the node
        _nodeParent.AddComponent<MapInfo>();
        //get map info from ndoe parent
        _mapInfo = _nodeParent.GetComponent<MapInfo>();
    }

    /// <summary>
    /// function to read data from json file
    /// </summary>
    private void ReadJson()
    {
        //reading json data an transfering to _jsonData class
        _jsonData = JsonUtility.FromJson<RootJson>(jsonLevel.text);
    }


    #region  ======================== CREATE NODES ========================
    /// <summary>
    /// function to create nodes base on given json data
    /// </summary>
    private void CreateNodes()
    {
        //loop on y axis
        for(int y = 0 ; y < _jsonData.mapData.Count;y++)
        {
            //loop on x axis
            for(int x = 0 ; x < _jsonData.mapData[y].mapXPos.Count ; x++ )
            {

                switch(_jsonData.mapData[y].mapXPos[x])
                {
                    case 0:
                    //call path creator function
                    CreateNodes_SpawnPath(x,y,PathType.path,Color.white);
                    break;

                    case 1:
                    //create Obstacle
                    GameObject _object = CreateNodes_ObjectSpawner(Color.gray,"Wall");
                    //set position for the wall
                    _object.transform.position = new Vector2(x,y);
                    //set parent for the wall
                    _object.transform.parent = _nodeParent.transform;
                    break;

                    case 6:
                    //call path creator function
                    CreateNodes_SpawnPath(x,y,PathType.path,Color.white);
                    //create AI Object
                    GameObject _AI = CreateNodes_ObjectSpawner(Color.green,"AI");
                    //add AI behaviour
                    _AI.AddComponent<AIBehaviour>();
                    //add ai into map info
                    _mapInfo.AI = _AI.GetComponent<AIBehaviour>();
                    //parent into map
                    _AI.transform.parent = _mapInfo.transform;
                    break;

                    case 7:
                    //call path creator function
                    CreateNodes_SpawnPath(x,y,PathType.goal,Color.red);
                    break;
                }
                
            }
        }
    }

    /// <summary>
    /// function to create path for ai
    /// </summary>
    /// <param name="_x">x position</param>
    /// <param name="_y">y postiion</param>
    private void CreateNodes_SpawnPath(int _x, int _y,PathType _pathType,Color _spriteColor)
    {
        //spawning new nodes
        Node _spawnedNode = Instantiate(_nodePrefab);
        //calculating final x postion - F stand for final (fx) - Final X value
        int _fX = _x + offSet.x; 
        //calculating final y postion - F stand for final (fy) - Final Y value
        int _Fy = _y + offSet.y;

        //moving node position base on given x and y position with offset position
        _spawnedNode.transform.position = new Vector2(_fX,_Fy);
        //parenting spawned node into node parent
        _spawnedNode.transform.parent = _nodeParent.transform;
        //store that node into node list
        _nodeList.Add(_spawnedNode);
        //add node into map
        _mapInfo.allNode.Add(_spawnedNode);

        //change color
        _spawnedNode.GetComponent<SpriteRenderer>().color = _spriteColor;

        //if pathtype is goal
        if(_pathType == PathType.goal)
        {
            //then set goal for map info
            _mapInfo.targetNode = _spawnedNode;
            
        }
    }

    
    /// <summary>
    /// function to create node obstacle and image with it 
    /// </summary>
    /// <param name="_x"> x position </param>
    /// <param name="_y"> y position </param>
    /// <param name="_color"> color of the obstacle </param>
    private GameObject CreateNodes_ObjectSpawner(Color _color,string _name)
    {
        //create new object with name wall
        GameObject _object = new GameObject (_name);
        //add sprite renderer into it so we can render sprite
        SpriteRenderer _objectSpriteRenderer = _object.AddComponent<SpriteRenderer> ();
        //create new texture so we can have an image
        Texture2D _objectTexture = new Texture2D(100,100);
        //set pixel for that whole texture with chosen color
        _objectTexture.SetPixel(0, 0, _color);
        //apply all of it into sprite renderer
        _objectTexture.Apply();
        //create new sprite so we can put it into renderer
        Sprite _objectSprite = Sprite.Create(_objectTexture, new Rect(0, 0, _objectTexture.width, _objectTexture.height), new Vector2(0.5f,0.5f));

        //set sprite to sprite renderer
        _objectSpriteRenderer.sprite = _objectSprite;
        //set color to sprite renderer
        _objectSpriteRenderer.color = _color;

        return _object;
    }
    

    #endregion


    /// <summary>
    /// function to create connection on surrounding neighbour in current node
    /// </summary>
    private void CreateConnection()
    {
        //get a node
        for(int i = 0; i< _nodeList.Count ; i++)
        {
            //get all neighbours except the current one
            for(int j = i + 1; j < _nodeList.Count ; j++)
            {
                //if the distance between neightbour is not far
                if(Vector2.Distance(_nodeList[i].transform.position,_nodeList[j].transform.position) <= 1f)
                {
                    //connect current node to neighbour
                    Helper_NodeConnector(_nodeList[i],_nodeList[j]);
                    //connect neighbour to current node
                    Helper_NodeConnector(_nodeList[j],_nodeList[i]);
                }
            }
        }
    }

  
    #region  ============ HELPER FUNCTIONS ============

    private void Helper_NodeConnector(Node from , Node to)
    {
        //if the origin node is the same as target node then stop
        if(from == to) return;
        //if not
        //then connect to neighbour
        from.neighbours.Add(to);
    }

    #endregion

}

# region ================== JSON CLASSES ==================
[Serializable]
public class MapDataX
{
    public List<int> mapXPos = new List<int>();
}

[Serializable]
public class RootJson
{
    public List<MapDataX> mapData = new List<MapDataX>();
}
#endregion

public enum PathType
{
    path,
    goal,
}