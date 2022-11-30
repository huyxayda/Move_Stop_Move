using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : UICanvas
{
    public GameObject equipButton;
    public GameObject equippedButton;

    public WeaponPicture currentPicture;
    public List<WeaponPicture> pictures;
    private int currentNumber;
    

    private void Start()
    {
        DefaultPicture();
    }

    private void Update()
    {
        if (currentPicture.isEquip)
        {
            equipButton.SetActive(false);
            equippedButton.SetActive(true);
        }
        else
        {
            equipButton.SetActive(true);
            equippedButton.SetActive(false);
        }
    }
    public void CloseButton()
    {
        UIManager.Instance.CloseUI<WeaponShop>();
    }
    public void EquipWeapon()
    {
        //LevelManager.Instance.player.ChangeWeapon(currentPicture.weaponType);
        //LevelManager.Instance.player.weapon = currentPicture.weaponType;
        //LevelManager.Instance.player.WeaponpoolType = currentPicture.WeaponpoolType;
        LevelManager.Instance.EquipWeapon(currentPicture.weaponType);
        LevelManager.Instance.EquipBullet(currentPicture.WeaponpoolType);
        Debug.Log(currentPicture.weaponType);
        for (int i = 0; i < pictures.Count; i++)
        {
            if(i == currentNumber)
            {
                currentPicture.isEquip = true;

            }
            else
            {
                pictures[i].isEquip = false;
            }
        }
    }

    public void DefaultPicture()
    {
        currentNumber = 0;
        currentPicture = pictures[currentNumber];
        for(int i = 1; i < pictures.Count; i++)
        {
            pictures[i].gameObject.SetActive(false);
        }
    }

    public void PrevWeapon()
    {
        if(currentNumber > 0)
        {
            currentPicture.gameObject.SetActive(false);
            currentNumber--;
            currentPicture = pictures[currentNumber];
            pictures[currentNumber].gameObject.SetActive(true);
        }       
    }

    public void NextWeapon()
    {
        if(currentNumber < pictures.Count - 1)
        {
            pictures[currentNumber].gameObject.SetActive(false);
            currentNumber++;
            currentPicture = pictures[currentNumber];
            pictures[currentNumber].gameObject.SetActive(true);
        }
    }
}

