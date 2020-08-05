
using UnityEngine;

public class Barrier : MonoBehaviour
{ //路障生成
    protected float m_timer = 0;
    protected float run_time = 0;
    public int maxCount = 3;
    private int count = 0;
    public bool isStart;
    public GameObject Barriers;//父物体
    public GameObject barrier;//需要实例化的prefab



    private void Start()
    {
    

    }
    private void Update()
    {//if is start
        //m_timer 小于0就生成敌人
        m_timer -= Time.deltaTime;
        run_time += Time.deltaTime;
        //随机地点时间生成路障，一种路障最多3个吧
        if (m_timer <= 0 && count < maxCount)
        {
         
                m_timer = Random.Range(3, 15);
                GameObject a = Instantiate(barrier, barrier.transform.position, Quaternion.identity);
            //Instantiate(barrier, m_transform.position, Quaternion.identity);这里这个bug记一下：实例化物体而不是prefab
                barrier.transform.position = new Vector2(Random.Range(-9f, 9f),  Random.Range(-5f, 5f));
                a.transform.parent = Barriers.transform;
                count++;
            
        }
       


    }
}
