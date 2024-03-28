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
        Time.timeScale = 1.0f; //1������� �Ѵٴ� ��? 0.1�ϴϱ� 0.1��� ����Ǵµ�
        InvokeRepeating("MakeSquare", 0f, 1f ); //InvokeRepeating �ݺ������� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2"); //N2�� �Ҽ��� ���ڸ����� ǥ��
        }    
    }

   

    void MakeSquare()
    {
        Instantiate(square); //()�� ����
    }

    public void GamaOver()
    {
        isPlay = false;
        anim.SetBool("IsDie", true);
        Invoke("TimeStop", 0.5f); //�Լ� ������
        NowScore.text = time.ToString("N2");


        //�ְ������� �ִٸ�
        if(PlayerPrefs.HasKey(key))
        {
            float best = PlayerPrefs.GetFloat(key); //�ҷ�����
            // �ְ� ���� < ���� ����
            if (best < time)
            {
                //���� ������ �ְ� ������ �����Ѵ�
                PlayerPrefs.SetFloat(key, time); //�����ϱ�
                BestScore.text = time.ToString("N2"); //�̹� ���� �Ҽ� ��°�ڸ�����
            }
            else
            {
                BestScore.text = best.ToString("N2"); //�ְ� ���� �Ҽ� ��°�ڸ�����
            }
        }
        else
        {
            PlayerPrefs.SetFloat(key, time); //�����ϱ�
            BestScore.text = time.ToString("N2");
        }
            
            
            //�ְ� ���� > ���� ����
            // �ƹ��͵� ���Ѵ�.
        //�ְ������� ���ٸ�
            //���� ������ �����Ѵ�



        EndPanel.SetActive(true);
    }

    void TimeStop()
    {
        Time.timeScale = 0.0f;
    }
}
