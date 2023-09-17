using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    /*
        Controlador de stats
        FOR - Forca - dano do jogador
        CON - Constituicao - vida do jogador
        DES - Destreza - velocidade de ataque do jogador
        SOR - Sorte - chance de crítico do jogador
    */
    private float FOR = 10f, CON = 10f, DES = 10f, SOR = 10f;
    private GameObject[] enemies;

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
    [SerializeField] private float curVida;

    private void Start() {
        maxVida = getVida();
        curVida = maxVida;

        currentLevel = 1;
        levelLimit = 10f;
        xp = 0f;

        ResetWeapon();
        updateHUD();
    }

    private void updateVida(){ maxVida = getVida(); updateHUD(); }

    public void tomarDano(float dano){
        curVida -= dano;
        if(curVida <= 0){
            curVida = 0;
            GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(true);
            GameObject.Find("Spawner").SetActive(false);
            enemies = GameObject.FindGameObjectsWithTag("enemie");
            for(int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }
            gameObject.SetActive(false);  
        }

        updateHUD();
    }

    public void curarVida(float cura){
        curVida += cura;
        if(curVida > maxVida){
            curVida = maxVida;
        }

        updateHUD();
    }

    private float levelLimit, xp;
    public int currentLevel;
    public void AddXP(int amount){
        xp += amount;
        Debug.Log(xp);
        if(xp == levelLimit){
            Debug.Log("level up!");
            LevelUp();
        }

        updateHUD();
    }

    private void LevelUp(){
        currentLevel++;
        xp = 0;

        // TODO ajustar crescimento do xp pro próximo nível
        levelLimit = 10 + (int) Math.Pow(2, currentLevel);
        Debug.Log("Novo level cap:" + levelLimit);
    }

    [SerializeField] private Gun playerGun;
    [SerializeField] private Sprite defaultGunSprite;

    public void SwitchWeapon(Gun gun){
        playerGun.isDefault = false;
        playerGun = gun;

        updateHUD();
    }

    public void ResetWeapon(){
        playerGun.gunSprite = defaultGunSprite;
        playerGun.isDefault = true;
        
        playerGun.bulletsPerShot = 1;
        playerGun.fireRate = 1f;
        //playerGun.projectileCooldown = 1f;
        playerGun.projectileSpeed = 2f;
        playerGun.spread = 0f;

        updateHUD();
    }

    [Header("HUD Settings")]
    [SerializeField] private TextMeshProUGUI healthIndicator;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI coinIndicator;

    //[SerializeField] private TextMeshProUGUI ammoIndicator;
    //[SerializeField] private Image gunIcon;

    public void updateHUD(){
        healthIndicator.text = curVida + " / " + maxVida;
        healthBar.fillAmount = curVida / maxVida;

        coinIndicator.text = xp.ToString("00000");

        //gunIcon.sprite = playerGun.gunSprite;
        //if(playerGun.isDefault) ammoIndicator.text = "\u221E"; // Código do símbolo infinito
        //else ammoIndicator.text = playerGun.ammo.ToString();
    }
}
