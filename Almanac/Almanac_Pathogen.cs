using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Almanac_Pathogen : Almanac {
    private void Awake() {
        
        if (charactersParent != null) {
            characterSlots = charactersParent.GetComponentsInChildren<CharacterSlot>();
        }

        // int level = SaveLoad.saveManager.GetLevel();
        int level = SaveLoad.GetLevel();
        characters = level == 0
            ? null
            : level == 1
                ? (SaveLoad.GetSceneIndex() < SaveLoad.maxLevel1 - 2 
                    ? charactersTemplate.GetRange(0, 2) 
                    : charactersTemplate.GetRange(0, 3))
                : level == 2
                    ? (SaveLoad.GetSceneIndex() < SaveLoad.maxLevel2 - 2
                        ? charactersTemplate.GetRange(0, 4)
                        : charactersTemplate.GetRange(0, 5))
                    : characters = charactersTemplate;

        // characters = charactersTemplate;
        
        RefreshUI();
    }
}
