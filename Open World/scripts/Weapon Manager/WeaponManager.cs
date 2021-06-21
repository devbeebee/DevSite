using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] float lastShot;
    [SerializeField] WeaponScriptable weapon = default;
    [SerializeField] Transform weaponHolder = default;
    [SerializeField] Vector3 weaponAim = default;
    [SerializeField] Vector3 weaponAimDefault = default;
    private float curField;
    private float dampVelocity2;
    private void Start()
    {
        weaponAimDefault = weaponHolder.localPosition;
    }

    private void Update()
    {
        if (NewInput.Instance.RawFire > .9f)
        {
            lastShot = weapon.UseWeapon(lastShot);
        }
        /*
        if (NewInput.Instance.Reload)
        {
            Debug.Log("reload");
        }
        */

        AimDownSights();
    }

    void AimDownSights()
    {
        if (NewInput.Instance.RawAim > 0f)
        {
            weaponHolder.localPosition = Vector3.MoveTowards(weaponHolder.position, weaponAim, (NewInput.Instance.RawAim *5)* Time.deltaTime);
        }
        else
        {
            weaponHolder.localPosition = Vector3.Lerp(weaponHolder.position, weaponAimDefault, 1.0f);
        }
    }
}
