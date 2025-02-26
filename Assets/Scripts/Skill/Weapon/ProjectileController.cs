using UnityEngine;

/// <summary>
/// ����ü(Projectile)�� �����ϴ� ��Ʈ�ѷ� Ŭ����.
/// ����ü�� �̵�, �浹 ����, ���� �ð� üũ �� ������ ���.
/// </summary>
public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer; // ���� �� �浹 ������ ���̾� ����ũ

    private RangeWeaponHandler rangeWeaponHandler; // ����ü�� �߻��� ���� �ڵ鷯
    private float currentDuration; // ����ü�� ������ �ð�
    private Vector2 direction; // ����ü �̵� ����
    private bool isReady; // ����ü�� Ȱ��ȭ�Ǿ����� ����

    private Rigidbody2D _rigidbody; // ����ü�� Rigidbody2D ������Ʈ
    private SpriteRenderer spriteRenderer; // ����ü�� ��������Ʈ ������

    public bool fxOnDestory = true; // ����ü �ı� �� FX ȿ�� ���� ����

    /// <summary>
    /// Awake()���� Rigidbody2D �� SpriteRenderer ������Ʈ�� ������.
    /// </summary>
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // ���� ������Ʈ���� ��������Ʈ ������ ��������
        _rigidbody = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ ��������
    }

    /// <summary>
    /// Update()���� ���� �ð��� üũ�ϰ� ����ü�� �̵���Ŵ.
    /// </summary>
    private void Update()
    {
        if (!isReady) return; // ����ü�� �غ���� �ʾҴٸ� ���� ���� X

        currentDuration += Time.deltaTime; // ��� �ð� ����

        // ����ü�� ���� �ð��� �ʰ��Ǹ� ����
        if (currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        // ����ü �̵� (���� * ���� �ӵ�)
        _rigidbody.velocity = direction * rangeWeaponHandler.AttackSpeed;
    }

    /// <summary>
    /// �浹 ���� ��, ���̳� ������ �浹�ϸ� ����ü ����.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ������ �浹���� ���
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestory);
        }
        // ���� ���(Enemy ��)�� �浹���� ���
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }

        // ���Ϳ� �浹���� ���
        if (collision.CompareTag("Monster"))
        {
            BaseMonster monster = collision.GetComponent<BaseMonster>();

            if (monster != null)
            {
                // ���Ϳ��� ������ ����
                monster.Damage(rangeWeaponHandler.Damage);
                Debug.Log($"������ ���� HP: {monster.Health}");

                // ����ü ����
                //DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
            }
            else
            {
                Debug.LogWarning("BaseMonster ������Ʈ�� ã�� �� ����!");
            }
        }
    }

    /// <summary>
    /// ����ü�� �ʱ�ȭ�ϰ�, �߻� ����� �Ӽ��� ������.
    /// </summary>
    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler)
    {
        rangeWeaponHandler = weaponHandler; // �߻��� ���� �ڵ鷯 ����

        this.direction = direction; // ����ü �̵� ���� ����
        currentDuration = 0; // ���� �ð� �ʱ�ȭ

        // ����ü ũ�� �� ���� ����
        transform.localScale = Vector3.one * weaponHandler.bulletSize;
        spriteRenderer.color = weaponHandler.ProjectileColor;

        isReady = true; // ����ü Ȱ��ȭ
    }

    /// <summary>
    /// ����ü�� �����ϴ� �޼���.
    /// </summary>
    /// <param name="position">������ ��ġ</param>
    /// <param name="createFx">�ı� �� FX ȿ�� ���� ����</param>
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject); // ����ü ����
    }
}
