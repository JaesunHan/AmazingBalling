using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public LayerMask whatIsProp;  

    public ParticleSystem explosionParticle;
    public AudioSource explosionAudio;

    public float maxDamage = 100f;
    public float explosionForce = 1000f;
    public float lifTime = 10f;
    public float explosionRadius = 20f;

    private void Start()
    {
        Destroy(gameObject, lifTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        //가상의 구를 그려서 그 안의 모든 콜라이더들을 전부 가져온다.
        //Physics.OverlapSphere(transform.position, explosionRadius);
        //마지막에 레이어마스크를 옵션으로 주게 되면 해당 레이어의 콜라이더들만 전부 가져온다.
        Collider[] colliders =  Physics.OverlapSphere(transform.position, explosionRadius, whatIsProp);

        for (int i = 0; i < colliders.Length; i++)
        {
            var targetRigidBoy = colliders[i].GetComponent<Rigidbody>();

            //어떤 지점의 폭발의 위치와, 폭발력, 폭발 반경을 지정해주면, 자신의 위치가 해당 위치까지의 거리를 계산해서
            //스스로 튕겨나가는 작용이 됨
            targetRigidBoy.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            Prop targetProp = colliders[i].GetComponent<Prop>();

            float damage = CalculateDamage(colliders[i].transform.position);
            targetProp.TakeDamage(damage);
        }
        


        explosionParticle.transform.parent = null;

        explosionParticle.Play();
        explosionAudio.Play();

        GameManager.instance.OnBallDestroy();

        Destroy(explosionParticle.gameObject, explosionParticle.duration);
        Destroy(gameObject);


    }

    /// <summary>
    /// 거리에 따라 데미지를 차등으로 적용한다.
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;
        float distance = explosionToTarget.magnitude;

        float edgeToCenterDistance = explosionRadius - distance;
        float percentage = edgeToCenterDistance / explosionRadius;

        float damage = maxDamage * percentage;
        //매개변수로 받은 두 수중 가장 큰 값이 반환된다.
        damage = Mathf.Max(0, damage);

        return damage;
    }
}
