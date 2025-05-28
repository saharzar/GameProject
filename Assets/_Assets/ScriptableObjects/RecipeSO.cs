using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenobjectSOList;
    public string recipeName;

}
