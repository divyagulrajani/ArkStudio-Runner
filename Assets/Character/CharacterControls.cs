using UnityEngine;

public class CharacterControls : MonoBehaviour
{

    [SerializeField] LayerMask groundLayers;
    [SerializeField] private float speed = 15;
    [SerializeField] private float force = 200;
    [SerializeField] Rigidbody rigidBody; 

    private float horizontalInput;

    public void FixedUpdate()
    {
        // constant forward movement
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        // movement on x-axis
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime; 
        // moving the player
        rigidBody.MovePosition(rigidBody.position + forwardMove + horizontalMove);

        // Downward force
        rigidBody.AddForce(new Vector3(-transform.up.x, -transform.up.y, -transform.up.z) * force, ForceMode.Force);
        
    }

    public void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }
}
