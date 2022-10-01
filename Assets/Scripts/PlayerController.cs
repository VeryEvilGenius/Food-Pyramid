using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Sprite burgerIdle;
    public Sprite BurgerAttack4;

    public DirectionEnum Direction = DirectionEnum.None;

    // mutually exclude certain keys when pressed
    public bool movementMutex;

    public bool idle;

    // Start is called before the first frame update
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        // better to query once and store on the stack as a local variable instead of re-querying the engine
        var isRightDown = Input.GetKeyDown("right");
        var isLeftDown = Input.GetKeyDown("left");
        var isUpDown = Input.GetKeyDown("up");

        var isAnimationIdle = spriteRenderer.sprite == burgerIdle;

        // This code is responsible for the player movement, its purpose is to make the player experience as fluid as possible
        // same thing as the original code except it uses a boolean (technically faster but takes up the same amount of space due to technical limitations)
        if (!movementMutex)
        {
            // set the mutex if any of the movement keys are down
            if (isRightDown || isLeftDown || isUpDown) movementMutex = true;
            
            // set the direction conditionally based on key down
            if (isRightDown) Direction = DirectionEnum.Right;
            if (isLeftDown) Direction = DirectionEnum.Left;
            if (isUpDown) Direction = DirectionEnum.Up;
        }

        // check to see if the animation is idle
        if (isAnimationIdle) idle = false;
        // and if no keys are down reset the animation state
        else if (!isRightDown && !isLeftDown && !isUpDown)
        {
            Direction = DirectionEnum.None;
            movementMutex = false;
            idle = true;
        }

        // play animations based on direction enum value
        if (Direction.Hash == 0) return;
        animator.SetTrigger(Direction.Hash);
    }
}
