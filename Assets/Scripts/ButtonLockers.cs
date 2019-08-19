using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLockers : MonoBehaviour
{
    [SerializeField]
    private Button[] mazeButtons;

    public static bool isButtonLocked = true;

    void Start()
    {
        DisableButtons();
    }

    private void DisableButtons()
    {
        for (int i = 0; i < mazeButtons.Length; i++)
        {
            mazeButtons[i].interactable = false;
        }
    }

    public void EnableButoons()
    {
        isButtonLocked = false;

        for (int i = 0; i < mazeButtons.Length; i++)
        {
            mazeButtons[i].interactable = true;
        }
    }
}
