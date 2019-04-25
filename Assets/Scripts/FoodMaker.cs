using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FoodMaker : MonoBehaviour
{
    //单例模式
    private static FoodMaker _instance;
    public static FoodMaker Instance
    {
        get { return _instance; }
    }
    
    
    //设置生成食物的上下最大范围
    public int xBorder = 20;
    public int yBorder = 12;
    //利用offset来处理初始位置的左右坐标偏差
    public int xOffset = 7;
    public int yOffset = 4;

    //获取需要生成的食物的prefab
    public GameObject foodPrefab;
    //定义一个存放食物图片的数组
    public Sprite[] foodSprites;
    private Transform foodHolder;
    
    public GameObject rewardPrefab;//获取奖励物品的prefab

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        foodHolder = GameObject.Find("FoodHolder").transform;
        FoodPop(false);
    }

    public void FoodPop(bool isReward)
    {
        //生成一个食物贴图的索引值
        int index = Random.Range(0, foodSprites.Length);
        //实例化一个预制体
        GameObject food = Instantiate(foodPrefab);
        //给食物的image组件贴图赋值
        food.GetComponent<Image>().sprite = foodSprites[index];
        //将食物生成在FoodHolder组件上
        food.transform.SetParent(foodHolder,false);//false 不自动转换坐标
        
        //计算并设置生成食物的坐标
        int x = Random.Range(-xBorder + xOffset, xBorder);
        int y = Random.Range(-yBorder + yOffset, yBorder);
        food.transform.localPosition = new Vector3(x*30,y*30,0);

        if (isReward)
        {
            //实例化一个预制体
            GameObject reward = Instantiate(rewardPrefab);
            //将奖励生成在FoodHolder组件上
            reward.transform.SetParent(foodHolder,false);//false 不自动转换坐标
            //计算并设置生成奖励的坐标
            x = Random.Range(-xBorder + xOffset, xBorder);
            y = Random.Range(-yBorder + yOffset, yBorder);
            reward.transform.localPosition = new Vector3(x*30,y*30,0);
        }
    }


}
