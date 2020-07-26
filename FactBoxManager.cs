using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactBoxManager : MonoBehaviour {

    [SerializeField] private Transform factBoxParent;
    [SerializeField] private FactBox[] factBoxes;
    [SerializeField] private float factInterval = 3f;

    private int i = 0;

    // Start is called before the first frame update
    void Start() {
        factBoxes = factBoxParent.GetComponentsInChildren<FactBox>();
        foreach (FactBox factBox in factBoxes) {
            factBox.gameObject.SetActive(false);
        }

        factBoxes[0].gameObject.SetActive(true);
        i++;
    }

    // Update is called once per frame
    void Update() {
        if (i >= factBoxes.Length) return;
        StartCoroutine(NextFact());
    }

    private IEnumerator FirstFact() {

        yield return new WaitForSeconds(factInterval);

        factBoxes[0].gameObject.SetActive(true);
        i++;
    }

    private IEnumerator NextFact() {
        if (!factBoxes[i - 1].gameObject.activeSelf) {
            Destroy(factBoxes[i - 1].gameObject);
            
            yield return new WaitForSeconds(factInterval);

            if (i < factBoxes.Length) factBoxes[i].gameObject.SetActive(true);
            i++;
        }
    }
}
