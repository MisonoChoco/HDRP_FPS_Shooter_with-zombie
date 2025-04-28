using UnityEngine;
using Fusion;
using static Unity.Collections.Unicode;

public class FusionPlayerMovement : NetworkBehaviour
{
    public CharacterController controller;
    public float speed = 5f;

    public override void FixedUpdateNetwork()
    {
        //base.FixedUpdateNetwork();
        if (!Object.HasInputAuthority) return;
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0, vertical);
        controller.Move(move * speed * Runner.DeltaTime);
    }
}