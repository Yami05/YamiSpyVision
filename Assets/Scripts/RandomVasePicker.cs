using System.Collections.Generic;
using UnityEngine;

public class RandomVasePicker : Singleton<RandomVasePicker>
{

    [SerializeField] private GameObject vase;
    [SerializeField] private List<Transform> brokens = new List<Transform>();
    [SerializeField] private List<GameObject> vases = new List<GameObject>();

    private List<GameObject> brokenParts = new List<GameObject>();

    private GameObject go;
    private BoxCollider mesh;
    private GameEvents gameEvents;

    private void Start()
    {
        vase = Resources.Load<GameObject>("Vase/kirik");
        gameEvents = GameEvents.instance;

        foreach (Transform vase in vase.transform) brokens.Add(vase);
        for (int i = 0; i < 3; i++)
        {
            go = Instantiate(brokens[Random.Range(0, brokens.Count)].gameObject, new Vector3(transform.position.x,
               transform.position.y, transform.position.z + brokens.IndexOf(brokens[i])), Quaternion.identity);
            vases.Add(go);
            mesh = go.AddComponent<BoxCollider>();
            mesh.size = new Vector3(2.5f, 2.5f, 2);

        }
        //for (int i = 0; i < vases.Count; i++)
        //{
        //    for (int j = 0; j < vases[i].transform.childCount; j++)
        //    {
        //        brokenParts.Add(vases[i].transform.GetChild(j).gameObject);
        //    }
        //}
    }

    public void RemovePart(GameObject go) => vases.Remove(go);

    public void GetChildCount()
    {
        if (vases.Count == 0)
        {
            gameEvents.NextLevel?.Invoke();
        }
    }
}
