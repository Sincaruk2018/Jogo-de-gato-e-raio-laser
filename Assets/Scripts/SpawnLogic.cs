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
            if(EnemyRoulette <=10f)
            {
                Instantiate(Ratao, Target, transform.rotation);
                Timer = TimerMax;
            }

            else if (EnemyRoulette > 10f && EnemyRoulette <=20f)
            {
                Instantiate(Atirador, Target, transform.rotation);
                Timer = TimerMax;
            }

            else if(EnemyRoulette > 20f)
            {
                Instantiate(Ratinho, Target, transform.rotation);
                Timer = TimerMax;
            }
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
