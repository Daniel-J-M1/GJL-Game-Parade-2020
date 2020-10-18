using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    string machineType;
    bool buying = false;

    // Start is called before the first frame update
    void Start()
    {
        machineType = transform.tag;
        buying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            print("buying");
            buying = true;
        }
        else
            buying = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (buying)
        {
            if (other.name == "Player")
            {
                PlayerController pcont = other.GetComponent<PlayerController>();
                PlayerCombat pcomb = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerCombat>();
                WeaponCollider weapColl = GameObject.FindGameObjectWithTag("Bic").GetComponent<WeaponCollider>();
                if (pcont.GetCash() >= 10)
                {
                    if (machineType == "BlueVendingMachine") //Blue - ammo
                    {
                        pcont.AlterCash(-10);
                        pcomb.SetMaxAmmo(10);
                        weapColl.baseAmmoGain += 0.1f;
                    }
                    if (machineType == "OrangeVendingMachine") //Orange - dash CD
                    {
                        pcont.AlterCash(-10);
                        pcont.SetDashCooldown(-0.15f);
                    }
                    if (machineType == "GreenVendingMachine") //Green - Health
                    {
                        if (pcont.GetHealth() < pcont.GetMaxHealth())
                        {
                            pcont.AlterCash(-10);
                            pcont.AlterHealth(10, false);
                        }
                    }
                    if (machineType == "PurpleVendingMachine") //Purple - Attack Speed
                    {
                        pcont.AlterCash(-10);
                        weapColl.baseDamage += 0.2f;
                        pcomb.SetSwingSpeed(0.98f);
                    }
                }
            }
        }
    }
}
