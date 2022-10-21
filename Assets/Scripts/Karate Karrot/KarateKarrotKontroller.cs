using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarateKarrotKontroller : MonoBehaviour
{
    public Animator animator;

    //Attack Rate & Cooldown Timer
    public float cooldown = 3;
    public float time = 0;
    public bool timer = false;

    //Idle animations take approximately 0.5 seconds to complete
    //Uppercut animations take approximately 0.5 seconds to complete

    // Start is called before the first frame update
    void Start()
    {
        timer = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);

        if(timer)
        {
            time = time + 1 * Time.deltaTime;

            if(time >= cooldown)
            {
                time = 0;
                Karrot_NextCombo();
                timer = false;
            }
        }
    }

    public void Karrot_NextCombo()
    {
        float NextCombo = Random.Range(1,3);

        Brutal_Harvest();
    }

    //karate Karrot executes a combo where he performs 3 Uppercuts. (Joke Explanation: The manner in which, Karrot launches his uppercuts is reminisent of the way a carrot would pop out of the ground
    //During a "harvest". But it has a "brutal" twist as the carrot has a thirst for blood, seeking the Player's demise)
    public void Brutal_Harvest()
    {
        Uppercut_Right_True();
        Invoke("Uppercut_Right_False", 0.5f);
        Invoke("Uppercut_Left_True", 0.5f);
        Invoke("Uppercut_Left_False", 1);
        Invoke("Uppercut_Right_True", 1.5f);
        Invoke("Uppercut_Right_False", 2);

        Invoke("ComboComplete", 2.5f);
    }

    public void Karrot_Kollision()
    {
        //Coming Soon
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
    
    public void ComboComplete()
    {
        timer = true;
    }
}
