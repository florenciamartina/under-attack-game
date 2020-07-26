using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Boss : PlayerMovement {

    private float h_input;
    protected override void HMove() {
        
        h_input = Input.GetAxisRaw("Horizontal");
        if (h_input >= 0) {
            horizontalMove = 1 * runSpeed;  
        } else {
            horizontalMove = h_input * runSpeed;
        }

        if (controller.isTouchingWall()) horizontalMove = 0;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
}