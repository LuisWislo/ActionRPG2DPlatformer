using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossScript : MonoBehaviour
{
    private EnemyBeing self;
    private GameObject ply, leg0, leg1;
    private int part2, part3;
    private bool shootEye, spawnRing, changeDir, in_2, in_3;
    private AudioManager audio;

    public GameObject eye;
    public GameObject ring;
    public float eyeCooldown, ringCooldown;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<EnemyBeing>();
        ply = GameObject.Find("Player");
        leg0 = GameObject.Find("leg_0");
        leg1 = GameObject.Find("leg_1");
        part2 = Mathf.FloorToInt(self.health * 0.75f);
        part3 = Mathf.FloorToInt(self.health * 0.4f);
        shootEye = true;
        spawnRing = false;
        changeDir = false;
        audio = FindObjectOfType<AudioManager>();
        audio.Stop("MenuSong");
        audio.Stop("SettingsSong");
        audio.Play("FirstBoss");
        in_2 = false;
        in_3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!in_2 && self.health <= part2)
        {
            Instantiate(self.deathParticle, leg0.transform.position, Quaternion.identity);
            Destroy(leg0);
            spawnRing = true;
            eyeCooldown = eyeCooldown / 2;
            in_2 = true;
        } else if (!in_3 && self.health <= part3)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            Instantiate(self.deathParticle, leg1.transform.position, Quaternion.identity);
            Destroy(leg1);
            changeDir = true;
            in_3 = true;
        }

        if (shootEye)
        {
            Instantiate(eye, new Vector3(10, Random.Range(ply.transform.position.y - 2, ply.transform.position.y + 2)), Quaternion.Euler(0, 0, 90));
            shootEye = false;
            StartCoroutine(ShootEye());
        }

        if (spawnRing)
        {
            Instantiate(ring, new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0), Quaternion.identity);
            spawnRing = false;
            StartCoroutine(SpawnRing());
        }

        if (in_3 && changeDir)
        {
            changeDir = false;
            StartCoroutine(ChangeDirection());
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (in_3)
        {
            StopCoroutine(ChangeDirection());
            changeDir = false;
            StartCoroutine(ChangeDirection());
        }
    }

    IEnumerator ShootEye()
    {
        yield return new WaitForSeconds(eyeCooldown);
        shootEye = true;
    }

    IEnumerator SpawnRing()
    {
        yield return new WaitForSeconds(ringCooldown);
        spawnRing = true;
    }

    IEnumerator ChangeDirection()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)), ForceMode2D.Impulse);
        yield return new WaitForSeconds(2);
        changeDir = true;
    }
}
