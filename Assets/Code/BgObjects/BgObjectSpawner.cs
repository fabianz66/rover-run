using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObjectSpawner : MonoBehaviour
{
    public GameObject billboardPrefab;
    public GameObject locationPrefab;
    public GameObject roadSignGreenPrefab;
    public GameObject finishLinePrefab;
    public Transform playerTransform;
    public Transform spawnPosition;

    //Which GameObject to use as parent to add the prefabs
    public Transform parentTransform;

    //All groups
    private readonly List<BgObjectGroup> groups = new List<BgObjectGroup>();

    #region All images to use in the background

    public Sprite impreza1;
    public Sprite impreza2;

    public Sprite alajuela1;
    public Sprite alajuela2;

    public Sprite sanJose1;
    public Sprite sanJose2;

    public Sprite heredia1;
    public Sprite heredia2;

    public Sprite puntarenas1;
    public Sprite puntarenas2;

    public Sprite guanacaste1;
    public Sprite guanacaste2;

    public Sprite limon1;
    public Sprite limon2;

    public Sprite cartago1;
    public Sprite cartago2;

    #endregion

    private void Start()
    {
        CreateAreas();
    }

    void CreateAreas()
    {
        int position = 0;
        int length = 50;        
        BgObjectGroup group = null;

        //IMPREZA
        group = new BgObjectGroup("IMPREZA LTDA", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "IMPREZA", null);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.impreza1);
        group.AddBgObject(BgObject.TYPE.FINISH_LINE, null, null);
        groups.Add(group);
        position += length;

        //ALAJUELA
        group = new BgObjectGroup("ALAJUELA", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "ALAJUELA", null);
        group.AddBgObject(BgObject.TYPE.BILLBOARD, "TE AMO OSI", null);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.alajuela1);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.alajuela2);
        groups.Add(group);
        position += length;

        //SAN JOSE
        group = new BgObjectGroup("SAN JOSE", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "SAN JOSE", null);        
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.sanJose1);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.sanJose2);
        groups.Add(group);
        position += length;

        //HEREDIA
        group = new BgObjectGroup("HEREDIA", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "HEREDIA", null);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.heredia1);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.heredia2);
        groups.Add(group);
        position += length;

        //PUNTARENAS
        group = new BgObjectGroup("PUNTARENAS", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "PUNTARENAS", null);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.puntarenas1);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.puntarenas2);
        groups.Add(group);
        position += length;

        //GUANACASTE
        group = new BgObjectGroup("GUANACASTE", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "GUANACASTE", null);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.guanacaste1);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.guanacaste2);
        groups.Add(group);
        position += length;

        //LIMON
        group = new BgObjectGroup("LIMON", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "LIMON", null);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.limon1);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.limon2);
        groups.Add(group);
        position += length;

        //CARTAGO
        group = new BgObjectGroup("CARTAGO", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "CARTAGO", null);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.cartago1);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.cartago2);
        groups.Add(group);
        position += length;

        //IMPREZA
        group = new BgObjectGroup("IMPREZA LTDA", position, length);
        group.AddBgObject(BgObject.TYPE.ROAD_SIGN_GREEN, "IMPREZA", null);
        group.AddBgObject(BgObject.TYPE.IMAGE, null, this.impreza1);
        groups.Add(group);
    }

    private void FixedUpdate()
    {
        BgObjectGroup bog = GetBgObjectGroup(playerTransform.position.x); //Current bg object group in screen
        if (bog == null) return;

        BgObject bo = bog.NextObject(playerTransform.position.x);
        if (bo == null) return;

        GameObject go = null;
        switch (bo.Type) {
            case BgObject.TYPE.ROAD_SIGN_GREEN:
                go = Instantiate(roadSignGreenPrefab, parentTransform);
                go.GetComponent<BgObjectText>().SetText(bo.Text);                    
                break;
            case BgObject.TYPE.BILLBOARD:
                go = Instantiate(billboardPrefab, parentTransform);
                go.GetComponent<BgObjectText>().SetText(bo.Text);
                break;
            case BgObject.TYPE.IMAGE:
                go = Instantiate(locationPrefab, parentTransform);
                go.GetComponent<BgObjectImage>().SetSprite(bo.Sprite);
                break;
            case BgObject.TYPE.FINISH_LINE:
                go = Instantiate(finishLinePrefab, parentTransform);
                go.name = "FinishLine"; // Check PlayerSpeed script.
                break;
        }

        if (go == null) return;
        
        float height = go.GetComponent<SpriteRenderer>().bounds.size.y;
        go.transform.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y + height / 2, spawnPosition.position.z);        
    }

    private BgObjectGroup GetBgObjectGroup(float positionX) {
        foreach (BgObjectGroup bog in groups) {
            if (bog.IsInside(positionX)) {
                return bog;
            }
        }
        return null;
    }
}

