using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }


    public GameObject player;
    //snacks
    public GameObject milk;
    public GameObject spicy;
    public GameObject ice;
    public GameObject rabbit;
    public GameObject durian;
    public GameObject noodles;
    public GameObject chips;
    public GameObject barbecue;
    public GameObject mustard;
    //barriers
    public GameObject tissue;
    public GameObject cup;
    public GameObject remote;
    public GameObject knife;
    public GameObject book;
    //UI
    public GameObject winPrefab;
    public GameObject startCountDownPrefab;
    public GameObject gameoverPrefab;
    public AudioClip startclip;
    public GameObject startPanel;
    public GameObject gamePanel;
    public Text scoreText;
    //计时
    public float timer = 180;
    public Text timeText;


    //要做超级零食和超级玩家吗-坑
    public bool isSuperPlayer = false;
    private List<GameObject> snackGos = new List<GameObject>();
    private int snackNum = 0;


    //score count

    public int score = 0;

    private void Awake()

    {

        _instance = this;





        //把所有零食放在一个列表下，为每隔一段时间生成一个超级零食做准备
        foreach (Transform t in GameObject.Find("Snacks").transform)
        {
            snackGos.Add(t.gameObject);
        }
        snackNum = GameObject.Find("Snacks").transform.childCount;
    }

    //start设置开始时大家都不许动
    private void Start()
    {
        SetGameState(false);
    }

   
    
    //倒计时UI
    IEnumerator PlayStartCountDown()
    {
        GameObject go = Instantiate(startCountDownPrefab);
        yield return new WaitForSeconds(4f);//yield:出产
        Destroy(go);
        SetGameState(true);
        Invoke("CreateSuperSnack", 10f);
       
       
    }


    private void Update()//实时记得分,计时
    {   //计时
        //  if (gamePanel.activeInHierarchy && timer > 0)
        {

            timer -= Time.deltaTime;
            int minutes = (int)timer / 60 - 1;
            minutes = (int)timer / 60;
            float seconds = timer % 60;
            timeText.text = "0" + minutes.ToString("2") + ":" + seconds.ToString("00");

            //有错误

        }

        //当时间到了，结束
        /*else
        {
            gamePanel.SetActive(false);
            //隐藏面板
            Instantiate(winPrefab);//实例化winPrefab，耗内存,显示胜利标语(
            StopAllCoroutines();// 游戏胜利，停止所有携程(
            SetGameState(false);
            SetGameState(false);
        }

        //游戏结束，重载游戏
        if (noweat == pacdotNum)
        {
            if (Input.anyKeyDown)//按下键即可
            {
                SceneManager.LoadScene(0);
            }
        }
        */

        //判断game面板没有被隐藏。更新分数

        if (gamePanel.activeInHierarchy)
        {
            scoreText.text = score.ToString("0000");

        }

    }



    //当吃掉一个零食会有的操作
    public void OnEatSnack(GameObject go)//存个参数好把被吃掉的参数传过来

    {
        score += 100;

        snackGos.Remove(go);//这边在我建的挑幸运零食的list里也把被吃的删掉
    }

    //当吃掉一个超级零食会有的操作
    public void OnEatSuperSnack()

    {
        score += 200;
        Invoke("CreateSuperSnack", 10f);
        isSuperPlayer = true;
        FreezeSnack();
        Invoke("RecoveryBarrier", 3f);

    }


    // 生成超级零食
    private void CreateSuperSnack()
    {
        if (snackGos.Count < 3)
        {
            return;//防止10s和摧毁i两个线程冲突
        }
        int tempIndex = Random.Range(0, snackGos.Count);
        snackGos[tempIndex].transform.localScale = new Vector3(3, 3, 3);
        snackGos[tempIndex].GetComponent<SnacksMove>().isSuperSnack = true;
    }
    //解冻零食的操作
    private void RecoverySnack()
    {
        DisFreezeSnack();
        isSuperPlayer = false;

    }

    //冻结敌人,改到这里了
    private void FreezeSnack()
    {

        SnacksMove._instance.Snacks.transform.GetComponentInChildren<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);


    }
    //解冻敌人
    private void DisFreezeSnack()
    {


        SnacksMove._instance.Snacks.transform.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

    }

    //UI 初始不动
    private void SetGameState(bool state)
    {
        SnacksMove._instance.Snacks.transform.GetComponentInChildren<SnacksMove>().enabled = state;//是有问题的，他身上没挂这个代码

    }
}
    