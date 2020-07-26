using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour{

    private Animator anim;

    [Header("Laser")]
    [SerializeField] private LineRenderer lineRenderer;
    private LineRenderer[] lineRenderers;
    [SerializeField] private float laserWidth = 3f;
    [SerializeField] private float laserLength = 30f;

    private float[] x;
    private float[] y;

    private float[] angle;

    [SerializeField] private float dps;

    [SerializeField] private float attackTime = 3f;
    [SerializeField] private float attackInterval = 5f;
    private float currTime;
    private bool isAttack = false;

    private bool canAttack = true;
    private bool canDamage = false;
    private RaycastHit2D[] hits;

    [Header("Movement")]

    [SerializeField] private float upCap;
    [SerializeField] private float downCap;
    [SerializeField] private float speed = 3f;
    private float target;
    private bool isMoving;

    [Header("Health")]

    [SerializeField] private int maxHealth = 500;
    private int currHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ParticleSystem attackEffect;
    [SerializeField] private AudioSource deadSound;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();

        if (healthBar != null) healthBar.SetMaxHealth(maxHealth);
        currHealth = maxHealth;

        lineRenderers = new LineRenderer[5];
        for (int i = 0; i < 5; i++) {
            lineRenderers[i] = Instantiate(lineRenderer);
        }

        // DeactivateLasers();
        foreach(LineRenderer laser in lineRenderers) {
            laser.gameObject.SetActive(false);
            laser.startWidth = laserWidth;
            laser.endWidth = laserWidth;
        }

        x = new float[]{laserLength, laserLength, 0, -laserLength, -laserLength};
        y = new float[]{0, laserLength, laserLength, laserLength, 0};
        angle = new float[]{0, 45, 90, 135, 180};

        currTime = 0;
        isMoving = false;
    }

    // Update is called once per frame
    void Update() {

        if (!isAttack) Move();

        if (currTime <= 0 && canAttack) {
            StartCoroutine(Attack());
        } else {
            currTime -= Time.deltaTime;
        }

        if (isAttack) {
            anim.SetTrigger("Attack");
            // ActivateLasers();
            if (canDamage) Damage();
        }
    }

    private void Move() {
        if (transform.position.y == upCap || transform.position.y == downCap) isMoving = false;

        if (!isMoving) {
            target = transform.position.y < upCap ? upCap : downCap;
            isMoving = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, 
                                new Vector2(transform.position.x, target), speed * Time.deltaTime); 
    }

    public void TakeDamage(int damage) {
        currHealth -= damage;
        if (healthBar != null) healthBar.SetHealth(currHealth);

        // if (attackEffect != null) Instantiate(attackEffect);

        if (currHealth <= 0) {
            Death();
        }
    }

    private IEnumerator Attack() {

        Debug.Log("Attack");
        isAttack = true;
        currTime = attackInterval;

        // ActivateLasers();

        yield return new WaitForSeconds(attackTime);
        
        isAttack = false;
        DeactivateLasers();
    }

    private void Death() {
        DeactivateLasers();
        Destroy(healthBar.gameObject);

        speed = 0f;
        canAttack = false;

        deadSound.Play();
        Destroy(gameObject, 2f);
    }

    private void ActivateLasers() {
        int i = 0;
        foreach(LineRenderer laser in lineRenderers) {
            laser.gameObject.SetActive(true);
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, transform.position + new Vector3(x[i], y[i], 0));
            i++;
        }

        canDamage = true;
    }

    private void Damage() {
        for(int i = 0; i < lineRenderers.Length; i++) {
            hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.5f * laserWidth, laserLength), angle[i], Vector2.right);
            foreach(RaycastHit2D hit in hits) {
                if (hit.transform.gameObject.CompareTag("Player")) {
                    if (hit.transform.position.y < transform.position.y - laserWidth / 2) continue;
                    hit.transform.gameObject.GetComponent<PlayerStats>().TakeDamage(dps);
                }
            }

            Debug.DrawRay(transform.position, new Vector3(x[i], y[i], 0), Color.red);
        }
    }

    private void DeactivateLasers() {
        anim.ResetTrigger("Attack");
        canDamage = false;
        isAttack = false;
        hits = null;
        foreach(LineRenderer laser in lineRenderers) {
            laser.gameObject.SetActive(false);
        }
    }
}
