using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FactBox : MonoBehaviour {

    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private float scrollSpeed = 0.0000002f;

    [SerializeField] private float delayScroll = 5f;
    [SerializeField] private float delayTime = 2f;
    private float currTime = 0f;

    private bool delayed = false;
    [SerializeField] private bool isCredits = false;

    private void Start() {
        currTime = 0f;
    }

    // Update is called once per frame
    void Update() {

        if (isCredits && Input.GetButtonDown("Menu")) {
            SceneManager.LoadScene(0);
        }

        if (currTime >= delayScroll) {
            AutoScroll();
        } else {
            currTime += Time.deltaTime;
        }
    }

    private void AutoScroll() {

        if (scrollbar.value > 0) scrollbar.value -= scrollSpeed;

        if (scrollbar.value <= 0 && !delayed) {
            delayed = true;
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay() {
        yield return new WaitForSeconds(delayTime);
        // Destroy(this.gameObject);
        gameObject.SetActive(false);
    }
}
