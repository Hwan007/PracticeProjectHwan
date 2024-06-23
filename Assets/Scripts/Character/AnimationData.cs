using Define;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimationData {
    public Dictionary<EFsmState, int> AnimatorHash { get; protected set; }
    [SerializeField] List<StateInfo> parametername;

    public void InitializeAnimatorHash() {
        AnimatorHash = new Dictionary<EFsmState, int>();
        foreach (var str in parametername) {
            AnimatorHash.Add(str.state, Animator.StringToHash(str.parameterName));
        }
    }

    public int GetAnimationHash(EFsmState state) {
        return AnimatorHash[state];
    }
}

[Serializable]
public struct StateInfo {
    public string parameterName;
    public EFsmState state;
}