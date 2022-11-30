using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum SkinShopState { Hair, Pant}
public class SkinShop : UICanvas
{
    public GameObject HairBtnOn;
    public GameObject HairBtnOff;
    public GameObject HairView;

    public GameObject PantBtnOn;
    public GameObject PantBtnOff;
    public GameObject PantView;

    public SkinShopState currentShopState;

    public List<SkinPicture> hairPictures = new List<SkinPicture>();
    public List<SkinPicture> pantPictures = new List<SkinPicture>();

    public SkinPicture currentPictures;
    public int currentIndex;

    public TextMeshProUGUI textSkin;
    public TextMeshProUGUI textCost;
    public TextMeshProUGUI textCoin;

    public GameObject BtnBuy;
    public GameObject BtnEquip;
    public GameObject BtnEquipped;


    private void Start()
    {
        TurnOnHair();
        TurnOffPant();
        currentShopState = SkinShopState.Hair;
    }
    public void OnHairBtn()
    {
        TurnOnHair();
        TurnOffPant();
        currentShopState = SkinShopState.Hair;
        textSkin.text = " ";
        TurnOffBtn();
    }

    public void OnPantBtn()
    {
        TurnOffHair();
        TurnOnPant();
        currentShopState = SkinShopState.Pant;
        textSkin.text = " ";
        TurnOffBtn();

    }

    public void TurnOnHair()
    {
        HairBtnOff.SetActive(false);
        HairBtnOn.SetActive(true);
        HairView.SetActive(true);
    }

    public void TurnOffHair()
    {
        HairBtnOff.SetActive(true);
        HairBtnOn.SetActive(false);
        HairView.SetActive(false);
    }

    public void TurnOnPant()
    {
        PantBtnOff.SetActive(false);
        PantBtnOn.SetActive(true);
        PantView.SetActive(true);
    }

    public void TurnOffPant()
    {
        PantBtnOff.SetActive(true);
        PantBtnOn.SetActive(false);
        PantView.SetActive(false);
    }

    public void TurnOffBtn()
    {
        BtnBuy.SetActive(false);
        BtnEquip.SetActive(false);
        BtnEquipped.SetActive(false);
    }

    public void PantClick(int index)
    {
        currentIndex = index;
        currentPictures = pantPictures[currentIndex];
        UpdateName(currentPictures);
        UpdateCost(currentPictures);
        CheckBtn(currentPictures);
    }

    public void HairClick(int index)
    {
        currentIndex = index;
        currentPictures = hairPictures[currentIndex];
        UpdateName(currentPictures);
        UpdateCost(currentPictures);
        CheckBtn(currentPictures);
    }

    public void UpdateName(SkinPicture currentPictures)
    {
        textSkin.text = currentPictures.nameSkin;       
    }
    public void UpdateCost(SkinPicture currentPictures)
    {
        textCost.text = currentPictures.cost.ToString();
    }

    public void PurchaseItem()
    {
        if(currentPictures.cost <= LevelManager.Instance.coinPlayer)
        {
            //TODO them logic tru tien khi mua do 
            LevelManager.Instance.PayCoin(currentPictures.cost);
            textCoin.text = LevelManager.Instance.coinPlayer.ToString();
            currentPictures.isBuy = true;
            BtnEquipOn();
        }
        else
        {
            Debug.Log("Not enought money");
        }
    }

    public void EquiqItem()
    {
        

        if(currentPictures.skinShopState == SkinShopState.Hair)
        {
            for (int i = 0; i < hairPictures.Count; i++)
            {
                if(currentPictures == hairPictures[i])
                {
                    Debug.Log("trung roi nhe");
                    Debug.Log(currentPictures);
                    currentPictures.isEquip = true;
                }
                else
                {
                    hairPictures[i].isEquip = false;
                }
            }
            LevelManager.Instance.EquipHair(currentIndex);
        }
        if(currentPictures.skinShopState == SkinShopState.Pant)
        {
            for (int i = 0; i < pantPictures.Count; i++)
            {
                if (currentPictures == pantPictures[i])
                {
                    currentPictures.isEquip = true;
                }
                else
                {
                    pantPictures[i].isEquip = false;
                }
            }
            LevelManager.Instance.EquipPant(currentIndex);
        }
        BtnEquippedOn();
    }

    public void CheckBtn(SkinPicture currentPictures)
    {
        if(currentPictures.isBuy && !currentPictures.isEquip)
        {
            BtnEquipOn();
        }

        if (!currentPictures.isBuy && !currentPictures.isEquip)
        {
            BtnBuyOn();
        }

        if (currentPictures.isBuy && currentPictures.isEquip)
        {            
            BtnEquippedOn();
        }
    }

    public void BtnBuyOn()
    {
        BtnBuy.SetActive(true);
        BtnEquip.SetActive(false);
        BtnEquipped.SetActive(false);
    }

    public void BtnEquipOn()
    {
        BtnBuy.SetActive(false);
        BtnEquip.SetActive(true);
        BtnEquipped.SetActive(false);
    }

    public void BtnEquippedOn()
    {
        BtnBuy.SetActive(false);
        BtnEquip.SetActive(false);
        BtnEquipped.SetActive(true);
    }

    public void CloseButton()
    {
        UIManager.Instance.CloseUI<SkinShop>();
    }

    
}
