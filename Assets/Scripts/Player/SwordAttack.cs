using UnityEngine;
using System.Collections;
// I don`t use this script
public class SwordAtack : MonoBehaviour
{
    // функция возвращает ближайший объект из массива, относительно указанной позиции
    static GameObject NearTarget(Vector3 position, Collider2D[] array)
    {
        Collider2D current = null;
        float dist = Mathf.Infinity;

        foreach (Collider2D coll in array)
        {
            float curDist = Vector3.Distance(position, coll.transform.position);

            if (curDist < dist)
            {
                current = coll;
                dist = curDist;
            }
        }

        return current?.gameObject;
    }

    // point - точка контакта
    // radius - радиус поражения
    // layerMask - номер слоя, с которым будет взаимодействие
    // damage - наносимый урон
    // allTargets - должны-ли получить урон все цели, попавшие в зону поражения
    // Возвращает, был ли кто-то атакован. (bool)
    public static bool Action(Vector2 point, float radius, LayerMask layerMask, float damage, bool allTargets)
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, layerMask);

        if (!allTargets)
        {
            GameObject obj = NearTarget(point, colliders);
            if (obj != null && obj.GetComponent<EmemyHP>())
            {
                obj.GetComponent<EmemyHP>().GetDamage(damage);
                return true;
            }
            return false;
        }
        
        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<EmemyHP>())
            {
                hit.GetComponent<EmemyHP>().GetDamage(damage);
            }
        }
        if (colliders.Length != 0) {
            return true;
        }
        return false;
    }
}