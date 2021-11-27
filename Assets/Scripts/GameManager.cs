using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Text contador;
    [SerializeField] GameObject canvaWin;
    [SerializeField] GameObject canvaLose;
    [SerializeField] EnemieTorret torret;
   

    public GameObject [] enemies;
    float d;
    // Start is called before the first frame update
    private void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        d = enemies.LongLength;
    }
    void Start()
    {
        canvaWin.SetActive(false);
        canvaLose.SetActive(false);

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        GameOver();
        Contador();        
    }
    void GameOver()
    {
        if(player.gameOver == true)
        {
            player.gamePaused = true;
            Time.timeScale = 0;           
            canvaLose.SetActive(true);
        }
      
    }

    void Contador()
    {
       
        contador.text = d.ToString();
        if(contador.text == "0")
        {
            Ganar();
        }
        
    }
    void Ganar()
    {
        
        canvaWin.SetActive(true);
        player.gamePaused = true;
        Time.timeScale = 0;
          
        
    }
    public void Restar()
    {
        d -= 1;
    }
    public void Reiniciar()
    {

        player.gamePaused = false;
        Time.timeScale = 1; 
        SceneManager.LoadScene(0);
        

    }
}
