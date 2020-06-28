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
        characters = level == 0
            ? null
            : charactersTemplate.GetRange(0, 2);
        
        RefreshUI();
    }
}
