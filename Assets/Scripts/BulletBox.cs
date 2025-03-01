using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBox : MonoBehaviour
{
    public int bulletCount = 10;

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

    void OnTriggerEnter2D(Collider2D other){
        PlayerController pc = other.GetComponent<PlayerController>();
        if(pc != null){
            if(pc.curBulletCount < pc.maxBulletCount){
            pc.ChangeBulletCount(bulletCount);

            Instantiate(collectEffect,transform.position,Quaternion.identity);

            // AudioManager.instance.AudioPlay(collectClip);//播放音效
            Destroy(this.gameObject);
        }
        }
    }
}
