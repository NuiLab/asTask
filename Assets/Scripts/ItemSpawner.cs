using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject item;
    public Transform spawnPoint;


    public void Spawn()
    {
        GameObject spawnedItem = Instantiate(item, spawnPoint.position, spawnPoint.rotation);
    }

}
