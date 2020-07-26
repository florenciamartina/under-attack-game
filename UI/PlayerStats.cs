using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerStats : MonoBehaviour {
    protected static int nutrigems = 0;
	private int collectibles;

	[Header("UI")]
	[SerializeField] protected TextMeshProUGUI nutrigemsNumber;
	[SerializeField] private SpecialCollectibles specialCollectiblesUI;
	[SerializeField] private GameObject gameOverUI;

	[SerializeField] private int maxHealth = 100;
	private float currHealth;
	[SerializeField] private HealthBar healthBar;
	private int sceneIndex;

	[Header("Powerups")]
	[SerializeField] private GameObject shield;
	private bool isNKC = false;

	[Header("Sound FX")]
	[SerializeField] private AudioSource nutrigemSound;
	[SerializeField] private AudioSource collectibleSound;

	private PlayerMovement player;

	private int level;

	private void Awake() {
		player = GetComponent<PlayerMovement>();
	}

	protected virtual void Start() {
		if (gameOverUI != null) gameOverUI.SetActive(false);
		Time.timeScale = 1f;

		currHealth = maxHealth;
		if (healthBar != null) healthBar.SetMaxHealth(maxHealth);
		nutrigems = 0;
		
		sceneIndex = SceneManager.GetActiveScene().buildIndex;
		level = SaveLoad.GetLevel();
		Collectibles(level);

		if (specialCollectiblesUI != null) {
			specialCollectiblesUI.Add(collectibles);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.Equals("Collectible")) {			
			if (other.GetComponent<Nutrigem>() != null) {
            	nutrigems += 1;
				nutrigemsNumber.text = nutrigems.ToString();
				nutrigemSound.Play();

			} else if (other.GetComponent<SpecialCollectible>() != null) {
				collectibleSound.Play();

				collectibles++;
				specialCollectiblesUI.Add(collectibles);
			}

			Destroy(other.gameObject);
		} else if (other.gameObject.tag == "DeathZone") {

			SaveLoad.Dead();
			TakeDamage(1000);

		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag("Enemy")) {

			if (isNKC) {
				other.gameObject.GetComponent<Enemy>().Death();
				return;
			}
			
			int damage = other.gameObject.GetComponent<Enemy>() != null
				? other.gameObject.GetComponent<Enemy>().getDamage()
				: other.gameObject.GetComponent<Bullet_Enemy>().getDamage();

			TakeDamage(damage);
		}
	}

	private void OnDestroy() {
		Debug.Log("Player destroyed.");
	}

	public static int GetNutrigems() {
		return nutrigems;
	}

	public static void Purchase(int price) {
		nutrigems -= price;
	}

	public void TakeDamage(float damage)
	{
		if (shield.activeSelf)
		{
			Debug.Log("Shield active");
			shield.GetComponent<Shield>().TakeDamage(damage);
		}
		else
		{
			currHealth -= damage;
			if (healthBar != null) healthBar.SetHealth(currHealth);
			player.Hurt();
		}

		if (currHealth <= 0)
		{
			SaveLoad.Dead();
			if (gameOverUI != null)
			{
				Time.timeScale = 0f;
				gameOverUI.SetActive(true);
			}
			else
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

	private void Collectibles(int level) {
		Debug.Log(level);
		if (level == 1) {
			collectibles = sceneIndex - SaveLoad.minLevel1;
		} else if (level == 2) {
			collectibles = sceneIndex - (SaveLoad.minLevel2 + 1);
		} else {
			collectibles = sceneIndex - (SaveLoad.minLevel3); //+ 1);
		}
	}

	public void ActivateNKC() {
		isNKC = true;
	}

	public void DeactivateNKC() {
		isNKC = false;
	}

}
