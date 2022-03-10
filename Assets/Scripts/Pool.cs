using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Daje dostep z poziomu Unity
public class PoolItem
{
    public GameObject prefab; // Polaczenie z prefabrykatem
    public int amount; // ilosc podanych prefabrykatow
    public bool expandable; // czy mozemy zwiekszac ilosc?
}
public class Pool : MonoBehaviour
{
    public static Pool singleton; // Polaczenie z Pool("torba z prefabrykatami") / możliwość odnoszenia się do posiadanych obiektów
    public List<PoolItem> items; // Lista
    public List<GameObject> pooledItems; // Lista obiektów z której wyciągamy obiekty i odkładamy spowrotem gdy nie używamy

    void Awake() // Wywołane przed Start()
    {
        singleton = this; // odniesienie się do obecnej klasy Pool
        pooledItems = new List<GameObject>(); // utworzenie listy pooledItems
        foreach (PoolItem item in items)  // Dla każdego obiektu w liscie obiektow "PoolItem"...
        {
            for (int i = 0; i < item.amount; i++) // ... zostana utworzone kopie danego obiektu rowne podanej ilosci
            {
                GameObject obj = Instantiate(item.prefab); // wygenerowanie obiektu
                obj.SetActive(false); // ustawienie obiektu na nieaktywny
                pooledItems.Add(obj); // dodanie do listy pooledItems
            }
        }
    }

    public GameObject GetRandom() // Funkcja zwracajaca losowe obiekty z listy
    {
        Utils.Shuffle(pooledItems); // Losowanie listy
        for (int i = 0; i < pooledItems.Count; i++) // petla o dlugosci rownej liscie
        {
            if (!pooledItems[i].activeInHierarchy) // jesli obiekt nie jest aktywny w hierarchi...
            {
                return pooledItems[i]; // ... zwróć wylosowany obiekt
            }
        }

        foreach (PoolItem item in items) // Dla każdego obiektu w liscie obiektow "PoolItem"...
        {
            if (item.expandable) // ...sprawdź czy można zwiększyć ilość jego kopii
            {
                GameObject obj = Instantiate(item.prefab); // utworzenie obiektu
                obj.SetActive(false); // wylączenie obiektu
                pooledItems.Add(obj); // dodanie do listy
                return obj; // zrócenie utworzonego obiektu
            }
        }

        return null; // w przeciwnym wypadku niż powyższe, zwróć NIC
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public static class Utils // klasa przechowująca matematyczne funkcje
{
    public static System.Random r = new System.Random(); // utworzenie losowej wartosci
    public static void Shuffle<T>(this IList<T> list) // Funkcja losująca listę
    {
        int n = list.Count; // zmienna n równa długości listy
        while (n > 1) // dopuki n jest większe od 1 jest wykonywana pętla
        {
            n--; // przesuwamy się od góry do dołu
            int k = r.Next(n + 1); // losowy index listy
            // Zamiana wartości pozycjami
            T value = list[k]; 
            list[k] = list[n];
            list[n] = value;
        }
    }
}
