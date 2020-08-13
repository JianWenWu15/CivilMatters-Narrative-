using UnityEngine;

// This is a rudimentary 2D movement controller for the example scene.
public class PlayerMove2D : MonoBehaviour
{
    public string idleAnimatorState = "Hero_Idle";
    public string runAnimatorState = "Hero_Run";
    public float speed = 3;
    public GameObject hideWhenUsingPhysics2D;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
#if USE_PHYSICS2D
        if (hideWhenUsingPhysics2D) hideWhenUsingPhysics2D.SetActive(false);
#endif
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var input = new Vector3(horizontal, vertical, 0);
        transform.Translate(speed * Time.deltaTime * input);
        var scaleX = transform.localScale.x;
        var flip = (scaleX < 0 && horizontal > 0) || (scaleX > 0 && horizontal < 0);
        if (flip) transform.localScale = new Vector3(-scaleX, 1, 1);
        animator.Play(input.magnitude > 0.5f ? runAnimatorState : idleAnimatorState);
    }

}
