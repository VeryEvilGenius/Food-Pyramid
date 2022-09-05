using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer sprite_renderer;
    public Sprite Burger_Idle;

    public string direction = "None";
    public float input_counter = 0;

    public bool Idle;

    // Start is called before the first frame update
    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //This code is responsible for the player movement, its purpose is to make the player experience as fluid as possible
        if (Input.GetKeyDown("right") && input_counter == 0)
            {
                direction = "right";
                input_counter = 1;
            }
        if (Input.GetKeyDown("left") && input_counter == 0)
            {
                direction = "left";
                input_counter = 1;
            }
        if (Input.GetKeyDown("up") && input_counter == 0)
            {
                direction = "up";
                input_counter = 1;
            }
        if (sprite_renderer.sprite == Burger_Idle && Input.GetKeyDown("right") == false && Input.GetKeyDown("left") == false && Input.GetKeyDown("up") == false) 
            {
                Idle = true;
                direction = "None";
                input_counter = 0;
            }
        if (sprite_renderer.sprite != Burger_Idle) 
            {
                Idle = false;
            }
        if (direction == "right")
            {
                animator.SetTrigger("Dodge_Right");
            }
        if (direction == "left")
            {
                animator.SetTrigger("Dodge_Left");
            }
        if (direction == "up")
            {
                animator.SetTrigger("Attack");
            }
    }
}
