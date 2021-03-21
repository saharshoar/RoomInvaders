using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshCarve : MonoBehaviour
{
    public NavMeshObstacle meshObstacle;

    // Start is called before the first frame update
    void Start()
    {
        meshObstacle = GetComponent<NavMeshObstacle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateNav()
    {
        meshObstacle.carving = false;
    }
}
