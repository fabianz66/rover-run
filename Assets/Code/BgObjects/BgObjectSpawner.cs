using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BgObjectSpawner : MonoBehaviour
{
    //Attributes
    public GameObject billboardPrefab;
    public GameObject locationPrefab;
    public GameObject roadSignGreenPrefab;
    public GameObject finishLinePrefab;
    public GameObject increaseSpeedPrefab;
    public Transform playerTransform;
    public Transform spawnPosition;

    //Which GameObject to use as parent to add the prefabs
    public Transform parentTransform;

    //All groups
    private readonly List<BgObjectGroup> groups = new List<BgObjectGroup>();

    // Impreza Group
    public BgObjectGroup imprezaBgGroup;

    #region All images to use in the background

    public Sprite impreza1;

    public Sprite alajuela1;

    public Sprite sanJose1;

    public Sprite heredia1;

    public Sprite puntarenas1;

    public Sprite guanacaste1;

    public Sprite limon1;

    public Sprite cartago1;

    #endregion

    #region All images to use in the background

    public Sprite adConstruYO1;

    #endregion

    private void Start()
    {
        CreateGroups();
    }

    private void FixedUpdate()
    {
        BgObjectGroup bog = GetBgObjectGroup(spawnPosition.position.x); //Current bg object group in screen
        if (bog == null) return;

        BgObject bo = bog.NextObject(spawnPosition.position.x);
        if (bo == null) return;

        GameObject go = null;
        switch (bo.Type) {
            case BgObject.TYPE.ROAD_SIGN_GREEN:
                go = Instantiate(roadSignGreenPrefab, parentTransform);
                go.GetComponent<BgObjectText>().SetText(bo.Text);                    
                break;
            case BgObject.TYPE.BILLBOARD:
                go = Instantiate(billboardPrefab, parentTransform);
                //go.GetComponent<BgObjectImage>().SetSprite(bo.Sprite);
                break;
            case BgObject.TYPE.IMAGE:
                go = Instantiate(locationPrefab, parentTransform);
                go.GetComponent<BgObjectImage>().SetSprite(bo.Sprite);
                break;
            case BgObject.TYPE.FINISH_LINE:
                go = Instantiate(finishLinePrefab, parentTransform);
                go.name = "FinishLine"; // Check PlayerSpeed script.
                break;
            case BgObject.TYPE.INCREASE_SPEED:
                go = Instantiate(increaseSpeedPrefab, parentTransform);
                go.name = "IncreaseSpeed"; // Check PlayerSpeed script.
                break;
        }

        if (go == null) return;
        
        float height = go.GetComponent<SpriteRenderer>().bounds.size.y;
        go.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y + height / 2, spawnPosition.position.z);        
    }

    /**
     * Creates all groups that are shown in the background
     * */
    void CreateGroups()
    {
        int position = 0;
        int length = 150;
        BgObjectGroup group = null;

        //ALAJUELA
        group = new BgObjectGroup("ALAJUELA", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "ALAJUELA", null);        
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.alajuela1);        
        groups.Add(group);
        position += length;

        //CARTAGO
        group = new BgObjectGroup("CARTAGO", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "CARTAGO", null);        
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.cartago1);
        group.AddBgObject(BgObject.TYPE.BILLBOARD, "Encuentra Ing. o Arq. en www.construyo.cr", null);
        groups.Add(group);
        position += length;

        //GUANACASTE
        group = new BgObjectGroup("GUANACASTE", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "GUANACASTE", null);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.guanacaste1);
        groups.Add(group);
        position += length;

        //HEREDIA
        group = new BgObjectGroup("HEREDIA", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "HEREDIA", null);
        group.AddBgObject(BgObject.TYPE.INCREASE_SPEED, null, null);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.heredia1);
        groups.Add(group);
        position += length;

        //LIMON
        group = new BgObjectGroup("LIMON", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "LIMON", null);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.limon1);
        groups.Add(group);
        position += length;

        //PUNTARENAS
        group = new BgObjectGroup("PUNTARENAS", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "PUNTARENAS", null);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.puntarenas1);
        groups.Add(group);
        position += length;

        //SAN JOSE
        group = new BgObjectGroup("SAN JOSE", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "SAN JOSE", null);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.sanJose1);
        group.AddBgObject(BgObject.TYPE.BILLBOARD, "Encuentra Ing. o Arq. en www.construyo.cr", null);
        groups.Add(group);
        position += length;

        //IMPREZA
        imprezaBgGroup = new BgObjectGroup("IMPREZA LTDA", position, length);
        imprezaBgGroup.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "IMPREZA", null);        
        imprezaBgGroup.AddBgObject(BgObject.TYPE.IMAGE, null, this.impreza1);
        imprezaBgGroup.AddBgObject(BgObject.TYPE.FINISH_LINE, null, null);
        groups.Add(imprezaBgGroup);
    }

    /**
     * Gets a position in X and returns the current group
     * */
    public BgObjectGroup GetBgObjectGroup(float positionX) {
        foreach (BgObjectGroup bog in groups) {
            if (bog.IsInside(positionX)) {
                return bog;
            }
        }
        return null;
    }

    /**
     * Gets a position in X and returns the next group after the current one
     * */
    public BgObjectGroup GetNextBgObjectGroup(float positionX)
    {
        int current = -1;
        for (int i=0; i < groups.Count; i++)
        {
            BgObjectGroup bog = groups[i];
            if (bog.IsInside(positionX))
            {
                current = i;
                break;
            }
        }
        if (current != -1 && current < (groups.Count - 1))
        {
            return groups[current + 1];
        }
        return null;
    }
}

