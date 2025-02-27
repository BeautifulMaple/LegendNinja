using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;

    private void Awake()
    {
        instance = this;
    }

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
    {
        //if (rangeWeaponHandler == null)
        //{
        //    Debug.LogError("ShootBullet() ȣ�� �� weaponHandler�� null�Դϴ�! SkillManager���� ���� ���� Ȯ�� �ʿ�!");
        //    return;
        //}
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);

        if (obj == null)
        {
            Debug.LogError(" ProjectileController�� �����ϴ�! ������ Ȯ�� �ʿ�!");
            return;
        }

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler);
    }

}