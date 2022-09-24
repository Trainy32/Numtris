using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{

    public static gameManager I;

    void Awake()
    {
        I = this;
    }

    int currentStage = 1;
    public Text currentStageTxt;


    int stageGoal = 300;
    public Text stageGoalTxt;

    GameObject[] valuesArr;
    public Text displyTotal;
    int totalValues = 0;

    public GameObject gameOverTxt;
    public GameObject clearTxt;

     private float previousTime;


    // Start is called before the first frame update
    void Start()
    {
        currentStageTxt.text = currentStage.ToString();
        stageGoalTxt.text = stageGoal.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreDisplay();

        if (GetTotal() >= stageGoal) {
            gameClear();

        }

        previousTime = Time.time;
    }

    void initGame() {
        Time.timeScale = 1.0f;
    }

    public void gameOver() {
        gameOverTxt.SetActive(true);
    }

    public void gameClear() {
        clearTxt.SetActive(true);
        Invoke("hideClearTxt", 1);
        nextStage();
    }

    public void hideClearTxt() {
        clearTxt.SetActive(false);
    }

    public void nextStage() {
        currentStage ++;
        stageGoal += 300;
        currentStageTxt.text = currentStage.ToString();
        stageGoalTxt.text = stageGoal.ToString();

    }

    void ScoreDisplay() {
        valuesArr = GameObject.FindGameObjectsWithTag("BlockValue");
        totalValues = GetTotal();
        displyTotal.text = totalValues.ToString();
    }


    int GetTotal() {
        int valueSum = 0;

        for(int i = 0; i< valuesArr.Length; i++) {
           valueSum += int.Parse(valuesArr[i].GetComponent<Text>().text);
        }

        return valueSum;
    }
    

}


