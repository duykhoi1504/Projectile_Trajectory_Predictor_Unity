using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    //    private List<BulletSlot> bulletSlots;
    [SerializeField] private BulletSO bulletSO;
    [SerializeField] private bool canDraw = true;
    [SerializeField] private BulletType currentBulletType;
    [Header("Text Line On Scene")]

    [SerializeField] Transform startPoint;
    [SerializeField] Transform tartgetPoint;



    public Bullet GetBulletSlot(BulletType bulletType)
    {
        foreach (BulletSlot slot in bulletSO.BulletSlots)
        {
            if (slot.bulletType == bulletType)
            {
                return slot.bullet;
            }
        }
        return null;
    }
    public void SpawmBullet(Vector3 start, Vector3 end, BulletType bulletType)
    {
        currentBulletType = bulletType;
        Bullet bullet = GetBulletSlot(bulletType);
        Bullet bulletSpawn = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletSpawn.init(start, end);

    }
    private void Start()
    {
        // bulletSlots=bulletSO.BulletSlots;
        if (startPoint == null)
        {
            startPoint = this.transform;
        }
    }
    IEnumerator DelayLine(float duration)
    {
        canDraw = false;
        yield return new WaitForSeconds(duration);
        canDraw = true;
    }

    private void OnDrawGizmos()
    {

        if (!canDraw) return;
        if (!startPoint || !tartgetPoint) return;
        GetBulletSlot(currentBulletType).DrawGizmos(startPoint.position, tartgetPoint.position);
    }
}
