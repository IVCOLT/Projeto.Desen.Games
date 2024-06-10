using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController gc; 
    public Text coinsText;
    public int coins;

    void Awake()
    {
        if(gc == null )
        {
            gc = this ;
        }
        else if (gc != this)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateCoinsText()
    {
        coinsText.text = coins.ToString();
    }
}
