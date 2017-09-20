using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 startPos;
    public float moveSpeed = 2;
    public float rotationSpeed = 200;
    public Animation anim;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        transform.Translate(0, 0, v * Time.deltaTime * moveSpeed);
        transform.Rotate(0, h * Time.deltaTime * rotationSpeed, 0);

        if (Mathf.Abs(v) > 0.65f)
            anim.Play("run");
        else if (Mathf.Abs(v) > 0.1f)
            anim.Play("walk");
        else
            anim.Play("idle");

        if (Input.GetButtonDown("Fire1"))
            transform.position = startPos;
    }
}
