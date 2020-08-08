using System;
using UnityEngine;
using UnityEngine.UI;
namespace yhb
{
    [RequireComponent(typeof(Slider))]
    public class TimerUI : MonoBehaviour
    {
        public float WholeTime;
        public float CurrentTime;
        public float SpeedNormal, SpeedSlow;
        private float Speed;
        /// <summary>
        /// 时间耗尽的委托~
        /// </summary>
        public Action OnTimeOut;
        private Slider slider;
        /// <summary>
        /// 时间是否用尽
        /// </summary>
        public bool IsOut = false;
        void Awake()
        {
            slider = GetComponent<Slider>();
        }
        private void Start()
        {
            OnTimeOut += FadeAway;
            Speed = SpeedNormal;
        }
        void Update()
        {
            if (!IsOut)
            {
                if (CurrentTime > 0)
                {
                    if (CurrentTime < WholeTime * 0.2)
                        Speed = SpeedSlow;
                    CurrentTime -= Time.deltaTime * Speed;
                    slider.value = CurrentTime / WholeTime;
                }
                else { OnTimeOut(); }
            }
        }
        /// <summary>
        /// 消失就完事
        /// </summary>
        private void FadeAway()
        {
            this.enabled = false;
            Debug.Log("QTE ends");
        }
    }
}