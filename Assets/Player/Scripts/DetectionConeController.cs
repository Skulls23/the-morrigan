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


    public GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        int colliderId = other.GetComponentInParent<Enemy>().GetId();
        foreach(int id in enemyIds)
        {
            if (colliderId == id)
            {
                Debug.Log("Enemy " + id + " AlreadyInList");
                return;
            }    
        }
        enemiesList.Add(other.GetComponentInParent<Enemy>().gameObject);
        enemyIds.Add(colliderId);
    }

    private void OnTriggerExit(Collider other)
    {
        int colliderId = other.GetComponentInParent<Enemy>().GetId();
        for(int i =0; i< enemyIds.Count;i++)
        {
            if (colliderId == enemyIds[i])
            {
                enemiesList.RemoveAt(i);
                enemyIds.RemoveAt(i);
                return;
            }    
        }
    }


    public GameObject SelectTarget(Vector2 joysticDir, GameObject obj = null)
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
                int enemyIndex = checkNearestDirection(joysticDir, Camera.transform.position, false);
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
        float angle = -1;
        float distance = 0;

        for (int i = 0; i < enemyIds.Count; i++)
        {
            Vector3 direction = Vector3.zero;
            Vector3 enemyPosition = enemiesList[i].transform.position;
            Vector3 enemyDir = new Vector3(enemyPosition.x - startPos.x, 0, enemyPosition.z - startPos.z);
            enemyDir.Normalize();

            if (isAlreadyLocked)
            {
                if (enemyIds[i] == lockedEnemyId)
                {
                    continue;
                }   
                direction = new Vector3(joysticDir.x, 0, joysticDir.y);              
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
                Debug.Log(enemiesList[i].name + " has an angle of " + angle + ", no other enemies, he gets locked");
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
