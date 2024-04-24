using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public Transform Destination;
    public GameObject MusicManager;
    public GameObject BossMusic;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = Destination.position;
            MusicManager.SetActive(false);
            BossMusic.SetActive(true);
        }
    }
}
