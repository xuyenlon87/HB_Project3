using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeapon : UICanvas
{
    [SerializeField] private List<GameObject> listWeapon;
    [SerializeField] private Transform listRotate;
    [SerializeField] private List<int> listPrice;
    [SerializeField] private Text textPrice;
    private int currentIndexWeapon = 0;
    private int currentIndexPrice = 0;
    private void Start()
    {
        listWeapon[currentIndexWeapon].SetActive(true);
    }
    void Update()
    {
        listRotate.Rotate(Vector3.up, 45f * Time.deltaTime);
    }
    public void ButtonNext()
    {
        if (currentIndexWeapon < listWeapon.Count - 2)
        {
            listWeapon[currentIndexWeapon].SetActive(false);
            currentIndexWeapon += 1;
            listWeapon[currentIndexWeapon].SetActive(true);
        }
        else if (currentIndexWeapon == listWeapon.Count - 1)
        {
            currentIndexWeapon += 0;
        }
        textPrice.text = listPrice[currentIndexWeapon].ToString();
    }

    public void ButtonBack()
    {
        if (currentIndexWeapon >= 1)
        {
            listWeapon[currentIndexWeapon].SetActive(false);
            currentIndexWeapon -= 1;
            listWeapon[currentIndexWeapon].SetActive(true);
        }
        else if (currentIndexWeapon == listWeapon.Count - 1)
        {
            currentIndexWeapon -= 0;
        }
        textPrice.text = listPrice[currentIndexWeapon].ToString();

    }

    public void ButtonMainMenu()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        Close(0);
    }
}
