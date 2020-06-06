using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectControl : MonoBehaviour
{
    private List<PlayerSelectOption> AllPlayerOptions;

    public void RefreshUI() {
        foreach(PlayerSelectOption obj in AllPlayerOptions) {
            obj.RefreshUI();
        }
    }

    public void RegisterOption(PlayerSelectOption option) {
        if (AllPlayerOptions == null) {
            AllPlayerOptions = new List<PlayerSelectOption>();
        }
        AllPlayerOptions.Add(option);
    }
}
