using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarateKarrotKontroller : MonoBehaviour
{
    private PotatoJoeController PotatoJoeControllerScript;
    private HealthBarController HealthControllerScript;

    public Animator animator;

    //Attack Rate & Cooldown Timer
    private float ComboCooldown = 3;
    public float ComboTime = 0;
    private bool ComboTimer = false;

    //private float AttackCooldown = 0;
    public float AttackTime = 0;
    //private bool AttackTimer = false;

    public bool Alpha_Carotene = false;
    public bool Beta_Carotene = false;
    public bool Sigma_Carotene = false;

    public bool Vulnerable = false;
    //Idle animations take approximately 0.5 seconds to complete
    //Right 0.04, Left 1.26

    //jab 0.5 or 0.75
    //Slam attack 1.5

    // Start is called before the first frame update
    void Start()
    {
        HealthControllerScript = GameObject.Find("HealthRepresentative").GetComponent<HealthBarController>();
        PotatoJoeControllerScript = GameObject.Find("Potato Joe").GetComponent<PotatoJoeController>();
        
        ComboTimer = true;

        //SlamAttack_True();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);

        if(ComboTimer)
        {
            ComboTime = ComboTime + 1 * Time.deltaTime;

            if(ComboTime >= ComboCooldown)
            {
                ComboTime = 0;
                Karrot_NextCombo();
                ComboTimer = false;
            }
        }

        if(Alpha_Carotene)
        {
            AttackTime = AttackTime + 1 * Time.deltaTime;

            Uppercut_Right_True();

            if(AttackTime >= 0.5f)//0.04
            {
                Uppercut_Right_False();
                Uppercut_Left_True();
            }
            if(AttackTime >= 1)//1.3
            {
                Uppercut_Left_False();
                Uppercut_Right_True();
            }
            if(AttackTime >= 2) //I increased the attack time by 0.63 rather 1.26 (0.63 is half of 1.26) //1.93
            {
                Uppercut_Right_False();
            }
            if(AttackTime >= 2.5f) //2.33
            {
                ComboComplete();
            }
        }

        if(Beta_Carotene)
        {
            AttackTime = AttackTime + 1 * Time.deltaTime;

            Jab_True();

            if(AttackTime >= 1)
            {
                Jab_False();
            }
            if(AttackTime >= 1.1f)
            {
                Uppercut_Left_True();
            }
            if(AttackTime >= 3f) //I just choose three cuz it look nice.. (This makes more sense if you're Gentry Jones)
            {
                Uppercut_Left_False();
            }
            if(AttackTime >= 3.26f)
            {
                ComboComplete();
            }

        }

        if(Sigma_Carotene)
        {
            AttackTime = AttackTime + 1 * Time.deltaTime;

            Uppercut_Left_True();

            if(AttackTime >= 0.5f)
            {
                Uppercut_Left_False();
                Uppercut_Right_True();
            }
            if(AttackTime >= 1.32f)
            {
                Uppercut_Right_False();
                SlamAttack_True();
            }
            if(AttackTime >= 2.8f)
            {
                SlamAttack_False();
            }
            if(AttackTime >= 3.5f)
            {
                ComboComplete();
            }
        }
    }

    public void Karrot_NextCombo()
    {
        float NextCombo = Random.Range(1,3);

        if(NextCombo == 1)
        {
            Beta_Carotene = true;
        }

        if(NextCombo == 2)
        {
            Sigma_Carotene = true;
            //Alpha_Carotene = true;
        }

        if(NextCombo == 3)
        {
            Sigma_Carotene = true;
        }
    }

    public void Karrot_Kollision()
    {
        if(!Vulnerable)
        {
            Block();
            ComboComplete();
        }
        if(Vulnerable)
        {
            HealthControllerScript.BossHealth -= PotatoJoeControllerScript.Damage;
            Hit();
            ComboComplete();
        }
    }

    public void AttackStart()
    {
        
    }

    public void AttackEnd()
    {
        Vulnerable = false;
    }

    public void Weak()
    {
        Vulnerable = true;
    }

    public void Block()
    {
        animator.SetBool("Block", true);
        Invoke("Anti_Block", 0.3f);
    }

    public void Anti_Block()
    {
        animator.SetBool("Block", false);
    }

    public void Hit()
    {
        animator.SetBool("Hit!", true);
        Invoke("Anti_Hit", 0.5f);
    }

    public void Anti_Hit()
    {
        animator.SetBool("Hit!", false);
    }

    public void Uppercut_Right_True()
    {
        animator.SetBool("Uppercut Right", true);
    }
    public void Uppercut_Right_False()
    {
        animator.SetBool("Uppercut Right", false);
    }
    public void Uppercut_Left_True()
    {
        animator.SetBool("Uppercut Left", true);
    }
    public void Uppercut_Left_False()
    {
        animator.SetBool("Uppercut Left", false);
    }
    public void Jab_True()
    {
        animator.SetBool("Jab", true);
    }
    public void Jab_False()
    {
        animator.SetBool("Jab", false);
    }
    public void SlamAttack_True()
    {
        animator.SetBool("Slam Attack", true);
    }
    public void SlamAttack_False()
    {
        animator.SetBool("Slam Attack", false);
    }
    
    public void ComboComplete()
    {
        ComboTimer = true;
        ComboTime = 0;
        AttackTime = 0;
        Alpha_Carotene = false;
        Beta_Carotene = false;
        Sigma_Carotene = false;
        animator.SetBool("Uppercut Right", false);
        animator.SetBool("Uppercut Left", false);
        animator.SetBool("Jab", false);
        animator.SetBool("Slam Attack", false);
    }
}
