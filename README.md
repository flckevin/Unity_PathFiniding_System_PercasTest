# ABOUT THE PROJECT
<ins> </ins>
This is a simple Path Finding system using A* algorithim to execute it.<br>
The project also has tool to generate map base on data of Json file
<br>

Estiamted time to finish : 4h36 min
<br>
AI Usage : HELL NO


![Image](https://github.com/user-attachments/assets/f53d13c0-4a1b-426e-a92f-3feb43dacf30)

![Image](https://github.com/user-attachments/assets/c569fef0-ed87-455a-b09f-5a19fe1d841b)

<br>

# MAP GENERATION
<ins> </ins>
## HOW TO OPEN TOOL WINDOWS
<br>
In order to generate a map, you can locate tool on navigation bar<br>
then choose "QuocAnhHoangCustomTool -> AStarMapGenerator" <br>
A new window shall pop up with a list contanining information of<br>
<ol>
  <li>Json Level -> which json file to put it in</li>
  <li>AI Speed -> speed of AI Movement</li>
  <li>Off set -> which is off set for x and y axis of the map position</li>
</ol>

![Image](https://github.com/user-attachments/assets/35029c40-78fa-42d0-b382-f14405c523db)


<br>

## HOW TO GENERATE MAP
<br>
Now you know where to navigate the window but before you can generate a map<br>
you will need to fill up data in json file then generate it<br>

### Json File Template (20x20)
```json
{
    "mapData":
    [
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,0]},
        {"mapXPos":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]}
    ]
}
```
<br>
Annotation: <br>
<ul>
  <li>ID: 0 -> for path</li>
  <li>ID: 1 -> for obstacle</li>
  <li>ID: 6 -> for the starting point and AI</li>
  <li>ID: 7 -> for goal</li>
</ul>
<br>
After filling up the data, you just need to navigate to the tool<br>
Put the Json file into the "Json Level" in the tool and maggiiiic<br>
<br>

![Image](https://github.com/user-attachments/assets/4bb4547b-e28c-4a03-b9d6-b3c874619fd5)
