using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class IngredientPoolData
{
    public string name;
    public GameObject prefab;
    public int poolSize = 10;
}



public class IngredientPool : MonoBehaviour
{
    public static IngredientPool Instance;

    public List<IngredientPoolData> ingredientTypes;

    private Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();

    void Awake()
    {
        Instance = this;
        InitializePools();
    }

    void InitializePools()
    {
        foreach (var type in ingredientTypes)
        {
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < type.poolSize; i++)
            {
                GameObject obj = Instantiate(type.prefab);
                obj.SetActive(false);
                list.Add(obj);
            }
            pools[type.name] = list;
        }
    }

    public GameObject GetFromPool(string typeName)
    {
        if (!pools.ContainsKey(typeName)) return null;

        foreach (var obj in pools[typeName])
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        // Si no hay ninguno libre, crear uno nuevo
        IngredientPoolData data = ingredientTypes.Find(x => x.name == typeName);
        if (data != null)
        {
            GameObject newObj = Instantiate(data.prefab);
            newObj.SetActive(false);
            pools[typeName].Add(newObj);
            return newObj;
        }

        return null;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        
    }
}