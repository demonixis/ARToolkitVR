using UnityEngine;

public class AnimateModel : MonoBehaviour
{
    private CharacterController _character = null;
    private Animation _animation;

    public float moveSpeed = 1;
    public float turnSpeed = 150;

    private void Start()
    {
        _character = GetComponent<CharacterController>();
        _animation = GetComponent<Animation>();
    }

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h) + Mathf.Abs(v) > 0.15f)
            _animation.Play("walk");
        else
            _animation.Play("idle");

        transform.Translate(0, 0, v * moveSpeed * Time.deltaTime);
        transform.Rotate(0, h * turnSpeed * Time.deltaTime, 0);
    }
}
