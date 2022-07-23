using System.Collections;
using UnityEngine;

public class GhostKing : LivingEntity
{
    [SerializeField] float m_jumpForce = 20f; // ���� ����
    private BoxCollider2D coll;
    private AudioSource audioSource;
    private Animator animator;
    private Rigidbody2D rigid;
    public ParticleSystem TrornReadyParticle; // ���� �غ� ��ƼŬ
    public ParticleSystem TrornParticle; // ���� ��ƼŬ
    public ParticleSystem TeleportyParticle; // �ڷ���Ʈ ��ƼŬ
    GameObject target; // �÷��̾�
    SpriteRenderer ghostrenderer;
    public int damage; // �⺻���� �÷��̾�� ������ �� ������
    public GameObject button; // ����ü ������Ʈ
    public AudioClip attack1ReadyClip; // ���� �ڱ� �غ� Ŭ��
    public AudioClip attack1Clip; // ���� �ڱ� Ŭ��
    public AudioClip attack3Clip; // ����ü �߻� Ŭ��
    public AudioClip TeleportyClip; // �ڷ���Ʈ Ŭ��
    public AudioClip dashReadyClip; // �뽬 �غ� Ŭ��
    public AudioClip jumpAttackClip; // ���� ���� Ŭ��
    public AudioClip dashClip; // �뽬 Ŭ��
    [SerializeField] private float speed = 5f; // �뽬 �ӵ�
    bool isLook = false; // �뽬���̶��
    public GameObject dashPrefab; // �뽬 ������
    private bool isJumpAttack = false; // �������� ���ΰ�
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        ghostrenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Think());
    }

    void Update()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");
        if (shake == null)
            shake = GameObject.FindGameObjectWithTag("CameraRig").GetComponent<Shake>();
        if (!isLook)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized; // �÷��̾ �ٶ󺸰� �����
            if (dir.x >= 0) // �÷��̾ �����ʿ� �ִٸ�
            {
                ghostrenderer.flipX = false;
                dashPrefab.transform.position = transform.position + new Vector3(3, -1, 0); // ���� ǥ�õ� ���������� �ű��
                dashPrefab.transform.rotation = Quaternion.Euler(0,0,0);
            }
            else if (dir.x < 0)// �÷��̾ ���ʿ� �ִٸ�
            {
                ghostrenderer.flipX = true;
                dashPrefab.transform.position = transform.position + new Vector3(-3, -1, 0); // ���� ǥ�õ� �������� �ű��
                dashPrefab.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    IEnumerator Think()
    {
        while (GameManager.instance.gameState != GameManager.GameState.Run) // ���� �������� �ƴ϶��
        {
            yield return new WaitForSeconds(0.3f);
        }
        animator.SetTrigger("m_Idle");
        yield return new WaitForSeconds(1f);
        int ranAction = Random.Range(0, 10);
        switch (ranAction)
        {
            // ������ ���ð� �ھƳ��� ����
            case 0:
            case 6:
                StartCoroutine(UpThorn());
                break;
            // �����̵� �� ���� �ڱ�
            case 1:
            case 2:
            case 3:
                StartCoroutine(TeleportUpThorn());
                break;
            // ����ü ����
            case 4:
            case 5:
                StartCoroutine(Shot());
                break;
            // �뽬 ����
            case 7:
            case 8:
                StartCoroutine(Dash());
                break;
            // ���� ���� ����
            case 9:
            case 10:
                StartCoroutine(Jump());
                break;
        }
    }
    IEnumerator TeleportUpThorn() // �����̵��� ���� �ڱ� ����
    {
        bool randBool = (Random.value > 0.5f); // �����̵��ϴ� ��ġ ����
        int teleportCount = Random.Range(2, 4); // �ڷ���Ʈ Ƚ��
        for (int i = 0; i < teleportCount; i++) // �ڷ���Ʈ Ƚ����ŭ �ڷ���Ʈ�ϱ�
        {
            StartCoroutine(Teleport(randBool, 4, 1.6f)); // �ڷ���Ʈ�ϱ�
            yield return new WaitForSeconds(0.3f); // 1�� ��ٸ���
            randBool = !randBool; // �ٸ� �������� �ڷ���Ʈ 
        }
        yield return new WaitForSeconds(0.5f); // 1�� ��ٸ���

        StartCoroutine(UpThorn()); // ���� �ڱ�
    }
    IEnumerator Teleport(bool dir, float x, float y) // ����� ���̿� �Ÿ��� �Է� ����
    {
        animator.SetTrigger("Walk"); // �̵� �ִϸ��̼����� �ٲ���
        yield return new WaitForSeconds(0.3f); // 0.3�� ��ٸ���
        if (dir) // ������ Ʈ����
        {
            transform.position = target.transform.position + new Vector3(x, y, 0); // ���������� �����̵�
        }
        else
        {
            transform.position = target.transform.position + new Vector3(-x, y, 0); // �������� �����̵�
        }
        StartCoroutine(shake.ShakeCamera(0.1f, 0.1f)); // ī�޶� ������ ��
        Instantiate(TeleportyParticle, transform.position, Quaternion.identity); // ��ƼŬ ����
        audioSource.clip = TeleportyClip; // ����� ���
        audioSource.Play();
    }
    IEnumerator Dash() // �����̵� �� ����
    {
        bool randBool = (Random.value > 0.5f); // �����̵��ϴ� ��ġ ����
        StartCoroutine(Teleport(randBool, 4, 1.6f)); // �ڷ���Ʈ�ϱ�
        audioSource.clip = dashReadyClip;
        audioSource.Play(); // �뽬 �غ� ���� ����
        yield return new WaitForSeconds(0.5f);
        dashPrefab.SetActive(true); // �뽬 ��� ���̱�
        yield return new WaitForSeconds(1f);

        dashPrefab.SetActive(false); // �뽬 ��� �����
        isLook = true; // �뽬������ ����
        animator.SetTrigger("Walk"); // �̵� �ִϸ��̼����� �ٲٱ�
        audioSource.clip = dashReadyClip;
        audioSource.Play(); // �뽬 ���� ����
        for (int i = 0; i <= 80; i++)
        {
            if (ghostrenderer.flipX) // ������ �����ִٸ�
                transform.localPosition += -Vector3.right * speed * Time.deltaTime; // �������� �����̱�
            else if (!ghostrenderer.flipX) // �������� �����ִٸ�
                transform.localPosition += Vector3.right * speed * Time.deltaTime; // ���������� �����̱�
            yield return new WaitForSeconds(0.001f); ;
        }
        StartCoroutine(Teleport(!randBool, 4, 1.6f)); // �ڷ���Ʈ�ϱ�
        yield return new WaitForSeconds(0.5f);
        isLook = false; // �뽬���� �ƴѰɷ� ����
        StartCoroutine(Think());
    }
    IEnumerator Jump() // ����
    {
        animator.SetTrigger("Jump");
        rigid.velocity = new Vector2(rigid.velocity.x, m_jumpForce);
        isLook = true;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Teleport(true, 0, 5)); // �÷��̾� ���� ��ġ�� ��������
        rigid.gravityScale = 0f; // �߷°� 0���� �����
        rigid.velocity = Vector3.zero; // ���̻� �ö��� �ʱ�

        animator.SetTrigger("Attack1"); // attack2 �ִϸ��̼� ȣ��
        yield return new WaitForSeconds(0.5f);
        isJumpAttack = true; // �������� true�� �����
        for (float i = 0; i < 360; i += 40) // 360���� �Ҳ� �߻� 
        {
            Instantiate(button, transform.position + new Vector3(0, -0.4f, 0), Quaternion.Euler(0, 0, i));
            audioSource.clip = attack3Clip; // ����� ���
            audioSource.Play();
        }
        yield return new WaitForSeconds(1f);
        rigid.gravityScale = 5f; // �߷°� 5���� �����
    }

    IEnumerator Shot() // ����ü �߻�
    {
        bool randBool = (Random.value > 0.5f); // �����̵��ϴ� ��ġ ����
        StartCoroutine(Teleport(randBool, 5, 1.6f)); // �ڷ���Ʈ�ϱ�
        yield return new WaitForSeconds(0.3f);

        animator.SetTrigger("Attack3Ready"); // �߻� �غ�
        yield return new WaitForSeconds(1f);

        animator.SetTrigger("Attack3"); // �߻�
        for (float i = -60; i <= 60; i += 17)
        {
            Instantiate(button, transform.position, Quaternion.Euler(0, 0, i));
            audioSource.clip = attack3Clip; // ����� ���
            audioSource.Play();
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Think());
    }
    IEnumerator UpThorn() // ������ �ڴ� ����
    {
        float trornDir = 1f;
        WaitForSeconds endWait; // ������ ��ٸ��� ��
        if (Vector3.Distance(transform.position, target.transform.position) < 5)
        {
            trornDir = Random.Range(1, 2); // �÷��̾ �����ٸ� ���� ������ ���� �����ϱ�
            endWait = new WaitForSeconds(4f); // ���� ������ 4�� ��ٸ���
        }
        else
        {
            trornDir = Random.Range(3, 5); // �÷��̾ �ִٸ� ���� ������ ���� �ָ��ϱ�
            endWait = new WaitForSeconds(2f); // ���� ������ 2�� ��ٸ���
        }

        animator.SetTrigger("Attack1Ready"); // ���� �ڱ� �غ� �ڼ�
        audioSource.clip = attack1ReadyClip; // ����� ���
        audioSource.Play();
        for (float i = trornDir; i < trornDir * 5; i += trornDir)
        {
            Instantiate(TrornReadyParticle, transform.position + new Vector3(i, -1.4f, 0), Quaternion.Euler(90, 0, 0));
            Instantiate(TrornReadyParticle, transform.position + new Vector3(-i, -1.4f, 0), Quaternion.Euler(90, 0, 0));
        }
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Attack1"); // ���� �ڱ� �ڼ�
        audioSource.clip = attack1Clip; // ����� ���
        audioSource.Play();
        StartCoroutine(shake.ShakeCamera(0.05f, 0.1f)); // ī�޶� ������ ��
        for (float i = trornDir; i < trornDir * 5; i += trornDir)
        {
            Instantiate(TrornParticle, transform.position + new Vector3(i, -1.4f, 0), Quaternion.Euler(-90, 0, 0));
            Instantiate(TrornParticle, transform.position + new Vector3(-i, -1.4f, 0), Quaternion.Euler(-90, 0, 0));
        }
        yield return endWait; // end Wait�� ��ٸ���
        StartCoroutine(Think()); // ���� ���� �����
    }
    public override void OnDamage(float damage)
    {
        if (!dead)
        {
            base.OnDamage(damage);
        }

    }
    public override void Die()
    {
        base.Die();
        StopAllCoroutines();

        StartCoroutine(Dead());
    }
    IEnumerator Dead()
    {
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == target) // �÷��̾�� ��Ҵٸ�
        {
            LivingEntity attackTarget = target.GetComponent<LivingEntity>(); // �÷��̾��� LivingEntity ������Ʈ ã��
            attackTarget.OnDamage(damage); // �÷��̾�� damage ����ŭ ������ ������
        }

        if (collision.contacts[0].normal.y > 0.7f && isJumpAttack) // ���� �������̰� �����ߴٸ�
        {

            shake.ShakeCamera(0.2f, 0.1f); // ȭ������
            isLook = false;
            rigid.gravityScale = 2f; // �߷°� ����
            for (float i = 0; i <= 180; i += 180) // �翷�� �Ҳ� �߻� 
            {
                Instantiate(button, transform.position + new Vector3(0, -0.4f, 0), Quaternion.Euler(0, 0, i));
                audioSource.clip = jumpAttackClip; // ����� ���
                audioSource.Play();
                audioSource.clip = attack3Clip; // ����� ���
                audioSource.Play();
            }
            StartCoroutine(Think());
            isJumpAttack = false;
        }
    }
}
