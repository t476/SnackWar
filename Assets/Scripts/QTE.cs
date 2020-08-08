using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
namespace yhb
{
    public class QTE : MonoBehaviour
    {
        public GameObject timer;
        public KeyCode NeededKey;
        public int TargetPress;
        /// <summary>
        /// 期间按下对应按键次数
        /// </summary>
        private int PressTimes = 0;
        /// <summary>
        /// 重叠的两只手
        /// </summary>
        public GameObject doubleHand;
        public float PressCD;
        private float CurTime = 0;
        private void Awake()
        {
            GameObject[] Snacks = GameObject.FindGameObjectsWithTag("snack");
            foreach (GameObject snack in Snacks)
            {
                Destroy(snack);
            }
            //初始化
            doubleHand.SetActive(true);
            PressTimes = 0;
            CurTime = 0;
            timer.SetActive(true);
            timer.GetComponent<TimerUI>().OnTimeOut += End;
        }
        private void Update()
        {
            //检查按键
            if (Input.GetKeyDown(NeededKey) && CurTime > PressCD)
            {
                PressTimes++;
                if (PressTimes >= TargetPress)
                    Sucess();
                doubleHand.transform.Translate(new Vector3(0, -2 * Time.deltaTime, 0));
            }
            else doubleHand.transform.Translate(new Vector3(0, Time.deltaTime, 0));
            //处理CD
            if (CurTime > PressCD)
                CurTime = 0;
            else CurTime += Time.deltaTime;
        }
        /// <summary>
        /// 达到按键目标~
        /// </summary>
        public void Sucess()
        {
            End();
        }
        /// <summary>
        /// 结束调用~
        /// </summary>
        public void End()
        {
            //结束方法 生成零食并计分
            PressTimes = 0;
        }
    }
}