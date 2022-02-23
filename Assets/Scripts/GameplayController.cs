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

public enum MatchTypes {
  BEST, // best from X games
  WINS // play until X wins
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
  private Text infoText, matchText;

  private GameChoices playerChoice = GameChoices.NONE, opponentChoice = GameChoices.NONE;

  private int gameChoicesAmount = 3, limitValue = 3, playerWins = 0, opponentWins = 0, playedGames = 0;
  private MatchTypes matchType = MatchTypes.BEST;

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

  public void SetGameChoicesAmount(int value) {
    switch(value) {
      case 0:
        gameChoicesAmount = 3;
        break;
      case 1:
        gameChoicesAmount = 5;
        break;
    }
  }

  public void SetMatchType(int value) {
    switch(value) {
      case 0:
        matchType = MatchTypes.BEST;
        break;
      case 1:
        matchType = MatchTypes.WINS;
        break;
    }
  }

  public void SetLimitValue(int value) {
    switch(value) {
      case 0:
        limitValue = 3;
        break;
      case 1:
        limitValue = 5;
        break;
      case 2:
        limitValue = 7;
        break;
    }
  }

  public void clearMatchStatistic() {
    playerWins = 0;
    opponentWins = 0;
    playedGames = 0;
    infoText.text = "";
    if (matchType == MatchTypes.BEST)
    {
      matchText.text = $"Best of {limitValue} games";
    }
    else
    {
      matchText.text = $"Until {limitValue} wins";
    }
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
    int random = Random.Range(0, gameChoicesAmount);

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
        break;
      case 1:
        playerWins += 1;
        break;
      case -1:
        opponentWins += 1;
        break;
    }
    playedGames += 1;

    string gameResult = checkMatchResult();
    StartCoroutine(DisplayWinnerAndRestart(gameResult));
    return;
  }

  string checkMatchResult() {
    if ((matchType == MatchTypes.BEST && playedGames == limitValue) || (matchType == MatchTypes.WINS && (playerWins == limitValue || opponentWins == limitValue)))
    {
      return checkPlayersWins();
    }
    else
    {
      return "None";
    }
  }

  string checkPlayersWins() {
    if (playerWins > opponentWins)
    {
      return "Win";
    }
    else if (playerWins < opponentWins)
    {
      return "Lose";
    }
    else
    {
      return "Draw";
    }
  }

  IEnumerator DisplayWinnerAndRestart(string gameResult) {
    yield return new WaitForSeconds(1f);

    infoText.text = $"{playerWins}-{opponentWins}";
    if (gameResult == "None")
    {
      infoText.text += $", after {playedGames} games";
    }
    else
    {
      infoText.text += $", {gameResult}";
    }

    yield return new WaitForSeconds(1f);
    animationController.ResetAnimations();

    if (gameResult != "None")
    {
      yield return new WaitForSeconds(1f);
      animationController.ShowScreen(0);
    }
  }
}
