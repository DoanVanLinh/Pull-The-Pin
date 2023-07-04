using DG.Tweening;
using System.Collections;
using UnityEngine;
public class Bom : MonoBehaviour
{
    public float radiusExplosion;
    public float dangerExplosion;
    public float force;

    public LayerMask colorBallLayer;
    public LayerMask greyBallLayer;
    public LayerMask buckLayer;
    private bool isExplosion;

    private Collider[] colliders;

    private int length;

    public MeshRenderer mesh;
    private void Start()
    {
        isExplosion = false;
        transform.localScale = Vector3.one;
    }
    private void Explosion()
    {
        bool isIndanger = false;
        //Danger Color
        colliders = Physics.OverlapSphere(transform.position, dangerExplosion, colorBallLayer);

        length = colliders.Length;
        isIndanger = isIndanger ? isIndanger : length > 0;
        for (int i = 0; i < length; i++)
        {
            Ball ball = colliders[i].GetComponent<Ball>();
            if (ball != null)
                ball.AddForce((ball.transform.position - transform.position).normalized * force, true);
        }

        //Danger Grey
        colliders = Physics.OverlapSphere(transform.position, dangerExplosion, greyBallLayer);

        length = colliders.Length;
        isIndanger = isIndanger ? isIndanger : length > 0;
        for (int i = 0; i < length; i++)
        {
            Ball ball = colliders[i].GetComponent<Ball>();
            if (ball != null)
                ball.AddForce((ball.transform.position - transform.position).normalized * force, true);
        }

        //Color Ball
        colliders = Physics.OverlapSphere(transform.position, radiusExplosion, colorBallLayer);

        length = colliders.Length;
        for (int i = 0; i < length; i++)
        {
            Ball ball = colliders[i].GetComponent<Ball>();
            if (ball != null)
                ball.AddForce((ball.transform.position - transform.position).normalized * force, isIndanger);
        }

        //Grey Ball
        colliders = Physics.OverlapSphere(transform.position, radiusExplosion, greyBallLayer);

        length = colliders.Length;
        for (int i = 0; i < length; i++)
        {
            Ball ball = colliders[i].GetComponent<Ball>();
            if (ball != null)
                ball.AddForce((ball.transform.position - transform.position).normalized * force, isIndanger);
        }


        colliders = Physics.OverlapSphere(transform.position, radiusExplosion, buckLayer);

        length = colliders.Length;
        for (int i = 0; i < length; i++)
        {
            Buck buck = colliders[i].GetComponent<Buck>();
            if (buck != null)
                buck.Break();
        }
        Destroy(gameObject);
    }
    private void CastExplosion()
    {
        if (isExplosion) return;

        isExplosion = true;

        mesh.material.DOColor(Color.red, 1f);

        transform.DOScale(Vector3.one * 1.5f, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Explosion();
                GameManager.Instance.CamrraShake(1, 0.2f);
                DataManager.Instance.GetData().AddDailyMissionValue(EDailyMissionID.DefuseBomb, 1);

            });
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Helper.BALL_TAG) || other.gameObject.layer == LayerMask.NameToLayer(Helper.BUCK_LAYER))
        {
            CastExplosion();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer(Helper.BOM_LAYER))
        {
            CastExplosion();
            Bom otherBom = other.GetComponent<Bom>();
            if (otherBom != null)
                otherBom.CastExplosion();
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusExplosion);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dangerExplosion);
    }
#endif
}
