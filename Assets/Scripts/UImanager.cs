using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UImanager : MonoBehaviour
{
    
    public TMP_Text  bulletCountText;

    public static UImanager instance{get;  private set;}

    void Start(){
        instance = this;
    }
    public Image healthBar;//角色血条

    public void UpdateHealthBar(int curAmount,int maxAmount){
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;



    }

    //更新子弹数量文本
    public void UpdateBulletCount(int curAmount,int maxAmount){
        bulletCountText.text = curAmount.ToString() + " / " + maxAmount.ToString();
    }
}
