using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // ProjectileManager�� �̱��� �ν��Ͻ�
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }
    // ����� ����ü ������ �迭
    [SerializeField] private GameObject[] projectilePrefabs;

    private void Awake()
    {
        instance = this;
    }
    #region
    /// <summary>
    /// ����ü(Projectile)�� �����ϰ� �߻��ϴ� �޼���
    /// </summary>
    /// <param name="rangeWeaponHandler">�߻��ϴ� ���� �ڵ鷯</param>
    /// <param name="startPostiion">����ü�� ���� ��ġ</param>
    /// <param name="direction">����ü�� ���ư� ����</param>
    #endregion
    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
    {
        // ������ ���� �ڵ鷯�� BulletIndex�� ����Ͽ� ������ źȯ ������ ����
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);
        
        // �������� �ùٸ��� �������� �ʾ��� ��� ���� �޽��� ��� �� ����
        if (obj == null)
        {
            Debug.LogError(" ProjectileController�� �����ϴ�! ������ Ȯ�� �ʿ�!");
            return;
        }
        // ������ ����ü�� ProjectileController�� �ִ��� Ȯ�� �� �ʱ�ȭ ����
        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler);
    }

}