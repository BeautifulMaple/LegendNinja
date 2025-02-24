using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition; // ����ü�� ������ ��ġ

    [SerializeField] private int bulletIndex; // źȯ �ε��� (����� źȯ ���� �ĺ�)
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1; // ����ü ũ��
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration; // ����ü ���� �ð�
    public float Duration { get { return duration; } }

    [SerializeField] private float spread; // ����ü �߻� ���� ����ȭ (���� ����)
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot; // �� ���� ���� �� �߻��� ����ü ����
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectilesAngel; // ����ü �� ���� (����)
    public float MultipleProjectilesAngel { get { return multipleProjectilesAngel; } }

    [SerializeField] private Color projectileColor; // ����ü ����
    public Color ProjectileColor { get { return projectileColor; } }

    private ProjectileManager projectileManager; // ����ü ���� �� ����

    /// <summary>
    /// Start()���� ProjectileManager�� ������.
    /// </summary>
    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance; // �̱��� �ν��Ͻ� ��������
    }

    /// <summary>
    /// ���� �� ���� ���� ����ü�� �����Ͽ� �߻�.
    /// </summary>
    public override void Attack()
    {
        base.Attack();

        float projectilesAngleSpace = multipleProjectilesAngel; // ����ü �� ���� ����
        int numberOfProjectilesPerShot = numberofProjectilesPerShot; // �߻��� ����ü ����

        // ����ü�� ��� �����ϱ� ���� �ּ� ���� ���
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * multipleProjectilesAngel;

        // ������ ������ŭ ����ü ����
        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i; // �⺻ ���� ����
            float randomSpread = Random.Range(-spread, spread); // ������ ���� ȿ�� �߰�
            angle += randomSpread;
            //CreateProjectile(Controller.LookDirection, angle);
            CreateProjectile(Shoot.LookDirection, angle); // �׽�Ʈ


        }
    }

    /// <summary>
    /// ����ü�� �����Ͽ� �߻�.
    /// </summary>
    /// <param name="_lookDirection">ĳ���Ͱ� �ٶ󺸴� ����</param>
    /// <param name="angle">����ü ȸ�� ����</param>
    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet(
            this,
            projectileSpawnPosition.position,
            RotateVector2(_lookDirection, angle)); // ����ü ���� ȸ�� �� �߻�
    }

    /// <summary>
    /// ���͸� Ư�� ������ ȸ����Ű�� �޼���.
    /// </summary>
    /// <param name="v">ȸ���� ����</param>
    /// <param name="degree">ȸ���� ����</param>
    /// <returns>ȸ���� ����</returns>
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
