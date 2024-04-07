using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHP : MonoBehaviour
{
    public SpriteRenderer hpSprite;
    public Animator hpAnimation;
    public PlayerMovement hp;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        hpAnimation.speed = 0f;
        float animFrame = Mathf.Abs((hp.hp/3f)-1f);
        if (animFrame == 1)
        {
            hpSprite.enabled = false;
        }
        else
        {
            hpAnimation.Play("Health", 0, animFrame);
        }

    }
}
