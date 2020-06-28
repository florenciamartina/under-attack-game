using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Raft : MonoBehaviour
{

    private int nextLevel;
    private int currentSceneIndex;

    [Header("Sound FX")]
    [SerializeField] private AudioSource raftSound;
    private GameObject specColl;

    // Start is called before the first frame update
    private void Start()
    {
        specColl = GameObject.Find("SpecialCollectible");
        nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (specColl == null) {
                raftSound.Play();
                //SaveLoad.saveManager.Save();
                SaveLoad.Save();
                SceneManager.LoadScene(nextLevel);
            } else {
                Debug.Log("Get Special Collectible first!");
            }
        }
    }

   


}
