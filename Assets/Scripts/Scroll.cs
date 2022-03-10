using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] float scrollingSpeed = -0.1f;
    void FixedUpdate() 
    {
        if(PlayerController.isDead) return;
        this.transform.position += PlayerController.player.transform.forward * scrollingSpeed;

        if (PlayerController.currentPlatform == null) return;
        if (PlayerController.currentPlatform.tag == "stairsUp")
            this.transform.Translate(0f, -scrollingSpeed * 0.6f, 0f);
        if (PlayerController.currentPlatform.tag == "stairsDown")
            this.transform.Translate(0f, scrollingSpeed * 0.6f, 0f);
    }
}
