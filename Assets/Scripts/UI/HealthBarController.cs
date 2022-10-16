using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{

    private PlayerController PlayerControllerScript;
    private PotatoJoeController PotatoJoeControllerScript;

    public Image HealthBar;
    public float BossHealth = 120;
    private float MaxHealth = 120;
    
    // Start is called before the first frame update
    void Start()
    {
        HealthBar = GetComponent<Image>();
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        PotatoJoeControllerScript = GameObject.Find("Potato Joe").GetComponent<PotatoJoeController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Displays the current bosses health
        HealthBar.fillAmount = BossHealth / MaxHealth;
    }
}
