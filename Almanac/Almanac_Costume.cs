using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Almanac_Costume : Almanac {
    private void Awake() {
        if (charactersParent != null) {
            characterSlots = charactersParent.GetComponentsInChildren<CharacterSlot>();
        }

        characters = new List<Character>();
        for (int i = 0; i < charactersTemplate.Count; i++) {
            if (SaveLoad.GetPowerup(i) == 1) {
                characters.Add(charactersTemplate[i]);
            }
        }

        RefreshUI();
    }
}
