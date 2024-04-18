using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1;
    [SerializeField] private TextMeshProUGUI player2;
    private int player1Score = 0;
    private int player2Score = 0;
    // Start is called before the first frame update

    public void PhantomPoint()
    {
        player1Score += 1;
        player1.text ="" + player1Score;
        if (player1Score >= 3)
        {
            Debug.Log("Phantom Wins!");
        }
    }

    public void RattlesPoint()
    {
        player2Score += 1;
        player2.text = "" + player2Score;
        if (player2Score >= 3)
        {
            Debug.Log("Rattles Wins!");
        }
    }

    private void Start()
    {
        player1.text = "" + player1Score;
        player2.text = "" + player2Score;
    }
}
