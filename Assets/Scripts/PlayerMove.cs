using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float speed = 0.35f;
    //手下一次移动去的目的地
    private Vector2 dest = Vector2.zero;
    private void Start()
    {//保证手在游戏刚开始的时候不会动
        dest = transform.position;//当前的位置

    }
    private void FixedUpdate()
    {//移动的核心
        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);//插值temporary
        GetComponent<Rigidbody2D>().MovePosition(temp);//方法
        //按键检测且必须先达到上一个dest位置才可以进行新一次目标指令
        if ((Vector2)transform.position == dest)
        {//还不能斜着走
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Valid(Vector2.up))
            {
                dest = (Vector2)transform.position + Vector2.up;
            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Valid(Vector2.down))
            {
                dest = (Vector2)transform.position + Vector2.down;
            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && Valid(Vector2.left))
            {
                dest = (Vector2)transform.position + Vector2.left;
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Valid(Vector2.right))
            {
                dest = (Vector2)transform.position + Vector2.right;
            }//获取移动方向
            Vector2 dir = dest - (Vector2)transform.position;
            //把获取到的移动方向设置给动画状态机
            GetComponent<Animator>().SetFloat("Dirx", dir.x);
            GetComponent<Animator>().SetFloat("Diry", dir.y);
        }


    }
    private bool Valid(Vector2 dir)//射线检测确定还能继续往某个方向走不会撞墙

    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        {
            if (GameManager.Instance.isSuperPlayer)//这个跟那个一模一样为什么这个不行
            {
                if (collision.gameObject.CompareTag("barrier"))
                {

                    Destroy(collision.gameObject);
                    GameManager.Instance.score += 20;
                }


            }


            

        }
    }
}

