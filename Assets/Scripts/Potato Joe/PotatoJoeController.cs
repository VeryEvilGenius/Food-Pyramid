using UnityEngine;

public class PotatoJoeController : MonoBehaviour
{
    private PlayerController PlayerControllerScript;
    private HealthBarController HealthControllerScript;

    public Animator animator;
    
    //The Energy System is what determines how many times Potato Joe can attack before sleeping and how long he does.

    //Energy = How many attacks can Joe perform before resting
    public float Energy = 1;
    public float MaxEnergy = 1;

    //EnergyRecoveryTime = How many hits does it take for Joe to wake up!
    public float EnergyRecoveryTime = 0;
    public float MaxEnergyRecoveryTime = 1;

    //These are just booleans that help manage other variables
    private bool ReEnergize = false;
    private bool Interrupt = false;

    //Recognizes if an attack animation is playing
    public bool AttackRunning = false;

    //Attack Rate & Cooldown Timer
    public float cooldown = 3;
    public float time = 0;
    public bool timer = false;

    //Determines what damage state Joe is in
    public string Maturity = "Ripe";

    //Player Attack damage (Most likely will be moved to the Player Controller later...)
    private float Damage = 10;

    //PLEASE READ: Certain functions are only activated in the animator (ex. AttackStart, AttackEnd)

    // Start is called before the first frame update
    private void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
	    HealthControllerScript = GameObject.Find("HealthRepresentative").GetComponent<HealthBarController>();
        
        animator.SetLayerWeight(animator.GetLayerIndex("Potato"), 1);

