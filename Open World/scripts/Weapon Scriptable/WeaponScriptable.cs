using UnityEngine;

public enum WeaponTypes { Firearm, Throwable }

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Item/Weapons/weapon", order = 0)]
public class WeaponScriptable : BaseItemScriptable
{
    public float fireRate = default;
   
    [SerializeField] WeaponTypes WeaponType = default;
    [SerializeField] float clipSize = default;
    [SerializeField] float maxClipSize = default;

    public float UseWeapon(float lastShot)
    {
        if (Time.time > fireRate + lastShot)
        {
            switch (WeaponType)
            {
                case WeaponTypes.Firearm:
                    if (clipSize > 0)
                    {
                        clipSize--;
                      //  Debug.Log("Firing");
                    }
                    else
                    {
                     //   Debug.Log("Reload Needed");
                    }
                    return Time.time;
                case WeaponTypes.Throwable:
                   // Debug.Log("Throwing");
                    return Time.time;
            }
        }
        return lastShot;
    }

    void ReloadWeapon()
    {
        clipSize = maxClipSize;
    }
}
