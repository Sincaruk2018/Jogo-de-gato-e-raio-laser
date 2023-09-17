using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;
    
    [SerializeField] public float playerFOR;
    [SerializeField] public float playerCON;
    [SerializeField] public float playerAGI;
    [SerializeField] public float playerLCK;
    [SerializeField] public int playerMON;

    private void Awake()
    {
		if (Instance != null)
		{
			Destroy(this.gameObject);
			return;
		}
		
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
