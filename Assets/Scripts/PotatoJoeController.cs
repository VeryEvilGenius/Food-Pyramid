using UnityEngine;

public class PotatoJoeController : MonoBehaviour
{
    private PlayerController PlayerControllerScript;

    public Animator animator;

    // Start is called before the first frame update

    private void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        //This activates Potato Joe's blocking animation
        if (PlayerControllerScript.spriteRenderer.sprite == PlayerControllerScript.BurgerAttack4)
        {
            animator.SetBool("Block", true);
            Invoke("Potato_Anti_Block", 0.5f);
        }
    }

    //This cancels Potato Joe's blocking animation
    private void Potato_Anti_Block()
    {
        animator.SetBool("Block", false);
    }
}
