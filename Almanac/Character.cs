using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject {
    
    public string name;
    public string type;
    public Sprite icon; 
    [TextArea(3, 10)] public string description;
}
