using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
  [SerializeField]
  private Animator playerChoiceHandlerAnimator, choiceAnimator;

  public void ResetAnimations() {
    playerChoiceHandlerAnimator.Play("ShowHandler");
    choiceAnimator.Play("RemoveChoices");
  }

  public void PlayerMadeChoice() {
    playerChoiceHandlerAnimator.Play("RemoveHandler");
    choiceAnimator.Play("ShowChoices");
  }
}
