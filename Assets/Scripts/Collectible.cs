using static System.Console;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// using System.Math;

public class Collectible : MonoBehaviour
{

    
    public ParticleSystem collectEffect;

    public AudioClip collectClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 碰撞检测相关类
    // <param name="other"></param>

    void OnTriggerEnter2D(Collider2D other){
        //检测一下与草莓碰撞的物体有没有挂载playercontroller脚本
        PlayerController pc = other.GetComponent<PlayerController>();
        if(pc != null){
            if(pc.MyCurrentHealth < pc.MyMaxHealth){
            
            pc.ChangeHealth(1);  
        
            
            Instantiate(collectEffect,transform.position,Quaternion.identity);
            AudioManager.instance.AudioPlay(collectClip);//播放音效
            Destroy(this.gameObject);
            }
            
            // Debug.Log("玩家碰到草莓");
        }
    }
}
