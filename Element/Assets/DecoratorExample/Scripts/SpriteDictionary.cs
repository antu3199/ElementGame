

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteKVP {
    public Sprite sprite;
    public string key;
}

public class SpriteDictionary : Singleton<SpriteDictionary> {
    public List<SpriteKVP> sprites;

    public Sprite findSprite(string key) {
        for (int i = 0; i < sprites.Count; i++) {
            if (sprites[i].key == key) {
                return sprites[i].sprite;
            }
        }

        return null;
    }
}