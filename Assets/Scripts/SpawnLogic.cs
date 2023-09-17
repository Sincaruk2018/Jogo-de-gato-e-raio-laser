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
    private int counter = 0;
    private int difficulty = 0;
    private GameObject[] enemies;
    private int cur, past;
    [SerializeField] private GameObject Ratinho;
    [SerializeField] private GameObject Ratao;
    [SerializeField] private GameObject Atirador;
    void Start()
    {
        past = 0;
        TimerMax = 5f;
        Timer = 0;
        Player = GameObject.Find("GatoProtagonista");
    }


    void Update()
    {   
        
        enemies = GameObject.FindGameObjectsWithTag("enemie");
        cur = enemies.Length;
        if(cur<10 && cur < past)
        {
            Timer= 0;
        }
        past = cur;

        if (counter > 50 && counter<120)
        {
            difficulty = 1;
        }
        else if (counter >= 120 && counter <240)
        {
            difficulty = 2;
        }
        else if (counter>=240)
        {
            difficulty = 3;
        }



        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            setTarget();
            TimerMax = Random.Range(3f-difficulty, 5f-difficulty);

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
            counter++;
        }
    }

    void setTarget()
    {
        Vector2 xBounds = new Vector2(Player.transform.position.x + 8f, Player.transform.position.x - 8f);
        Vector2 yBounds = new Vector2(Player.transform.position.y + 4.5f, Player.transform.position.y - 4.5f);

        Target.Set(Random.Range(xBounds.x, xBounds.y), Random.Range(yBounds.x, yBounds.y),0f);
        if(Vector3.Distance(Target, Player.transform.position) < 3){
            setTarget();
        }
    }
}
