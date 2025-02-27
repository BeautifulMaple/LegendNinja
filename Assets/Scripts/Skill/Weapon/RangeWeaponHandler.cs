using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition; // ����ü�� ������ ��ġ

    [SerializeField] private int bulletIndex; // źȯ �ε��� (����� źȯ ���� �ĺ�)
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] public float bulletSize = 1; // ����ü ũ��
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration; // ����ü ���� �ð�
    public float Duration { get => duration; set => duration = value; }

    [SerializeField] private float spread; // ����ü �߻� ���� ����ȭ (���� ����)
    public float Spread { get => spread; set => spread = value; }

    [SerializeField] private float numberofProjectilesPerShot; // �� ���� ���� �� �߻��� ����ü ����
    public float NumberOfProjectilesPerShot { get => numberofProjectilesPerShot; set => numberofProjectilesPerShot = value; }


    [SerializeField] private float multipleProjectilesAngel; // ����ü �� ���� (����)
    public float MultipleProjectilesAngle { get => multipleProjectilesAngel; set => multipleProjectilesAngel = value; }

    [SerializeField] private Color projectileColor; // ����ü ����
    public Color ProjectileColor { get { return projectileColor; } }

    private ProjectileManager projectileManager; // ����ü ���� �� ����

    public RangeWeaponHandler(Transform projectileSpawnPosition, float damage, float speed, float cooldown, int bulletIndex, float bulletSize, float duration, float spread, float _numberofProjectilesPerShot, float _multipleProjectilesAngel, Color projectileColor, ProjectileManager projectileManager)
    {
        this.projectileSpawnPosition = projectileSpawnPosition;
        Damage = damage;
        AttackSpeed = speed;
        Delay = cooldown;
        
        this.bulletIndex = bulletIndex;
        this.bulletSize = bulletSize;
        this.duration = duration;
        this.spread = spread;
        NumberOfProjectilesPerShot = _numberofProjectilesPerShot;
        MultipleProjectilesAngle = _multipleProjectilesAngel;
        this.projectileColor = projectileColor;
        this.projectileManager = projectileManager;
    }

    /// <summary>
    /// Start()���� ProjectileManager�� ������.
    /// </summary>
    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance; // �̱��� �ν��Ͻ� ��������

        if (projectileManager == null)
        {
            Debug.LogError("ProjectileManager.Instance�� null�Դϴ�. ProjectileManager�� ���� �����ϴ��� Ȯ���ϼ���!");
        }
    }

    /// <summary>
    /// ���� �� ���� ���� ����ü�� �����Ͽ� �߻�.
    /// </summary>
    public override void Attack(Vector3 direction)
    {
        base.Attack(direction);

        float projectilesAngleSpace = multipleProjectilesAngel; // ����ü �� ���� ����
        float numberOfProjectilesPerShot = numberofProjectilesPerShot; // �߻��� ����ü ����

        // ����ü�� ��� �����ϱ� ���� �ּ� ���� ���
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * multipleProjectilesAngel;

        // ������ ������ŭ ����ü ����
        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i; // �⺻ ���� ����
            float randomSpread = Random.Range(-spread, spread); // ������ ���� ȿ�� �߰�
            angle += randomSpread;
            CreateProjectile(direction, angle);
        }
    }
    

    /// <summary>
    /// ����ü�� �����Ͽ� �߻�.
    /// </summary>
    /// <param name="_lookDirection">ĳ���Ͱ� �ٶ󺸴� ����</param>
    /// <param name="angle">����ü ȸ�� ����</param>
    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        if (projectileManager == null)
        {
            Debug.LogError("ProjectileManager�� null�Դϴ�! �̱����� �ùٸ��� �����Ǿ����� Ȯ���ϼ���.");
            return;
        }
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
