using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSkin : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> skins = new List<Sprite>();
    [SerializeField] private Text characterName;
    
    public int selectedSkin;
    public void NextOption()
    {
        selectedSkin = selectedSkin + 1;
        if (selectedSkin == skins.Count)
        {
            selectedSkin = 0;
        }
        spriteRenderer.sprite = skins[selectedSkin];
        PlayerPrefs.SetInt("selectedSkin", selectedSkin);
        if (selectedSkin == 0) {
            characterName.text = "Cute Frog";
        }
        else if (selectedSkin == 1)
        {
            characterName.text = "Blue Guy";
        }
        else if (selectedSkin == 2)
        {
            characterName.text = "Pink Guy";
        }
    }

    public void BackOption()
    {
        selectedSkin = selectedSkin - 1;
        if (selectedSkin < 0)
        {
            selectedSkin = skins.Count-1;
        }
        spriteRenderer.sprite = skins[selectedSkin];
        PlayerPrefs.SetInt("selectedSkin", selectedSkin);
        if (selectedSkin == skins.Count)
        {
            selectedSkin = 0;
        }
        spriteRenderer.sprite = skins[selectedSkin];
        if (selectedSkin == 0)
        {
            characterName.text = "Cute Frog";
        }
        else if (selectedSkin == 1)
        {
            characterName.text = "Blue Guy";
        }
        else if (selectedSkin == 2)
        {
            characterName.text = "Pink Guy";
        }
    }

}
