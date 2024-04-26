using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StageSelection : MonoBehaviour
{
    public GameObject[] stages;
    public int index = 0;
    public Vector2 direction;
    public bool selected = false;
    public void ChangeStageSelection(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void Select(InputAction.CallbackContext context)
    {
        selected = context.action.triggered;
    }


    // Update is called once per frame
    void Update()
    {
        if (direction.x < 0)
        {
            index--;
        }
        else if (direction.x > 0)
        {
            index++;
        }

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

        
    }

    private void LateUpdate()
    {
        direction = Vector2.zero;
    }
}
