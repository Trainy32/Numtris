using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.4f;
    public static int height = 21;
    public static int width = 12;
    private static Transform[,] grid = new Transform[width, height+1];
    public Text[] blockValues = new Text[4];


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++) {
            blockValues[i].text = Random.Range(-99,99).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.position += new Vector3(-1f, 0f, 0f);

            if(!ValidMove()){
                transform.position -= new Vector3(-1f, 0f, 0f);
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.position += new Vector3(1f, 0f, 0f);

            if(!ValidMove()){
                transform.position -= new Vector3(1f, 0f, 0f);
            }
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            if(!ValidMove()){
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
        

        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime )) {
            transform.position += new Vector3(0f, -1f, 0f);

            if(!ValidMove()){
                transform.position -= new Vector3(0f, -1f, 0f);
                AddToGrid();
                CheckForLines();

                this.enabled = false;

                if(this.transform.position.y <= height-3){
                    FindObjectOfType<Spawner>().NewGameBlocks();
                } else {
                    Debug.Log("Game Over");
                    gameManager.I.gameOver();
                }
            }
            previousTime = Time.time;
        }
        
    }

    void CheckForLines() {
        for (int i = height-1; i >=0; i--) {
            if(HasLine(i)) {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine(int i) {
        Debug.Log(i);
        for(int j = 0; j< width; j++) {
            if(grid[j, i] == null) {
                return false;
            }
        }

        return true;
    }


    void DeleteLine(int i) {
        for(int j = 0; j< width; j++) {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i) {
        for (int y = i; y < height; y++){
            for (int j = 0; j < width; j++) {
                if(grid[j, y] != null) {
                    grid[j, y-1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y-1].transform.position -= new Vector3(0, 1, 0);
                }   
            }
        }
    }

    void AddToGrid() {
        foreach (Transform children in transform) {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }


    bool ValidMove() {
        foreach (Transform children in transform) {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if(roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height) {
                Debug.Log("out of frame");
                return false;
            }

            if(grid[roundedX, roundedY] != null){
                Debug.Log("occupied block");
                return false;
            }

        }

        return true;
    }
    
}
