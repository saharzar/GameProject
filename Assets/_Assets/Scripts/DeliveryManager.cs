using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnSpawnTimer;
    private float spawnSpawnTimerMax=4f;
    private int waitingRecipesMax = 4;

    private void Awake()
    {
        Instance = this; 
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        spawnSpawnTimer -= Time.deltaTime;
        if(spawnSpawnTimer <= 0f)
        {
            spawnSpawnTimer = spawnSpawnTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);
            } 
        }
    }

 

} 
