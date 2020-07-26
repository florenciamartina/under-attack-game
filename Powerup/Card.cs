using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject {
    
    public Sprite icon;
    public string powerupName;
    [TextArea(3, 10)] public string desc;
    public int price;

    public int powerupID;
}
