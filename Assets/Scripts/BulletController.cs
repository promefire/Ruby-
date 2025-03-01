using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    Rigidbody2D rbody;
    
    public AudioClip hitEnemyClip;
    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();

        Destroy(this.gameObject,2f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move(Vector2 moveDirection,float moveForce){
        rbody.AddForce(moveDirection * moveForce);//施加一个移动方向的力


    }
    private void OnCollisionEnter2D(Collision2D other) {
        EnemyController ec = other.gameObject.GetComponent<EnemyController>();
        if(ec != null){
            ec.ChangeHealth(-1);
            
        }
        AudioManager.instance.AudioPlay(hitEnemyClip);//播放音效
        Destroy(this.gameObject);//碰到就销毁    
    
    } 


}
