using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLogic : MonoBehaviour
{
    private GameObject Player;
    private Vector3 Target;
    private float Timer;
    private float TimerMax;
    private float EnemyRoulette;
    [SerializeField] private GameObject Ratinho;
    [SerializeField] private GameObject Ratao;
    [SerializeField] private GameObject Atirador;
    void Start()
    {
        TimerMax = 5f;
        Timer = 0;
        Player = GameObject.Find("GatoProtagonista");
    }


    void Update()
    {
        if(Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            setTarget();
            TimerMax = Random.Range(4f, 8f);

            EnemyRoulette = Random.Range(0, 100);
            if(EnemyRoulette <=20f)
            {
                Instantiate(Ratao, Target, transform.rotation);
                Timer = TimerMax;
            }

            else if (EnemyRoulette > 20f && EnemyRoulette <=30f)
            {
                Instantiate(Atirador, Target, transform.rotation);
                Timer = TimerMax;
            }

            else if(EnemyRoulette > 30f)
            {
                Instantiate(Ratinho, Target, transform.rotation);
                Timer = TimerMax;
            }
        }
    }

    void setTarget()
    {
        Target.Set(Random.Range(-8f, 8f), Random.Range(-4.5f, 4.5f),0f);
        if(Vector3.Distance(Target, Player.transform.position) < 3){
            setTarget();
        }
    }
}
