using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /*
        Controlador de stats
        FOR - Forca - dano do jogador
        CON - Constituicao - vida do jogador
        DES - Destreza - velocidade de ataque do jogador
        SOR - Sorte - chance de crítico do jogador
    */
    [SerializeField] private float FOR = 10f, CON = 10f, DES = 10f, SOR = 10f;

    // Não sei se precisa disso kek
    public float getFOR(){ return this.FOR; }
    public float getCON(){ return this.CON; }
    public float getDES(){ return this.DES; }
    public float getSOR(){ return this.SOR; }


    public void upgradeFOR(float amount){ this.FOR += amount; }
    public void upgradeCON(float amount){ this.CON += amount; }
    public void upgradeDES(float amount){ this.DES += amount; }
    public void upgradeSOR(float amount){ this.SOR += amount; }

    // TODO refinar as equações dos stats
    // Talvez salvar esses valores em variável (ou lambda?)
    public float getDano(){ return this.FOR / 10f; }
    public float getVida(){ return this.CON * 5f; }
    public float getAttackSpeed(){ return 10f / this.DES; }
    public float getCriticalPercentage(){ return this.SOR / 100f; }

    private float maxVida;
    private float curVida;

    private void Start() {
        maxVida = getVida();
        curVida = maxVida;
    }

    private void updateVida(){ maxVida = getVida(); }

    public void tomarDano(float dano){
        curVida -= dano;
        if(curVida <= 0){
            curVida = 0;
            // TODO Game Over / Restart
        }
    }

    public void curarVida(float cura){
        curVida += cura;
        if(curVida > maxVida){
            curVida = maxVida;
        }
    }
}