        //Origin of Potato Joe's Existence (Remove if you seek his destruction)
        timer = true;
    }

    // Update is called once per frame
    private void Update()
    {
        //This is the cooldown timer that creates the time between Potato Joe's attacks
        if(timer && Energy > 0)
        {
            time = time + 1 * Time.deltaTime;

            if(time >= cooldown)
            {
                time = 0;
                Potato_NextAttack();
                timer = false;
            }
        }
        //Basically a attack cooldown for the player
        if(PlayerControllerScript.spriteRenderer.sprite == PlayerControllerScript.groundBeef)
        {
            ReEnergize = false;
        }

        //When Potato Joe's out of Energy he rests
        if(Energy <= 0)
        {
            animator.SetBool("Stunned", true);
        } else
        {
            animator.SetBool("Stunned", false);
        }
        //Essentially Potato Joe's Wake Up Call for when he's dazed
        if(EnergyRecoveryTime == MaxEnergyRecoveryTime)
        {
            Potato_Steady();
        }

        //As Joe is damaged, his behavior will change
        if(Maturity == "Ripe")
        {
            cooldown = 1.5f;
            MaxEnergy = 1;
            animator.SetFloat("AttackSpeed", 1.0f);
        }
        if(Maturity == "Raw")
        {
            cooldown = 1;
            MaxEnergy = 1;
            animator.SetFloat("AttackSpeed", 1.2f);
        }
        if(Maturity == "Rotten")
        {
            cooldown = 0;
            MaxEnergy = 1;
            animator.SetFloat("AttackSpeed", 1.5f);
        }
    }

    //This little slice of code is what brings Joe to LIFE
    private void Potato_NextAttack()
    {
        float NextAttack = Random.Range(1,3);

        if(NextAttack == 1 && Energy > 0)
        {
            Potato_SingleClaw_Direction();
        }

        if(NextAttack == 2 && Energy > 0)
        {
            Potato_DoubleClaw_Direction();
        }
    }

    //Determines which direction Joe will attack you from (The Random.Range creates a 50% Chance)
    private void Potato_SingleClaw_Direction()
    {
        float PotatoSingleClaw = Random.Range(1,3);

        if(PotatoSingleClaw == 1 && Energy > 0)
        {
            //Single Claw Right
            animator.SetTrigger("Single Claw Right");
        }
        if(PotatoSingleClaw == 2 && Energy > 0)
        {
            //Single Claw Left
            animator.SetTrigger("Single Claw Left");
        }
    }

    private void Potato_DoubleClaw_Direction()
    {
        float PotatoDoubleClaw = Random.Range(1,3);

        if(PotatoDoubleClaw == 1 && Energy > 0)
        {
            //Double Claw Right
            animator.SetTrigger("Double Claw Right");
        }
        if(PotatoDoubleClaw == 2 && Energy > 0)
        {
            //Double Claw Left
            animator.SetTrigger("Double Claw Left");
        }
    }

    //This cancels Potato Joe's blocking animation
    private void Potato_Anti_Block()
    {
        animator.SetBool("Block", false);
    }

    //Returns Potato Joe to his stunned animation after being hit
    private void Potato_Anti_Hit()
    {
        animator.SetBool("Hit!", false);

        if(EnergyRecoveryTime == MaxEnergyRecoveryTime)
        {
            Potato_Steady();
        }
    }

    //Wake up. Wake up.. Wake up...
    private void Potato_Steady()
    {
        animator.SetBool("Stunned", false);
        animator.SetTrigger("Recovery");
        Energy = MaxEnergy;
        EnergyRecoveryTime = 0;
    }

    //This function plays when a attack animation ends
    public void AttackEnd()
    {
        AttackRunning = false;
        timer = true;
    }
    //This function plays when a attack animation starts
    public void AttackStart()
    {
        Interrupt = false;
        AttackRunning = true;
        Energy -= 1;
    }
    
    //This function plays at the start of the stun animation
    public void StunnedStart()
    {
        Interrupt = true;
    }

    //This function is activated if the Player collides with Joe
    public void Potato_Collision()
    {
        //When Joe's attacks are interrupted by the player
        if (AttackRunning && !this.animator.GetCurrentAnimatorStateInfo(1).IsName("Stunned"))
        {
            time = time = -1;
            animator.SetBool("Block", true);
            Invoke("Potato_Anti_Block", 0.3f);
            timer = true;
            //If an attack is interrupted Joe's lost energy will be refunded!
            if(!Interrupt)
            {
                Energy += 1;
            }
            Interrupt = true;
        }
        //Activates Potato Joe's blocking animation
        if (this.animator.GetCurrentAnimatorStateInfo(1).IsName("Idle"))
        {
            time = time = -1f;
            animator.SetBool("Block", true);
            Invoke("Potato_Anti_Block", 0.3f);
        }
        //Activates Potato Joe's Hit Animation, Ouch!
        if (this.animator.GetCurrentAnimatorStateInfo(1).IsName("Stunned"))
        {
            animator.SetBool("Hit!", true);
            Invoke("Potato_Anti_Hit", 0.3f);
            timer = true;

            //If Potato Joe is hit X amount of times while stunned he will wake up. He's a Deep Sleeper!
            if(!ReEnergize)
            {
		        HealthControllerScript.BossHealth -= Damage;
                EnergyRecoveryTime += 1;
                ReEnergize = true;
            }
        }

        //Controls Potato Joe's Damage States
        if (HealthControllerScript.BossHealth > 85) 
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Potato"), 1);
            Maturity = "Ripe";
        }
        if (HealthControllerScript.BossHealth < 85 && HealthControllerScript.BossHealth >= 50) 
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Potato"), -0);
            animator.SetLayerWeight(animator.GetLayerIndex("Potato Hurt"), 1);
            Maturity = "Raw";
        }

        if (HealthControllerScript.BossHealth < 50) 
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Potato Hurt"), -0);
            animator.SetLayerWeight(animator.GetLayerIndex("Potato"), -0);
            animator.SetLayerWeight(animator.GetLayerIndex("Potato Badly Hurt"), 1);
            Maturity = "Rotten";
        }
    }

    //This code is not used at the moment but will be later (This funtion will control what frames Potato Joe can hurt the player on)
    public void Kill_Frame()
    {
        if(PlayerControllerScript.safe == false)
        {
            PlayerControllerScript.GameOver = true;
        }
        if(PlayerControllerScript.safe == true)
        {
            Damage *= 1.2f;
        }
    }
}
