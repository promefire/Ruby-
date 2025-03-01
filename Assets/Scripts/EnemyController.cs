using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    public int speed = 3;
    public float changeDirectionTime = 2f;//改变方向的时间。
    public float changeTimer;//改变方向的计时器 

    private int Health = 5;

    public int getHealth{get {return Health;}}

    private bool isDead;

    public ParticleSystem brokenEffect;//获取特效组件

    // private Health;
    private Rigidbody2D rbody;
    public bool isVertical;
    private Vector2 moveDirection;
    private Animator anim;//获取动画组件

    public AudioClip robotFixedClip;

    // public AudioClip robotWalkClip;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveDirection = isVertical ? Vector2.up : Vector2.right;
        changeTimer = changeDirectionTime;
        isDead = false;
    
        

    }

    // Update is called once per frame
    void Update()
    {
        if(isDead){return;}//挂了就别移动了
        changeTimer -= Time.deltaTime;
        if(changeTimer < 0){
            moveDirection *= -1;
            changeTimer = changeDirectionTime;
        }
        Vector2 position = rbody.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;
        rbody.MovePosition(position);
        anim.SetFloat("moveX",moveDirection.x);
        anim.SetFloat("moveY",moveDirection.y);

    }
    //怪物掉血
    public void ChangeHealth(int amount){
        Health = Math.Clamp(Health + amount ,0,Health);
        if(Health == 0){
            Fixed();
            Destroy(this.gameObject,3f);
        }
        Debug.Log(Health);
    }
    //播放死亡动画
    public void Fixed(){
        isDead = true;
        if(brokenEffect.isPlaying == true){
            brokenEffect.Stop();
        }
        AudioManager.instance.AudioPlay(robotFixedClip);//播放音效
        //死了就没碰撞掉血了
        rbody.simulated = false;
        anim.SetTrigger("fix");
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if(pc != null){
            pc.ChangeHealth(-1);
        }
    }
}
