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

    int stageGoal = 1000;
    int totalValues = 0;
    GameObject[] valuesArr;
    public Text displyTotal;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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


