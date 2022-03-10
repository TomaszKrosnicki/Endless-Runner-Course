using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateWorld : MonoBehaviour
{
    static public GameObject dummyTraveler;
    static public GameObject lastPlatform;

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    void Awake()
    {
        dummyTraveler = new GameObject("dummy");
    }

    public static void RunDummy()
    {
        GameObject p = Pool.singleton.GetRandom(); // wczytanie objektu z listy
        if (p == null) return;

        if (lastPlatform != null)
        {
            if (lastPlatform.tag == "platformTSection")
            dummyTraveler.transform.position = lastPlatform.transform.position + PlayerController.player.transform.forward * 20f; //przemieszczenie dummy

            else
            dummyTraveler.transform.position = lastPlatform.transform.position + PlayerController.player.transform.forward * 10f; //przemieszczenie dummy

            if (lastPlatform.tag == "stairsUp")
                dummyTraveler.transform.Translate(0f, 5f, 0f);
        }

        lastPlatform = p; // wczytany obiekt = lastPlatform
        p.SetActive(true); // właczenie obiektu
        p.transform.position = dummyTraveler.transform.position; // ustawienie pozycji obiektu
        p.transform.rotation = dummyTraveler.transform.rotation;

        if(p.tag == "stairsDown")
        {
            dummyTraveler.transform.Translate(0f, -5f, 0f);
            p.transform.Rotate(0f, 180f, 0f);
            p.transform.position = dummyTraveler.transform.position;
        }
    }
}
