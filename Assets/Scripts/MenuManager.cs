using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainCanvas, instructionCanvas;
    
    private void Start() {
        if(mainCanvas != null && instructionCanvas != null){
            mainCanvas.SetActive(true);
            instructionCanvas.SetActive(false);
        }
    }

    public void StartGame(){
        SceneManager.LoadScene(1); // Arrumar pra chamar a cena de upgrades
    }

    public void GoToMenu(){
        SceneManager.LoadScene(0);
    }

    public void ShowInstructions(){
        mainCanvas.SetActive(false);
        instructionCanvas.SetActive(true);
    }

    public void HideInstructions(){
        instructionCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void CloseGame(){
        Application.Quit();
    }
}
