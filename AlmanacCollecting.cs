using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmanacCollecting : MonoBehaviour
{

    [SerializeField] private Character[] collectedCharacters;
    [SerializeField] private Character[] collectedPathogens;

    [SerializeField] private Almanac almanac;
    [SerializeField] private Almanac almanacPathogen;
    
    // Start is called before the first frame update
    void Start() {
        foreach(Character character in collectedCharacters) {
            almanac.AddCharacter(character);
        }

        foreach(Character pathogen in collectedPathogens) {
            almanacPathogen.AddCharacter(pathogen);
        }
    }
}
