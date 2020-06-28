using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AlmanacManager : MonoBehaviour
{

    [SerializeField] private Almanac almanac;
    [SerializeField] private CardPanel cardPanel;

    private string unknown = "???";
    private void Awake() {
        almanac.OnCharacterLeftClickedEvent += ShowDesc;
    }

    private void ShowDesc(Character character) {

        if (character == null) {
            cardPanel.characterName.text = unknown;
            cardPanel.characterName.text = unknown;
            cardPanel.characterType.text = unknown;
            cardPanel.characterDesc.text = unknown;
            cardPanel.character.GetComponent<Animator>().SetTrigger("Default");
        } else {
            cardPanel.characterName.text = character.name;
            cardPanel.characterType.text = character.type;
            cardPanel.characterDesc.text = character.description;
            cardPanel.character.GetComponent<Animator>().SetTrigger(character.name);
        }
        
        cardPanel.Show();
    }
}
