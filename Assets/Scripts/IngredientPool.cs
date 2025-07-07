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
        if (Instance == null)
        {
            Instance = this;
            InitializePools();
        }
        else
        {
            Destroy(gameObject);
        }
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

                // Guardar el tipo en un componente auxiliar
                PooledIngredient pooled = obj.GetComponent<PooledIngredient>();
                if (pooled == null)
                {
                    pooled = obj.AddComponent<PooledIngredient>();
                }
                pooled.typeName = type.name;

                list.Add(obj);
            }
            pools[type.name] = list;
        }
    }

    public GameObject GetFromPool(string typeName)
    {
        if (!pools.ContainsKey(typeName)) return null;

        List<GameObject> pool = pools[typeName];

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                GameObject obj = pool[i];
                obj.SetActive(true);
                return obj;
            }
        }

        // No hay objetos disponibles, crear uno nuevo
        IngredientPoolData data = ingredientTypes.Find(x => x.name == typeName);
        if (data != null)
        {
            GameObject newObj = Instantiate(data.prefab);
            newObj.SetActive(true);

            PooledIngredient pooled = newObj.GetComponent<PooledIngredient>();
            if (pooled == null)
            {
                pooled = newObj.AddComponent<PooledIngredient>();
            }
            pooled.typeName = typeName;

            pool.Add(newObj);
            return newObj;
        }

        return null;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);

        // Asegurarse de que se conoce a qué tipo pertenece
        PooledIngredient pooled = obj.GetComponent<PooledIngredient>();
        if (pooled != null && !pools.ContainsKey(pooled.typeName))
        {
            pools[pooled.typeName] = new List<GameObject> { obj };
        }
    }
}