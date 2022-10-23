using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Boss
{
    Potato,
    Carrot
}

public class HealthBarController : MonoBehaviour
{

    private PlayerController PlayerControllerScript;
    private PotatoJoeController PotatoJoeControllerScript;
    private KarateKarrotKontroller KarateKarrotKontrollerScript;

    public Image HealthBar;
    public float BossHealth = 120;
    private float MaxHealth = 120;

    private Boss Opponent = Boss.Carrot;
    
    // Start is called before the first frame update
    void Start()
    {
        HealthBar = GetComponent<Image>();
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        PotatoJoeControllerScript = GameObject.Find("Potato Joe").GetComponent<PotatoJoeController>();
        KarateKarrotKontrollerScript = GameObject.Find("Karate Karrot").GetComponent<KarateKarrotKontroller>();

        MaxHealth = BossHealth;

        if(Opponent == Boss.Potato)
        {
            BossHealth = 120;
        }
        if(Opponent == Boss.Carrot)
        {
            BossHealth = 200;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Displays the current bosses health
        HealthBar.fillAmount = BossHealth / MaxHealth;
    }
}
