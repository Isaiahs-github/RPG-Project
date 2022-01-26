using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{

    #region Singleton

    public static Inventory instance;

    // CHANGED
    public InventoryUI inviUI; 

    void Start(){
        inviUI.init();
    }

    void Awake  (){
        if(instance != null){
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion
    

    public delegate void OnItemChanged(); // Allows you to do some 
    public OnItemChanged onItemChangedCallback; // really great stuff.

    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add (Item item){

        if (!item.isDefaultItem){
            if (items.Count >= space){
                Debug.Log("Not enough room.");
                return false;
            }
            items.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }
    
    public void Remove (Item item){
        items.Remove(item);

        if (onItemChangedCallback != null)
            Debug.Log("calling onItemChangedCallback.");
            onItemChangedCallback.Invoke();
    }
    
 
}
