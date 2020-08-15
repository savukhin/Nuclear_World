using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour {
    private NPC npc;
    public GameObject speechText;
    public GameObject optionsPanel;
    public GameObject answerOption_prefab;
    private GameObject[] answerOptions = new GameObject[0];
    public HUDController owner;

    public void SetNPC(NPC state) {
        npc = state;
    }

    public void Close() {
        ClearDialog();
        owner.Close();
    }

    public void DrawDialog(DialogueUnit currentDialogueUnit) {
        speechText.GetComponent<Text>().text = currentDialogueUnit.statement;

        if (currentDialogueUnit.answers.Length == 0) {
            answerOptions = new GameObject[1];
            DialogueAnswerUnit understood = gameObject.AddComponent<DialogueAnswerUnit>();
            understood.statement = "Понял";
            understood.type = DialogueAnswerUnit.answerType.end;
            print(understood.statement);
            print(understood.nextUnit);
            answerOption_prefab.GetComponent<AnswerOption>().SetAnswer(understood);
            answerOptions[0] = Instantiate(answerOption_prefab,
                optionsPanel.transform);

            answerOptions[0].GetComponent<AnswerOption>().owner = this;
            Destroy(understood);
            return;
        
        }

        answerOptions = new GameObject[currentDialogueUnit.answers.Length];
        for (int i = 0; i < currentDialogueUnit.answers.Length; i++)
        {
            answerOption_prefab.GetComponent<AnswerOption>().SetAnswer(currentDialogueUnit.answers[i]);
            answerOptions[i] = Instantiate(answerOption_prefab,
                optionsPanel.transform);

            answerOptions[i].GetComponent<AnswerOption>().owner = this;
        }
    }

    public void ClearDialog() {
        speechText.GetComponent<Text>().text = "";
        foreach (GameObject obj in answerOptions)
            Destroy(obj);
        answerOptions = new GameObject[0];
    }

    public void TakeAnswer(DialogueAnswerUnit answer) {
        print(answer.statement);
    }

    public void ChangeDialog(DialogueUnit currentDialogueUnit) {
        ClearDialog();
        if (currentDialogueUnit == null)
            print("popka");
        DrawDialog(currentDialogueUnit);
    }

    public void Open() {
        gameObject.SetActive(true);
        DialogueUnit currentDialogueUnit = npc.GetDialogueUnit();

        DrawDialog(currentDialogueUnit);
    }
}
