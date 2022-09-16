using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager : MonoBehaviour
{
    // Start is called before the first frame update

    string [] levelInfo;
    string myFilePath, fileName;
    public boardGrid grid;
    


    void Start()
    {
        LevelMoves level = GameObject.Find("Level").GetComponent<LevelMoves>();
        print(level.level);
        
        fileName = "RM_A" + level.level;
        myFilePath = Application.streamingAssetsPath + "/Files/" + fileName + ".txt"; 
        ReadFromFile(level);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReadFromFile(LevelMoves levelMoves)
    {
        levelInfo = File.ReadAllLines(myFilePath);
        string[] getWidth = levelInfo[1].Split(char.Parse(":"));
        string[] getHeight = levelInfo[2].Split(char.Parse(":"));
        string[] movesCount = levelInfo[3].Split(char.Parse(":"));
        string[] getGridArray = levelInfo[4].Split(char.Parse(":"));
        grid.grid_width = int.Parse(getWidth[1]);
        grid.grid_height = int.Parse(getHeight[1]);
        levelMoves.numMoves = int.Parse(movesCount[1]);
 
        char[] newArray = new char[grid.grid_width * grid.grid_height];
        int count = 0;
        foreach(char i in getGridArray[1])
        {
            print(i);

            if(i != ',' && i != ' ')
            {
                newArray[count] = i;
                count++;
            }
        }

        grid.gridArray = newArray;

    }
}
