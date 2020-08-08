using UnityEngine;//生成和移动要分开

public class SnacksMove : MonoBehaviour
{   
    public static SnacksMove _instance;
    //加上零食的移动方式，还有超级零食的产生
    public bool isSuperSnack = false;
    //随机生成零食和它的位置

    //零食移动
    public GameObject snack;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {


    }
    private void Update()
    {

        }


    private void OnCollisionEnter2D(Collision2D collision)//参数是被调用物
    {
        if (collision.gameObject.CompareTag("Player")&&(Input.GetKey(KeyCode.Space)))
        {
            Destroy(gameObject);
            GameManager.Instance.score += 100;
            SnacksCreate._instance.count--;
            
        }
    }


}


    











