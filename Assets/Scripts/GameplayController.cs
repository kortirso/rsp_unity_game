using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameChoices {
  NONE,
  ROCK,
  PAPER,
  SCISSORS,
  LIZARD,
  SPOCK
}

public class GameplayController : MonoBehaviour
{
  public Dictionary<GameChoices, int> rockResults, paperResults, scissorsResults;
  public Dictionary<GameChoices, Dictionary<GameChoices, int>> gameResults;

  [SerializeField]
  private Sprite rockSprite, scissorsSprite, paperSprite, lizardSprite, spockSprite;

  [SerializeField]
  private Image playerChoiceImage, opponentChoiceImage;

  [SerializeField]
  private Text infoText;

  private GameChoices playerChoice = GameChoices.NONE, opponentChoice = GameChoices.NONE;

  private AnimationController animationController;

  void Awake() {
    animationController = GetComponent<AnimationController>();
  }

  void Start() {
    rockResults = new Dictionary<GameChoices, int>()
    {
      { GameChoices.ROCK, 0 },
      { GameChoices.PAPER, -1 },
      { GameChoices.SCISSORS, 1 },
      { GameChoices.LIZARD, 0 },
      { GameChoices.SPOCK, 0 },
    };

    paperResults = new Dictionary<GameChoices, int>()
    {
      { GameChoices.ROCK, 1 },
      { GameChoices.PAPER, 0 },
      { GameChoices.SCISSORS, -1 },
      { GameChoices.LIZARD, 0 },
      { GameChoices.SPOCK, 0 },
    };

    scissorsResults = new Dictionary<GameChoices, int>()
    {
      { GameChoices.ROCK, -1 },
      { GameChoices.PAPER, 1 },
      { GameChoices.SCISSORS, 0 },
      { GameChoices.LIZARD, 0 },
      { GameChoices.SPOCK, 0 },
    };

    gameResults = new Dictionary<GameChoices, Dictionary<GameChoices, int>>()
    {
      { GameChoices.ROCK, rockResults },
      { GameChoices.PAPER, paperResults },
      { GameChoices.SCISSORS, scissorsResults },
      { GameChoices.LIZARD, rockResults },
      { GameChoices.SPOCK, rockResults },
    };
  }

  public void SetChoice(GameChoices gameChoice) {
    switch(gameChoice) {
      case GameChoices.ROCK:
        playerChoiceImage.sprite = rockSprite;
        playerChoice = GameChoices.ROCK;
        break;
      case GameChoices.SCISSORS:
        playerChoiceImage.sprite = scissorsSprite;
        playerChoice = GameChoices.SCISSORS;
        break;
      case GameChoices.PAPER:
        playerChoiceImage.sprite = paperSprite;
        playerChoice = GameChoices.PAPER;
        break;
      case GameChoices.LIZARD:
        playerChoiceImage.sprite = lizardSprite;
        playerChoice = GameChoices.LIZARD;
        break;
      case GameChoices.SPOCK:
        playerChoiceImage.sprite = spockSprite;
        playerChoice = GameChoices.SPOCK;
        break;
    }

    SetOpponentChoice();
    DetermineWinner();
  }

  void SetOpponentChoice() {
    int random = Random.Range(0, 5);

    switch(random) {
      case 0:
        opponentChoiceImage.sprite = rockSprite;
        opponentChoice = GameChoices.ROCK;
        break;
      case 1:
        opponentChoiceImage.sprite = scissorsSprite;
        opponentChoice = GameChoices.SCISSORS;
        break;
      case 2:
        opponentChoiceImage.sprite = paperSprite;
        opponentChoice = GameChoices.PAPER;
        break;
      case 3:
        opponentChoiceImage.sprite = lizardSprite;
        opponentChoice = GameChoices.LIZARD;
        break;
      case 4:
        opponentChoiceImage.sprite = spockSprite;
        opponentChoice = GameChoices.SPOCK;
        break;
    }
  }

  void DetermineWinner() {
    switch(gameResults[playerChoice][opponentChoice]) {
      case 0:
        infoText.text = "Draw";
        break;
      case 1:
        infoText.text = "Win";
        break;
      case -1:
        infoText.text = "Lose";
        break;
    }

    StartCoroutine(DisplayWinnerAndRestart());
    return;
  }

  IEnumerator DisplayWinnerAndRestart() {
    yield return new WaitForSeconds(2f);
    infoText.gameObject.SetActive(true);
    yield return new WaitForSeconds(2f);
    infoText.gameObject.SetActive(false);
    animationController.ResetAnimations();
  }
}
