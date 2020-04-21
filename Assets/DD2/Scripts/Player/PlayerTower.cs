using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DD2.SOArchitecture;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] GameObject buildTowerPrefab;
    [SerializeField] GameObject towerPrefab;
    [SerializeField] Transform camera;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask towerMask;

    [SerializeField] Vector3Variable lookInput;
    [SerializeField] float rotationSensitivity;

    PlayerStats playerStats;

    Vector3 towerPosition;
    [SerializeField] bool moveBuild;
    [SerializeField] bool rotateBuild;
    [SerializeField] bool enableBuild;
    Transform tower;


    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public void BuildTower()
    {
        if (tower)
        {
            return;
        }
        tower = Instantiate(buildTowerPrefab).transform;
        moveBuild = true;
        rotateBuild = false;
        enableBuild = false;
        towerPosition = Vector3.zero;
    }

    public void ConfirmBuild()
    {
        if (moveBuild)
        {
            if (enableBuild)
            {
                moveBuild = false;
                rotateBuild = true;
            }
        }
        else if (rotateBuild)
        {
            Transform builtTower = Instantiate(towerPrefab).transform;
            builtTower.position = towerPosition;
            builtTower.forward = tower.forward;
            Destroy(tower.gameObject);
            tower = null;
        }
    }

    void BuildPosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, playerStats.GetBuildRange() + 3, groundMask))
        {
            towerPosition = hit.point;
            Collider[] colliders = Physics.OverlapSphere(tower.position, 1, towerMask);
            if (colliders.Length > 0)
            {
                //Make tower red
                enableBuild = false;
            }
            else
            {
                enableBuild = true;
            }
        }
        else
        {
            towerPosition = Vector3.down * 1000;
        }
        tower.position = towerPosition;
    }

    void BuildRotation()
    {
        Quaternion rotation = tower.rotation; 
        rotation *= Quaternion.Euler(Vector3.up * lookInput.Value.x * rotationSensitivity * Time.deltaTime);
        tower.rotation = rotation;
    }

    void Update()
    {
        if (tower)
        {
            if (moveBuild)
            {
                BuildPosition();
                rotateBuild = false;
            }

            if (rotateBuild)
            {
                BuildRotation();
                moveBuild = false;
            }
            else
            {
                tower.forward = transform.forward;
            }
        }
        else
        {
            enableBuild = false;
        }
    }
}
