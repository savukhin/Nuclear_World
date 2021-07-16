using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboMeleeAttack : MonoBehaviour {
    public MeleeAttack[] attacks;
    //[System.NonSerialized]
    private List<string> m_againstTags;
    public List<string> againstTags {
        get
        {
            return m_againstTags;
        }
        set
        {
            m_againstTags = value;
            foreach (var attack in attacks) {
                attack.againstTags = againstTags;
            }
        }
    }
    private int step = 0;
    private bool busy = false;

    void Start() {
        
    }

    public void Activate() {
        if (busy)
            return;
        busy = true;
        attacks[step].Activate();
        attacks[step].AttackEnded.RemoveAllListeners();
        attacks[step].AttackEnded.AddListener(() => {busy = false;});
        step = (step + 1) % attacks.Length;
    }

    public float GetNextAttackDuration() {
        return attacks[step].duration;
    }
}
