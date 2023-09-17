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
	int FORQNT, DEFQNT, AGIQNT, LCKQNT;
	
	/* Declarando componentes da UI */
	[SerializeField ]public TMPro.TextMeshProUGUI[] priceText = new TMPro.TextMeshProUGUI[4];
	[SerializeField ]public TMPro.TextMeshProUGUI[] statsText = new TMPro.TextMeshProUGUI[4];
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

		FORQNT = (int)Mathf.Floor((PlayerStatsManager.Instance.playerFOR / valuePerLevel));
		DEFQNT = (int)Mathf.Floor((PlayerStatsManager.Instance.playerCON / valuePerLevel));
		AGIQNT = (int)Mathf.Floor((PlayerStatsManager.Instance.playerAGI / valuePerLevel));
		LCKQNT = (int)Mathf.Floor((PlayerStatsManager.Instance.playerLCK / valuePerLevel));
        if (FORQNT > 1)
        {
			for (int i = 1; i <= FORQNT; i++)
			{
				price[0] += (int)Mathf.Floor(Mathf.Pow(goldenRatio, i));
			}
			price[0]--;
		}
        if (DEFQNT > 1)
        {
			for (int i = 1; i <= DEFQNT; i++)
			{
				price[1] += (int)Mathf.Floor(Mathf.Pow(goldenRatio, i));
			}
			price[1]--;
		}
        if (AGIQNT > 1)
        {
			for (int i = 1; i <= AGIQNT; i++)
			{
				price[2] += (int)Mathf.Floor(Mathf.Pow(goldenRatio, i));
			}
			price[2]--;
		}
        if(LCKQNT > 1)
        {
			for (int i = 1; i <= LCKQNT; i++)
			{
				price[3] += (int)Mathf.Floor(Mathf.Pow(goldenRatio, i));
			}
			price[3]--;
		}
		
		
		
		





		placeholder = GameObject.Find("Info 1");
		statsText[0] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		placeholder = GameObject.Find("Info 2");
		statsText[1] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		
		placeholder = GameObject.Find("Info 3");
		statsText[2] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		placeholder = GameObject.Find("Info 4");
		statsText[3] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		
		placeholder = GameObject.Find("Text power");
		priceText[0] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		placeholder = GameObject.Find("Text defense");
		priceText[1] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		placeholder = GameObject.Find("Text constitution");
		priceText[2] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();
		
		placeholder = GameObject.Find("Text luck");
		priceText[3] = placeholder.GetComponent<TMPro.TextMeshProUGUI>();




		statsText[0].text = "Power: " + PlayerStatsManager.Instance.playerFOR.ToString();
		statsText[1].text = "Defense: " + PlayerStatsManager.Instance.playerCON.ToString();
		statsText[2].text = "Agility: " + PlayerStatsManager.Instance.playerAGI.ToString();
		statsText[3].text = "Luck: " + PlayerStatsManager.Instance.playerLCK.ToString();
        priceText[0].text = price[0].ToString("00000");
		priceText[1].text = price[1].ToString("00000");
		priceText[2].text = price[2].ToString("00000");
		priceText[3].text = price[3].ToString("00000");

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
				PlayerStatsManager.Instance.playerFOR += valuePerLevel;
				statsText[id].text = "Power: " + PlayerStatsManager.Instance.playerFOR;
				aux_power = (int)PlayerStatsManager.Instance.playerFOR/valuePerLevel;
			break;
			case 1:
				PlayerStatsManager.Instance.playerCON += valuePerLevel;
				statsText[id].text = "Defense: " + PlayerStatsManager.Instance.playerCON;
				aux_power = (int)PlayerStatsManager.Instance.playerCON/valuePerLevel;
			break;
			case 2:
				PlayerStatsManager.Instance.playerAGI += valuePerLevel;
				statsText[id].text = "Agility: " + PlayerStatsManager.Instance.playerAGI;
				aux_power = (int)PlayerStatsManager.Instance.playerAGI/valuePerLevel;
			break;
			case 3:
				PlayerStatsManager.Instance.playerLCK += valuePerLevel;
				statsText[id].text = "Luck: " + PlayerStatsManager.Instance.playerLCK;
				aux_power = (int)PlayerStatsManager.Instance.playerLCK/valuePerLevel;
			break;
		}
		
		aux_power = Mathf.Floor(Mathf.Pow(goldenRatio,aux_power));
		price[id] += (int)aux_power;
		
		/* Atualização dos Textos*/
		priceText[id].text = price[id].ToString("00000");
		coinText.text = PlayerStatsManager.Instance.playerMON.ToString();
		return;
	}
}
