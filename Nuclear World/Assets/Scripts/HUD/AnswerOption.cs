using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerOption : MonoBehaviour {
    public Text text;
    public DialogueAnswerUnit dialogueAnswerUnit;
    public DialoguePanel owner;

    public void SetAnswer(DialogueAnswerUnit answer) {
        dialogueAnswerUnit = answer;
        text.text = answer.statement;
    }

    public void SetText(string state) {
        text.text = state;
    }

    public void OnClick() {
        if (dialogueAnswerUnit.type == DialogueAnswerUnit.answerType.end) {
            owner.Close();
            return;
        }

        if (dialogueAnswerUnit.type == DialogueAnswerUnit.answerType.next) {
            if (dialogueAnswerUnit == null)
                owner.ChangeDialog(null);
            owner.ChangeDialog(dialogueAnswerUnit.nextUnit);
        }
    }
}
