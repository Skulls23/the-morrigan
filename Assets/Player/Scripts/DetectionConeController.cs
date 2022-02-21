using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionConeController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemiesList;
    [SerializeField]
    private List<int> enemyIds;

    [SerializeField]
    private int lockedEnemyId;
    int nearestDirectionId = 0;

    int index;
    Vector2 joyDir;

    [SerializeField]
    private int RayRangeDebugger;
    public GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (enemiesList.Count > 0)
        {
            Debug.DrawRay(enemiesList[lockedEnemyId].transform.position, joyDir * 20, Color.blue);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Enemy>())
        {
            int colliderId = other.GetComponentInParent<Enemy>().GetId();
            foreach (int id in enemyIds)
            {
                if (colliderId == id)
                {
                    return;
                }
            }
            enemiesList.Add(other.GetComponentInParent<Enemy>().gameObject);
            enemyIds.Add(colliderId);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Enemy>())
        {
            int colliderId = other.GetComponentInParent<Enemy>().GetId();
            RemoveEnemyFromPool(colliderId);
        }
    }

    public void RemoveEnemyFromPool(int id)
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            if (id == enemyIds[i])
            {
                enemiesList.RemoveAt(i);
                enemyIds.RemoveAt(i);
                return;
            }
        }
    }


    public GameObject SelectTarget(Vector2 joysticDir = new Vector2(), GameObject obj = null)
    {
        GameObject enemySelected = null;
        //If the player not currently Locked
        if (obj == null)
        {
            if (enemyIds.Count == 0)
            {
                return null;
            }

            else if (enemyIds.Count == 1)
            {
                return enemiesList[0];
            }
            else
            {
                int enemyIndex = checkNearestDirection(Camera.transform.forward, Camera.transform.position, false);
                enemySelected = enemiesList[enemyIndex];
            }           
        }
        //If the player is already Locked
        else
        {
            enemySelected = enemiesList[checkNearestDirection(joysticDir, obj.transform.position, true)];
        }
        return enemySelected;
    }

    int checkNearestDirection(Vector2 joysticDir, Vector3 startPos, bool isAlreadyLocked)
    {
        Debug.Log("CheckNearestDirection");
        float angle = -1;
        float distance = 0;

        enemiesList[lockedEnemyId].GetComponent<Enemy>().isLocked = true;

        Vector3 temp = new Vector3(joysticDir.x, 0 ,joysticDir.y);
        Vector3 direction = enemiesList[lockedEnemyId].GetComponent<Enemy>().LockPoint.transform.TransformDirection(temp);
        Debug.DrawRay(enemiesList[lockedEnemyId].GetComponent<Enemy>().LockPoint.transform.position, -direction * RayRangeDebugger, Color.red, 2);


        for (int i = 0; i < enemiesList.Count; i++)
        {
            direction = Vector3.zero;
            Vector3 enemyPosition = enemiesList[i].transform.position;
            Vector3 enemyDir = new Vector3(enemyPosition.x - startPos.x, 0, enemyPosition.z - startPos.z);
            enemyDir.Normalize();

            if (isAlreadyLocked)
            {
                if (enemyIds[i] == lockedEnemyId)
                {
                    continue;
                }
                temp = new Vector3(joysticDir.x, 0, joysticDir.y);
                direction = transform.InverseTransformDirection(temp);
                
                joyDir = joysticDir;
            }
            else
            {
                direction = new Vector3(Camera.transform.forward.x, 0, Camera.transform.forward.z);
            }
            float tempAngle = Mathf.Abs(Vector3.SignedAngle(direction, enemyDir, Vector3.up));
            if (angle == -1 && tempAngle <90)
            {
                distance = Vector3.Distance(startPos, enemiesList[i].transform.position);
                angle = tempAngle;
                nearestDirectionId = i;
                //Debug.Log(enemiesList[i].name + " has an angle of " + angle + ", no other enemies, he gets locked");
            }
            else
            {
                tempAngle = Mathf.Abs(Vector3.SignedAngle(direction, enemyDir, Vector3.up));
                Debug.Log(enemiesList[i].name + " has an angle of " + tempAngle + " vs " + angle);
                if (tempAngle <= angle && tempAngle < 90)
                {
                    if(Mathf.Abs(tempAngle-angle) < 1)
                    {
                        float tempDistance = Vector3.Distance(startPos, enemiesList[i].transform.position);
                        if(tempDistance < distance)
                        {
                            angle = tempAngle;
                            nearestDirectionId = i;
                            continue;
                        }
                        continue;
                    }
                    angle = tempAngle;
                    nearestDirectionId = i;
                }
                
            }
        }

        lockedEnemyId = enemyIds[nearestDirectionId];
        Debug.Log(enemiesList[nearestDirectionId] + " has been chosen ");
        Debug.Log("The id of the locked enemy is" + enemyIds[nearestDirectionId]);       
        return nearestDirectionId;
    }
}
