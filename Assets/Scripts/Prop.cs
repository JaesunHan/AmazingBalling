using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int scroe = 5;
    public ParticleSystem explosionParticle;

    public float hp = 10f;

    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp < 0)
        {
            ParticleSystem instance = Instantiate(explosionParticle,  transform.position, transform.rotation);

            AudioSource explosionAudio = instance.GetComponent<AudioSource>();
            explosionAudio.Play(); 


            Destroy(instance.gameObject, instance.duration);
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
