using UnityEngine;

public class SnacksMove : MonoBehaviour
{
    public static SnacksMove _instance;
    //加上零食的移动方式，还有超级零食的产生
    public bool isSuperSnack = false;
    //随机生成零食和它的位置
    protected float m2_timer = 0;
    protected float run2_time = 0;
    public int maxCount = 3;
    private int count = 0;
    public bool isStart;


    //零食移动
    public float force;
    public float moveSpeed;
    public GameObject Snacks;//父物体
    public GameObject snack;//需要实例化的prefab
    private GameObject a;//实例化后的物体

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {


    }
    private void Update()
    {//if is start
        //m_timer 小于0就生成敌人
        m2_timer -= Time.deltaTime;
        run2_time += Time.deltaTime;
        //随机时间地点生成,一种最多3个吧
        if (m2_timer <= 0 && count < maxCount)
        {

            m2_timer = Random.Range(3, 15);
             a = Instantiate(snack, snack.transform.position, Quaternion.identity);
            //Instantiate(barrier, m_transform.position, Quaternion.identity);这里这个bug记一下：实例化物体而不是prefab
            snack.transform.position = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 5f));
            a.transform.parent = Snacks.transform;
            count++;

        }



        //开始运动
         a.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);//是a不是snack注意

        //用overlap方法检测碰撞
        Collider2D col;
        //方法会返回一个collider
        col = Physics2D.OverlapBox(transform.position,new Vector2(1.5f,1.5f),0);
        if(col!=null)
        {
            Debug.Log(col.gameObject.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireCube(transform.position, new Vector3(1.5f, 1.5f, 0));
    }
    
    
    /* private void OnTriggerEnter2D(Collider2D collision)
     {
         if (collision.gameObject.name == "player")
         {

             if (isSuperSnack)
             {
                 GameManager.Instance.OnEatSnack(gameObject);
                 GameManager.Instance.OnEatSuperSnack();
                 Destroy(gameObject);
             }
             else
             {
                 GameManager.Instance.OnEatSnack(gameObject);
                 Destroy(gameObject);
             }
             
   


        }
}*/





}




