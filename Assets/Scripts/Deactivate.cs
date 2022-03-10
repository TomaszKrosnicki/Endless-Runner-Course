using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    bool dSheduled = false;
    void OnCollisionExit(Collision player) 
    {
        if (PlayerController.isDead) return;
        if (player.gameObject.tag == "Player" && !dSheduled)
        {
        Invoke("SetInactive", 4.0f);
        dSheduled = true;
        }
    }
    void SetInactive()
    {
        if (PlayerController.isDead) return;
        this.gameObject.SetActive(false);
        dSheduled = false;
    }
}
