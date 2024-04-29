using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1;
    [SerializeField] private TextMeshProUGUI player2;
    private int player1Score = 0;
    private int player2Score = 0;
    public int firstTo;
    public GameObject phantom;
    public GameObject rattles;
    // Start is called before the first frame update

    public void PhantomPoint()
    {
        player1Score += 1;
        player1.text ="" + player1Score;
        rattles.GetComponent<PlayerInput>().SwitchCurrentActionMap("Stage Select");
        if (player1Score >= firstTo)
        {
            Debug.Log("Phantom Wins!");
            player1Score = 0;
            player2Score = 0;
            StartCoroutine(ResetScoreBoard());
        }
    }

    public void RattlesPoint()
    {
        player2Score += 1;
        player2.text = "" + player2Score;
        phantom.GetComponent<PlayerInput>().SwitchCurrentActionMap("Stage Select");
        if (player2Score >= firstTo)
        {
            Debug.Log("Rattles Wins!");
            player1Score = 0;
            player2Score = 0;
            StartCoroutine(ResetScoreBoard());
        }
    }

    private void Start()
    {
        player1.text = "" + player1Score;
        player2.text = "" + player2Score;
    }

    IEnumerator ResetScoreBoard()
    {
        yield return new WaitForSeconds(5f);
        player1.text = "" + player1Score;
        player2.text = "" + player2Score;
    }

    private void Update()
    {
        if (rattles == null)
        {
            rattles = FindObjectOfType<Rattles>().gameObject;
        }
        if (phantom == null)
        {
            phantom = FindObjectOfType<Phantom>().gameObject;
        }
    }
}
