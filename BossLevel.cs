using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevel : MonoBehaviour {

    [SerializeField] private Boss boss;

    // Start is called before the first frame update
    void Start() {

        boss = GameObject.Find("Boss").GetComponent<Boss>();
        
    }

    // Update is called once per frame
    void Update() {

        if (boss == null) {
            SaveLoad.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}
