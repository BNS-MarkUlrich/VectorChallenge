using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private float maxTravelDistance = 100f;
    private Vector3 predictionVelocity;

    void LateUpdate()
    {
        var travelDistance = Vector3.Distance(transform.position, origin.transform.position);
        if (travelDistance >= maxTravelDistance)
        {
            Destroy(gameObject);
            return;
        }
        
        Launch(predictionVelocity);
    }

    public override void InitBullet(Turret newOrigin)
    {
        base.InitBullet(newOrigin);
        predictionVelocity = origin.PredictedVelocity;
    }
}
