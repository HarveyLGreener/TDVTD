using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1;
    [SerializeField] private TextMeshProUGUI player2;
    private int player1Score = 0;
    private int player2Score = 0;
    public int firstTo;
    public bool reloaded = false;
    // Start is called before the first frame update

    public void PhantomPoint()
    {
        player1Score += 1;
        player1.text ="" + player1Score;
        if (player1Score >= firstTo)
        {
            Debug.Log("Phantom Wins!");
            player1Score = 0;
            player2Score = 0;
            reloaded = false;
        }
    }

    public void RattlesPoint()
    {
        player2Score += 1;
        player2.text = "" + player2Score;
        if (player2Score >= firstTo)
        {
            Debug.Log("Rattles Wins!");
            player1Score = 0;
            player2Score = 0;
        }
    }

    private void Start()
    {
        player1.text = "" + player1Score;
        player2.text = "" + player2Score;
    }

    private void Update()
    {
        if (player1Score == 0 && player2Score == 0 && !reloaded)
        {
            player1.text = "" + player1Score;
            player2.text = "" + player2Score;
            reloaded = false;
        }
    }
}
