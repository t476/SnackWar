﻿using System.Collections;
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

        
    }

    private void Start()
    {
        Invoke("CreateSuperSnack", 10f);
    }







    private void Update()//实时记得分,计时
    {
        //把所有零食放在一个列表下，为每隔一段时间生成一个超级零食做准备
        foreach (Transform t in GameObject.Find("Snacks").transform)
        {
            snackGos.Add(t.gameObject);
        }
        snackNum = GameObject.Find("Snacks").transform.childCount;



        //计时
        if (timer > 0)
        {

            timer -= Time.deltaTime;
            int minutes = (int)timer / 60 - 1;
            minutes = (int)timer / 60;
            float seconds = timer % 60;
            timeText.text = "0"+ minutes.ToString() + ":" + seconds.ToString("00");

            scoreText.text = score.ToString("0000");


        }

      
        else
        {

            // Instantiate(winPrefab);//实例化winPrefab，耗内存,显示胜利标语(改
            //StopAllCoroutines();// 游戏胜利，停止所有携程(
            Menu._instance.FinishGame();
            if (Input.anyKeyDown)//按下键即可
            {
                SceneManager.LoadScene(0);
            }
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
        Invoke("RecoverySnack", 3f);

    }


    // 生成超级零食
    private void CreateSuperSnack()
    {
        if (snackGos.Count < 2)
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

 
    private void FreezeSnack()
    {

         GameObject.Find("Snacks").GetComponentInChildren <SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
      //  Debug.Log(GameObject.Find("Snacks").GetComponentInChildren<SpriteRenderer>().color);

    }
    //解冻敌人
    private void DisFreezeSnack()
    {

      
            GameObject.Find("Snacks").GetComponentInChildren<SpriteRenderer>().color =   new Color(1f, 1f, 1f, 1f);

    }

 
}
    