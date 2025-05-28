using UnityEngine;
using System;

public class OvenCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    [SerializeField] private BakingRecipeSO[] bakingRecipeSOArray;

    private BakingRecipeSO currentRecipe;
    private float bakingTimer;
    private bool isBaking;

    private void Update()
    {
        if (HasKitchenObject() && isBaking)
        {
            bakingTimer += Time.deltaTime;

            float progress = bakingTimer / currentRecipe.bakingProgressMax;
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = progress
            });

            if (bakingTimer >= currentRecipe.bakingProgressMax)
            {
                KitchenObjectSO output = currentRecipe.output;
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(output, this);
                isBaking = false;
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                StartBaking();
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                StopBaking();
            }
        }
    }

    private void StartBaking()
    {
        currentRecipe = GetBakingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
        if (currentRecipe != null)
        {
            bakingTimer = 0f;
            isBaking = true;
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = 0f
            });
        }
    }

    private void StopBaking()
    {
        isBaking = false;
        currentRecipe = null;
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
        {
            progressNormalized = 0f
        });
    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        return GetBakingRecipeSOWithInput(input) != null;
    }

    private BakingRecipeSO GetBakingRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (BakingRecipeSO recipe in bakingRecipeSOArray)
        {
            if (recipe.input == input)
                return recipe;
        }
        return null;
    }
}
