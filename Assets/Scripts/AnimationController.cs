using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
  [SerializeField]
  private Animator playerChoiceHandlerAnimator, choiceAnimator, menuCanvasAnimator, gameCreateCanvasAnimator, gameFieldCanvasAnimator;

  [SerializeField]
  private Canvas menuCanvas, gameCreateCanvas, gameFieldCanvas;

  private int currentScreenIndex = 0;

  public void ResetAnimations() {
    playerChoiceHandlerAnimator.Play("ShowHandler");
    choiceAnimator.Play("RemoveChoices");
  }

  public void PlayerMadeChoice() {
    playerChoiceHandlerAnimator.Play("RemoveHandler");
    choiceAnimator.Play("ShowChoices");
  }

  public void ShowScreen(int index) {
    switch(currentScreenIndex) {
      case 0:
        menuCanvasAnimator.Play("HideCanvas");
        menuCanvas.gameObject.SetActive(false);
        break;
      case 1:
        gameCreateCanvasAnimator.Play("HideCanvas");
        gameCreateCanvas.gameObject.SetActive(false);
        break;
      case 2:
        gameFieldCanvasAnimator.Play("HideCanvas");
        gameFieldCanvas.gameObject.SetActive(false);
        break;
    }

    switch(index) {
      case 0:
        menuCanvas.gameObject.SetActive(true);
        menuCanvasAnimator.Play("ShowCanvas");
        break;
      case 1:
        gameCreateCanvas.gameObject.SetActive(true);
        gameCreateCanvasAnimator.Play("ShowCanvas");
        break;
      case 2:
        gameFieldCanvas.gameObject.SetActive(true);
        gameFieldCanvasAnimator.Play("ShowCanvas");
        break;
    }

    currentScreenIndex = index;
  }
}
