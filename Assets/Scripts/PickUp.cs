using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    MeshRenderer[] mrs;
    public GameObject scorePrefab;
    public GameObject particlePrefab;
    GameObject canvas;

    void Start() 
    {
        mrs = this.GetComponentsInChildren<MeshRenderer>();
        canvas = GameObject.Find("Canvas");
    }
    void OnTriggerEnter(Collider col) 
    {
        if(col.gameObject.tag == "Player")
        {
            GameData.singleton.UpdateScore(10);
            PlayerController.sfx[1].Play();
            GameObject scoreText = Instantiate(scorePrefab);
            scoreText.transform.SetParent(canvas.transform);

            Quaternion pEQuaternion = PlayerController.player.transform.rotation;
            pEQuaternion *= Quaternion.Euler(0f, 180f, 0f);

            GameObject pE = Instantiate(particlePrefab, this.transform.position, pEQuaternion);
            Destroy(pE, 1f);

            Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
            scoreText.transform.position = screenPoint;

            foreach(MeshRenderer m in mrs)
                m.enabled = false;
        }
    }

    void OnEnable() 
    {
        if (mrs != null)
            foreach (MeshRenderer m in mrs)  
                m.enabled = true;  
    }
}
