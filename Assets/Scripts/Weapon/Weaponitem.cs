using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponitem : MonoBehaviour,IItem
{
   public bool Selected{get;set;}
  
   
   
    void Update() {
        if(Selected && Input.GetButtonDown("Submit")){
            Use();
        }
    }
   public void Use(){
    FindObjectOfType<WeaponInventory>().AddWeapon(this.gameObject);
    this.transform.SetParent(FindObjectOfType<WeaponInventory>().GetComponent<Transform>());
   }

}
