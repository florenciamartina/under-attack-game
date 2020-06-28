using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class CharacterSlot : MonoBehaviour, IPointerClickHandler
{   

    public event Action<Character> OnLeftClickEvent;
    [SerializeField] private Image image;
    [SerializeField] private Sprite emptySlot;

    private Character _character;
    public Character character {
        get { return _character; }
        set {
            _character = value;
            if (_character == null) {
                image.sprite = emptySlot;
                image.enabled = true;
            } else {
                image.sprite = _character.icon;
                image.enabled = true;
            }
        }
    }

    private void OnValidate() {
        if (image == null) {
            image = GetComponent<Image>();
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left) {
            if (OnLeftClickEvent != null) {
                OnLeftClickEvent(character);
            }
        }
    }
}
