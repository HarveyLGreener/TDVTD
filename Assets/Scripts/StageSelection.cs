using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StageSelection : MonoBehaviour
{
    public GameObject[] stages;
    public int index = 0;
    public bool selected = false;
    public int indexAdder = 0;
    public bool pressed = false;
    public Gamepad loser;
    public void ChangeStageSelectionRight(InputAction.CallbackContext context)
    {

            pressed = context.action.triggered;
            if (pressed)
            {
            indexAdder++;
        }
    }
    public void ChangeStageSelectionLeft(InputAction.CallbackContext context)
    {
        pressed = context.action.triggered;
        if (pressed)
        {
            indexAdder--;
        }
    }
    public void Select(InputAction.CallbackContext context)
    {
        selected = context.action.triggered;
    }


    // Update is called once per frame
    void Update()
    {
        index += indexAdder;
        if (index < 0)
        {
            index = stages.Length-1;
        }
        else if (index > stages.Length-1)
        {
            index = 0;
        }

        foreach (GameObject stage in stages)
        {
            if (stage != stages[index])
            {
                stage.GetComponent<SpriteRenderer>().color = Color.grey;
                stage.gameObject.transform.localScale = Vector3.one;
            }
            else
            {
                stage.GetComponent<SpriteRenderer>().color = Color.white;
                stage.gameObject.transform.localScale = Vector3.one * 1.5f;
            }
        }

        if (selected)
        {
            SceneManager.LoadScene(index);
        }
    }

    private void LateUpdate()
    {
        indexAdder = 0;
        pressed = false;
        selected = false;
    }
}
