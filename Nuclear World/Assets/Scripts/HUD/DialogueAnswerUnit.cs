using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnswerUnit : MonoBehaviour {
    public string statement;
    public DialogueUnit nextUnit;
    public enum answerType {
        next,
        quest,
        end
    }

    public answerType type;

    public DialogueAnswerUnit() : base() {
        //print("a123");
    }

    public DialogueAnswerUnit(string s) : base() {
        nextUnit = new DialogueUnit();
        nextUnit.statement = s;
        //print("b123");
    }
}
