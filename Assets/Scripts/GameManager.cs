using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public ParticleSystem explosion;
    public float respawnTime = 3f;
    public float respawnInvulnerabilityTime = 3f;
    public int lives = 3;
    public int score = 0;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            DestroyImmediate(this.gameObject);
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if (asteroid.size < 0.75f)
            score += 100;
        else if (asteroid.size < 1.2f)
            score += 50;
        else
            score += 25;
    }
    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        this.lives--;

        if (this.lives <= 0)
            GameOver();
        else
            Invoke(nameof(Respawn), this.respawnTime);
    }

    private void GameOver()
    {
        this.lives = 3;
        this.score = 0;
    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollision), respawnInvulnerabilityTime);
    }

    private void TurnOnCollision()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
