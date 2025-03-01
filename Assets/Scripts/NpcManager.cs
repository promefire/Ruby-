using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcManager : MonoBehaviour
{

    public GameObject dialogImage;

    public GameObject tipImage;

    public float showTime = 4f;

    public float showTimer;//计时器
    // Start is called before the first frame update
    void Start()
    {
        showTimer = -1;
        tipImage.SetActive(true);
        dialogImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        showTimer -= Time.deltaTime;
        if(showTimer < 0){
            dialogImage.SetActive(false);
            tipImage.SetActive(true);
        }
    }
    //显示对话框
    public void show(){
        showTimer = showTime;
        tipImage.SetActive(false);
        dialogImage.SetActive(true);


    }
}
