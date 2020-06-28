using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public abstract class Almanac : MonoBehaviour {
    [SerializeField] protected List<Character> charactersTemplate;
    protected List<Character> characters;
    [SerializeField] protected Transform charactersParent;
    [SerializeField] protected CharacterSlot[] characterSlots;

    public event Action<Character> OnCharacterLeftClickedEvent;

    private void Start() {
        for (int i = 0; i < characterSlots.Length; i++) {
            characterSlots[i].OnLeftClickEvent += OnCharacterLeftClickedEvent;
        }
    }

    // private void OnValidate() {
    //     if (charactersParent != null) {
    //         characterSlots = charactersParent.GetComponentsInChildren<CharacterSlot>();
    //     }

    //     RefreshUI();
    // }

    private void Update() {
        if (characters == null) {
            Debug.Log("null");
        }
    }

    protected void RefreshUI() {
        
        int i = 0;
        // for (; i < characters.Count && i < characterSlots.Length; i++) {
        //     characterSlots[i].character = characters[i];
        // }

        if (characters != null) {
            foreach (Character character in characters) {
                characterSlots[i].character = character;
                i++;
            }
        }

        for (; i < characterSlots.Length; i++) {
            characterSlots[i].character = null;
        }
    }

    public void AddCharacter(Character character) {
        characters.Add(character);
        RefreshUI();
    }
}
