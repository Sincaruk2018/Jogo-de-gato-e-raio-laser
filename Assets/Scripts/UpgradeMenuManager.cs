using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UpgradeMenuManager : MonoBehaviour
{
	[SerializeField] int valuePerLevel;
	
	private int[] price = {1,1,1,1};
	float goldenRatio;
	float aux_power;	
	
	/* Declarando componentes da UI */
	[SerializeField ]public TMPro.TextMeshProUGUI[] priceText = new TMPro.TextMeshProUGUI[4];
	[SerializeField ]public TMPro.TextMeshProUGUI[] statsPrice = new TMPro.TextMeshProUGUI[4];
	[SerializeField] public TMPro.TextMeshProUGUI coinText;
	
	GameObject placeholder;
	
	
	
	
    // Start is called before the first frame update
    void Start()
    {
        goldenRatio = 1.618F;
        if(valuePerLevel <= 0)
        {
			valuePerLevel = 1;
		}
		
		placeholder = GameObject.Find("Info 1");
		priceText[0] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		placeholder = GameObject.Find("Info 2");
		priceText[1] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		
		placeholder = GameObject.Find("Info 3");
		priceText[2] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		placeholder = GameObject.Find("Info 4");
		priceText[3] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		
		placeholder = GameObject.Find("Text power");
		statsPrice[0] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		placeholder = GameObject.Find("Text defense");
		statsPrice[1] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		placeholder = GameObject.Find("Text constitution");
		statsPrice[2] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		placeholder = GameObject.Find("Text luck");
		statsPrice[3] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		
		
		
		priceText[0].text = "Power: " + PlayerStatsManager.Instance.playerFOR.ToString();
		priceText[1].text = "Defense: " + PlayerStatsManager.Instance.playerCON.ToString();
		priceText[2].text = "Agility: " + PlayerStatsManager.Instance.playerAGI.ToString();
		priceText[3].text = "Luck: " + PlayerStatsManager.Instance.playerLCK.ToString();
		
		coinText.text = PlayerStatsManager.Instance.playerMON.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2); 
	}
    
    public void Buy(int id)
    {
		
		/* Se jogador não conseguir comprar, a função falha e feijoada*/
		if(PlayerStatsManager.Instance.playerMON - price[id] < 0)
		{
			Debug.Log("Tocar som de falha");
			return;
		}
		
		PlayerStatsManager.Instance.playerMON -= price[id];
		
		/* Valor do preço = goldenratio ^ vezes que o upgrade foi comprado*/
		switch(id)
		{
			case 0:
				statsPrice[id].text = "Power: " + PlayerStatsManager.Instance.playerFOR;
				PlayerStatsManager.Instance.playerFOR += valuePerLevel;
				aux_power = PlayerStatsManager.Instance.playerFOR/valuePerLevel;
			break;
			case 1:
				statsPrice[id].text = "Defense: " + PlayerStatsManager.Instance.playerCON;
				PlayerStatsManager.Instance.playerCON += valuePerLevel;
				aux_power = PlayerStatsManager.Instance.playerCON/valuePerLevel;
			break;
			case 2:
				statsPrice[id].text = "Agility: " + PlayerStatsManager.Instance.playerAGI;
				PlayerStatsManager.Instance.playerAGI += valuePerLevel;
				aux_power = PlayerStatsManager.Instance.playerAGI/valuePerLevel;
			break;
			case 3:
				statsPrice[id].text = "Luck: " + PlayerStatsManager.Instance.playerLCK;
				PlayerStatsManager.Instance.playerLCK += valuePerLevel;
				aux_power = PlayerStatsManager.Instance.playerLCK/valuePerLevel;
			break;
		}
		
		aux_power = Mathf.Pow(goldenRatio,aux_power);
		price[id] += (int)aux_power;
		
		/* Atualização dos Textos*/
		priceText[id].text = price[id].ToString();
		coinText.text = PlayerStatsManager.Instance.playerMON.ToString();
		return;
	}
}
