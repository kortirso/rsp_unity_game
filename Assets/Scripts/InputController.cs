using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
  private AnimationController animationController;
  private GameplayController gameplayController;

  private string playersChoice;

  void Awake() {
    animationController = GetComponent<AnimationController>();
    gameplayController = GetComponent<GameplayController>();
  }

  public void StartGameCreation() {
    animationController.ShowScreen(1);
  }

  public void StartGame() {
    gameplayController.clearMatchStatistic();
    animationController.ShowScreen(2);
  }

  public void GetChoice() {
    string choiceName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
    GameChoices selectedChoice = GameChoices.NONE;
    switch(choiceName) {
      case "Rock":
        selectedChoice = GameChoices.ROCK;
        break;
      case "Scissors":
        selectedChoice = GameChoices.SCISSORS;
        break;
      case "Paper":
        selectedChoice = GameChoices.PAPER;
        break;
      case "Lizard":
        selectedChoice = GameChoices.LIZARD;
        break;
      case "Spock":
        selectedChoice = GameChoices.SPOCK;
        break;
    }

    gameplayController.SetChoice(selectedChoice);
    animationController.PlayerMadeChoice();
  }
}
