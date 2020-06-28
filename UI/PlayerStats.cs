using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerStats : MonoBehaviour {
    protected static int nutrigems = 0;
	private int mucins;
	private int GP1b;

	[Header("UI")]
	[SerializeField] protected TextMeshProUGUI nutrigemsNumber;
	[SerializeField] private SpecialCollectibles specialCollectiblesUI;

	private int maxHealth = 100;
	private int currHealth;
	[SerializeField] private HealthBar healthBar;
	private int sceneIndex;

	[Header("Powerups")]
	[SerializeField] private GameObject shield;

	[Header("Sound FX")]
	[SerializeField] private AudioSource nutrigemSound;
	[SerializeField] private AudioSource collectibleSound;

	private PlayerMovement player;

	private void Awake() {
		player = GetComponent<PlayerMovement>();
	}

	protected virtual void Start() {
		currHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		nutrigems = 0;
		sceneIndex = SceneManager.GetActiveScene().buildIndex;
		mucins = sceneIndex - 3;
		GP1b = sceneIndex - 7;
		
		if (mucins >= 3) {
			specialCollectiblesUI.Add(GP1b);
		} else {
			specialCollectiblesUI.Add(mucins);
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

				if (mucins >= 3) {
					GP1b += 1;
					specialCollectiblesUI.Add(GP1b);
				} else {
					mucins += 1;
					specialCollectiblesUI.Add(mucins);
				}
			}

			Destroy(other.gameObject);
		} 
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag("Enemy")) {

			int damage = other.gameObject.GetComponent<Enemy>() != null
				? other.gameObject.GetComponent<Enemy>().getDamage()
				: other.gameObject.GetComponent<Bullet_Enemy>().getDamage();

			if (shield.activeSelf) {
				Debug.Log("Shield active");
				shield.GetComponent<Shield>().TakeDamage(damage);
			} else {
				currHealth -= damage;
				healthBar.SetHealth(currHealth);
			}
		}

		if (currHealth <= 0) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	private void OnDestroy() {
		Debug.Log("Player destroyed.");
	}

	public static int GetNutrigems() {
		return nutrigems;
	}
	
	public void TakeDamage(int damage) {
		currHealth -= damage;
		healthBar.SetHealth(currHealth);

		player.Hurt();

		if (currHealth <= 0) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	private int GetLevel() {
		int maxIndexLvl1 = 5;
		int maxIndexLvl2 = 9;

        if (sceneIndex <= maxIndexLvl1) {
            return 1;
        } else if (sceneIndex <= maxIndexLvl2) {
            return 2;
        } else {
            return 0;
        }
    }

	public int GetSpecialCollectibles() {
		if (GetLevel() == 1) {
			return mucins;
		} else if (GetLevel() == 2) {
			return GP1b;
		} else {
			return 0;
		}
	}

}
