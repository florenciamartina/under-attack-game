using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Almanac_WBC : Almanac {
    
    private void Awake() {
        
        if (charactersParent != null) {
            characterSlots = charactersParent.GetComponentsInChildren<CharacterSlot>();
        }

        // int level = SaveLoad.saveManager.GetLevel();
        int level = SaveLoad.GetLevel();
        int count = 0;

        if (level == 1) {
            count = 2;
        } else if (level == 2) {
            count = 3;
        } else if (level == 3) {
            count = 3;
            for (int i = SaveLoad.maxLevel2 + 1; i <= SaveLoad.GetSceneIndex(); i++) {
                count++;
            }
        }

        characters = charactersTemplate.GetRange(0, count);
        if (SaveLoad.GetPowerup(1) == 1) { // if NKC is purchased
            characters.Add(charactersTemplate[6]);
        }
        
        RefreshUI();
    }
}
