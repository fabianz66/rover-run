using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCarOptionsFactory : MonoBehaviour
{
    public static string DEFAULT_PLAYER_SPRITE = "carro_perico";
    public GameObject prefab;
    public Text StarsCountTxt;
    private List<PlayerSelectOption> InScreenOptions = new List<PlayerSelectOption>();

    public void Start()
    {              
        List <OptionDesc> optionDescriptions = new List<OptionDesc>();
        optionDescriptions.Add(new OptionDesc("carro_perico", 150));
        optionDescriptions.Add(new OptionDesc("carro_lr_amarillo", 150));
        optionDescriptions.Add(new OptionDesc("carro_lr_azul", 150));
        optionDescriptions.Add(new OptionDesc("carro_lr_rojo", 150));
        optionDescriptions.Add(new OptionDesc("carro_lr_negro", 150));
        optionDescriptions.Add(new OptionDesc("carro_lr_rosa", 150));
        optionDescriptions.Add(new OptionDesc("carro_lr_camuflado", 300));
        optionDescriptions.Add(new OptionDesc("carro_lr_flamas", 300));
        optionDescriptions.Add(new OptionDesc("carro_lr_sarchi", 300));       

        foreach (OptionDesc desc in optionDescriptions) {
            GameObject newObj = (GameObject)Instantiate(prefab, transform);
            PlayerSelectOption opt = newObj.GetComponent<PlayerSelectOption>();
            opt.SelectedPlayerId = desc.sprite;
            opt.StarsCost = desc.cost;
            opt.ImgCarBody.sprite = Resources.Load<Sprite>(desc.sprite);
            opt.SelectCarScreen = this;
            InScreenOptions.Add(opt);
        }
    }

    public void RefreshUI()
    {
        foreach (PlayerSelectOption opt in InScreenOptions)
        {
            opt.RefreshUI();
        }
        StarsCountTxt.text = PlayerPrefs.GetInt(Constants.KEY_STARS_COUNT).ToString();
    }
 
    private class OptionDesc
    {
        public string sprite;
        public int cost;
        public OptionDesc(string sprite, int cost) {
            this.sprite = sprite;
            this.cost = cost;
        }
    }
}
