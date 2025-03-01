using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    private int maxHealth = 5;
    private int currentHealth ;

    //玩家的朝向
    private   Vector2 lookDirection = new Vector2(1,0);//默认朝右  

    Rigidbody2D rbody;//刚体组件
    public int MyMaxHealth{get {return maxHealth;}}
    public int MyCurrentHealth{get{return currentHealth;}}

    private float invincibleTime = 2f;//无敌时间2秒

    private float invincibleTimer;//无敌计时器

    private bool isInvincible;//是否无敌

    private Animator anim;//获取动画组件

    public GameObject bulletPrefab;

    //音效
    public AudioClip hitClip; //受伤

    public AudioClip bulletClip;

    public int curBulletCount;

    public int maxBulletCount = 99;






    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        invincibleTimer = 0;
        currentHealth = maxHealth;

        curBulletCount  = 2;
        anim = GetComponent<Animator>();
        UImanager.instance.UpdateHealthBar(currentHealth,maxHealth);//更新血条
        UImanager.instance.UpdateBulletCount(curBulletCount,maxBulletCount);

    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(transform.right * speed * Time.deltaTime);
        float moveX = Input.GetAxisRaw("Horizontal");//控制水平
        float moveY = Input.GetAxisRaw("Vertical");//W:1,S:-1;


        //
        Vector2 moveVector = new Vector2(moveX,moveY);
        if(moveVector.x != 0 || moveVector.y != 0 ){
            lookDirection = moveVector;
        }
        anim.SetFloat("Look X",lookDirection.x);
        anim.SetFloat("Look Y",lookDirection.y);
        anim.SetFloat("Speed",moveVector.magnitude);//取向量长度

        
        //移动
        Vector2 position = rbody.position;
        // position.x += moveX * speed * Time.deltaTime;
        // position.y += moveY * speed * Time.deltaTime;
        // transform.position = position;  更新组件位置
        position += moveVector * speed *Time.deltaTime;
        rbody.MovePosition(position);//更新刚体位置

        //无敌计时
        if(isInvincible){
        invincibleTimer -= Time.deltaTime;
        if(invincibleTimer < 0){
            isInvincible = false;
        }
    }  


    //按下J建进行攻击
    if(Input.GetKeyDown(KeyCode.J) && curBulletCount > 0){
        ChangeBulletCount(-1);
        anim.SetTrigger("Launch");
        GameObject bullet = Instantiate(bulletPrefab,rbody.position + Vector2.up * 0.5f,Quaternion.identity);//参数分别是对象、位置、方向（默认方向）
        AudioManager.instance.AudioPlay(bulletClip);//播放音效
        BulletController bc = bullet.GetComponent<BulletController>();
        if(bc != null){
            bc.move(lookDirection,300);
            
        }

    }
    //按下E建与npc交互
    if(Input.GetKeyDown(KeyCode.E)){
        RaycastHit2D hit = Physics2D.Raycast(rbody.position,lookDirection,2f,LayerMask.GetMask("NPC"));//layerMask根据名字检测层级
        if(hit.collider != null){
            Debug.Log("hit npc!!");
            NpcManager npc = hit.collider.GetComponent<NpcManager>();
            if(npc != null){
                npc.show();
            }
        }
    }


    }


    public void ChangeHealth(int amount){
        // 伤害是传入负值的，受到伤害，如果是无敌状态，就不进行伤害判定，反之，开启无敌状态
        if(amount < 0){
            if(isInvincible){
                return;
            }
            anim.SetTrigger("Hit");
            AudioManager.instance.AudioPlay(hitClip);//播放音效
            isInvincible = true;
            invincibleTimer = invincibleTime;//重置无敌时间
            
        }

        // 约束一下玩家生命值，0---max
        currentHealth = Math.Clamp(currentHealth + amount ,0,maxHealth);
        UImanager.instance.UpdateHealthBar(currentHealth,maxHealth);//更新血条
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    public void ChangeBulletCount(int amount){
        curBulletCount = Math.Clamp(curBulletCount + amount ,0,maxBulletCount);
        UImanager.instance.UpdateBulletCount(curBulletCount,maxBulletCount);
    }

}

