﻿using UnityEngine;
using System.Collections;

public class EvadeTrackingMissile : BaseGame
{
    public GameObject hero;
    public GameObject missile;
    public GameObject ceilingLimit;
    public GameObject floorLimit;
    public GameObject edgeLimit;
    float timer = float.MaxValue;

    public override void Start()
    {
        base.Start();

        ceilingLimit = CreateHorizontalLimit(ceilingLimit, 1);
        floorLimit = CreateHorizontalLimit(floorLimit, -1);
        edgeLimit = CreateVertitalLimit(edgeLimit);

        hero = CreateGameObject(hero);
        hero.GetComponent<WASDController>().isLeft = isLeft;
        hero.GetComponent<WASDController>().gameController = this;
    }

    public override void Update()
    {
        if (destroy || gameover)
            return;

        base.Update();

        if (timer >= 6f * Mathf.Pow(0.9f, difficulty))
        {
            timer = Random.Range(-0.1f - 4f * Mathf.Pow(0.9f, difficulty), -0.1f);
            CreateMissile(Mathf.Clamp(Mathf.Pow(1.05f, difficulty), 1f, 10f), Random.Range(0.01f, 0.05f));
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public void CreateMissile(float speed, float flexibility)
    {
        Vector3 missilePos;
        do
        {
            missilePos = randEdgePosition();
        } while ((missilePos - hero.transform.position).magnitude < 1f);

        var missileScript = ((GameObject)Instantiate(missile, missilePos, missile.transform.rotation)).GetComponent<MissileMove>();
        missileScript.gameController = this;
        missileScript.speed = speed;
        missileScript.flexibility = flexibility;
        missileScript.heroTransform = hero.transform;
    }

    Vector3 randEdgePosition()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                return new Vector3(Random.Range(startX, endX), startY + 0.15f);
            case 1:
                return new Vector3(Random.Range(startX, endX), endY - 0.15f);
            case 2:
                return new Vector3(startX + 0.15f, Random.Range(startY, endY));
            default:
                return new Vector3(endX - 0.15f, Random.Range(startY, endY));
        }
    }

    public override void End()
    {
        destroy = true;
        Destroy(floorLimit);
        Destroy(ceilingLimit);
        Destroy(edgeLimit);
        Destroy(hero);
        Destroy(gameObject);
    }
}
