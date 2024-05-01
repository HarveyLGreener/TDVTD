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
    public int player1Score = 0;
    public int player2Score = 0;
    public int firstTo;
    public GameObject phantom;
    public GameObject rattles;
    public GameObject winText;
    public TextMeshProUGUI winner;
    // Start is called before the first frame update

    public void PhantomPoint()
    {
        player1Score += 1;
        player1.text = "" + player1Score;
        rattles.GetComponent<PlayerInput>().SwitchCurrentActionMap("Stage Select");
        if (player1Score >= firstTo)
        {
            StartCoroutine(ResetScoreBoard("Phantom"));
        }
        rattles = null;
    }

    public void RattlesPoint()
    {
        player2Score += 1;
        player2.text = "" + player2Score;
        phantom.GetComponent<PlayerInput>().SwitchCurrentActionMap("Stage Select");
        if (player2Score >= firstTo)
        {
            StartCoroutine(ResetScoreBoard("Rattles"));
        }
        rattles = null;
    }

    private void Start()
    {
        player1.text = "" + player1Score;
        player2.text = "" + player2Score;
    }

    IEnumerator ResetScoreBoard(string player)
    {
        winText.active = true;
        winner.text = player + " wins!";
        yield return new WaitForSeconds(10f);
        player1Score = 0;
        player2Score = 0;
        player1.text = "" + player1Score;
        player2.text = "" + player2Score;
        winText.active = false;
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        rattles = FindObjectOfType<Rattles>().gameObject;
        phantom = FindObjectOfType<Phantom>().gameObject;
    }
}
