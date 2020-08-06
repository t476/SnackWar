using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnacksCreate : MonoBehaviour
{
    public static SnacksCreate _instance;
    protected float m2_timer = 0;
    protected float run2_time = 0;
    public int maxCount = 3;
    public int count = 0;
    public bool isStart;
    public float force;
    public float moveSpeed;
    public GameObject Snacks;//父物体
    public GameObject snack;//需要实例化的prefab
    private GameObject a;//实例化后的物体
    private void Awake()
    {
        _instance = this;
    }
    private void Update()
    {
        //if is start
        //m_timer 小于0就生成敌人
        m2_timer -= Time.deltaTime;
        run2_time += Time.deltaTime;
        //随机时间地点生成,一种最多3个吧
        if (m2_timer <= 0 && count < maxCount)
        {

            m2_timer = Random.Range(3, 6);
            //transform.rotation = Quaternion.Euler(Random.Range(0, 90), 0, 0);//s什么鬼，鬼畜极了
            a = Instantiate(snack, snack.transform.position, Quaternion.identity);
            //Instantiate(barrier, m_transform.position, Quaternion.identity);这里这个bug记一下：实例化物体而不是prefab，底下这个是保持角度四个0
            snack.transform.position = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 5f));
            a.transform.parent = Snacks.transform;
            count++;
        }

        //开始运动
        a.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);//是a不是snack注意
    }
}