using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWeapon : UICanvas
{
    [SerializeField] private List<GameObject> listWeapon;
    [SerializeField] private Transform listRotate;
    private int currentIndexWeapon = 0;
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

    }
}
