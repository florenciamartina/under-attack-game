using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SpecialCollectibles : MonoBehaviour
{
    [SerializeField] private Sprite[] collSprites;
    private Image collUI;
    private int index;

    private void Awake() {
        collUI = GetComponent<Image>();
    }
       
    public void Add(int index) {
        collUI.sprite = collSprites[index];
        // Debug.Log(index);
    }
}
