using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelGenerator : MonoBehaviour
{
    public float minYSpacing;
    public float maxYSpacing;

    public int maxInScene;

    public Obstacle[] obstacles;

    private List<GameObject> obstaclesInScene = new List<GameObject>();

    private float startMaxYSpacing;

    void Start()
    {
        startMaxYSpacing = maxYSpacing;
    }

    // Update is called once per frame
    void Update()
    {
        if (maxYSpacing > minYSpacing)
        {
            maxYSpacing = startMaxYSpacing - (transform.position.y / 20);
        }
        for (int i = obstaclesInScene.Count - 1; i > -1; i--)
        {
            if (obstaclesInScene[i] == null)
                obstaclesInScene.RemoveAt(i);
        }
        if(obstaclesInScene.Count < maxInScene)
        {
            CreateNewObstacle();
        }
    }

    public void CreateNewObstacle()
    {
        int randOb = Random.Range(0, obstacles.Length);
        float randX = Random.Range(obstacles[randOb].xNMin, obstacles[randOb].xNMax);
        float randY = Random.Range(maxYSpacing - 4, maxYSpacing);
        Vector3 randomPos = new Vector3(randX, randY + (obstacles[randOb].ySpace / 2));
        if(Random.Range(0,2) == 1)
        {
            randomPos = new Vector3(-randomPos.x, randomPos.y);
        }
        transform.position += randomPos;
        GameObject newOb = Instantiate(obstacles[randOb].prefab, transform.position, Quaternion.identity);
        transform.position = new Vector2(0, transform.position.y + (obstacles[randOb].ySpace / 2));
        obstaclesInScene.Add(newOb);
    }
}

[System.Serializable]
public class Obstacle
{
    public GameObject prefab;
    public float ySpace;
    public float xNMin;
    public float xNMax;
}
