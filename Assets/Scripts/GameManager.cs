using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject square;
    public GameObject EndPanel;
    public Text timeTxt;
    public Text NowScore;
    public Text BestScore;

    public Animator anim;

    bool isPlay = true;

    float time = 0.0f;

    string key = "BestScore";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f; //1배속으로 한다는 뜻? 0.1하니까 0.1배로 실행되는듯
        InvokeRepeating("MakeSquare", 0f, 1f ); //InvokeRepeating 반복적으로 실행
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2"); //N2는 소수점 두자리까지 표현
        }    
    }

   

    void MakeSquare()
    {
        Instantiate(square); //()를 생성
    }

    public void GamaOver()
    {
        isPlay = false;
        anim.SetBool("IsDie", true);
        Invoke("TimeStop", 0.5f); //함수 딜레이
        NowScore.text = time.ToString("N2");


        //최고점수가 있다면
        if(PlayerPrefs.HasKey(key))
        {
            float best = PlayerPrefs.GetFloat(key); //불러오기
            // 최고 점수 < 현재 점수
            if (best < time)
            {
                //현재 점수를 최고 점수에 저장한다
                PlayerPrefs.SetFloat(key, time); //저장하기
                BestScore.text = time.ToString("N2"); //이번 점수 소수 둘째자리까지
            }
            else
            {
                BestScore.text = best.ToString("N2"); //최고 점수 소수 둘째자리까지
            }
        }
        else
        {
            PlayerPrefs.SetFloat(key, time); //저장하기
            BestScore.text = time.ToString("N2");
        }
            
            
            //최고 점수 > 현재 점수
            // 아무것도 안한다.
        //최고점수가 없다면
            //현재 점수를 저장한다



        EndPanel.SetActive(true);
    }

    void TimeStop()
    {
        Time.timeScale = 0.0f;
    }
}
