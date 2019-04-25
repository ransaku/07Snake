using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHeads : MonoBehaviour
{

    public int step;
    private int x;//代表蛇头移动距离的增量值。
    private int y;//代表蛇头移动距离的增量值。
    private Vector3 headPos;
    public float velocity = 0.35f;//调用间隔参数来表示速度

    public List<Transform> bodyList = new List<Transform>();//设置一个List来存储蛇身的Transform
    public GameObject bodyPrefab;//获取需要生成蛇身的prefab
    public Sprite[] bodySprites=new Sprite[2];//定义一个数组存放蛇身图片
    public Transform canvas;

    
    
    void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;
    }

    void Start()
    {
        InvokeRepeating("Move",0,velocity);

        x = 0;
        y = step;//设置蛇预设行走方向向上。
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)&& y!=-step)
        {//向上走
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
            //蛇头方向默认
            x = 0;
            y = step;
        }
        if (Input.GetKey(KeyCode.A)&&x!=step)
        {//向左走   
            gameObject.transform.rotation= Quaternion.Euler(0,0,90);
            //蛇头贴图旋转为向左
            x = -step;
            y = 0;
        }
        if (Input.GetKey(KeyCode.S)&&y!=step)
        {//向下走
            gameObject.transform.rotation= Quaternion.Euler(0,0,180);
            //蛇头贴图旋转为向下
            x = 0;
            y = -step;
        }
        if (Input.GetKey(KeyCode.D)&&x!=-step)
        {//向右走
            gameObject.transform.rotation= Quaternion.Euler(0,0,-90);
            //蛇头贴图旋转为向右
            x = step;
            y = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
           CancelInvoke();//先取消当前状态的速度
           InvokeRepeating("Move",0,velocity-0.2f);     
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke();//先取消当前状态的速度
            InvokeRepeating("Move",0,velocity);
        }
    }

    void Move()
    {
        headPos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(headPos.x+x,headPos.y+y,headPos.z);
        
        if (bodyList.Count>0)//避免空指针问题先进行判断
        {
            //控制蛇身跟随蛇头移动，先将最后一个节点放到移动前蛇头的位置，再删除蛇尾节点
//            bodyList.Last().localPosition = headPos;
//            bodyList.Insert(0,bodyList.Last());
//            bodyList.RemoveAt(bodyList.Count-1);

            for (int i = bodyList.Count-2; i >=0; i--)
            {
                bodyList[i + 1].localPosition = bodyList[i].localPosition;
            }

            bodyList[0].localPosition = headPos;
        }
     
    }

    void Grow()
    {
        int index = (bodyList.Count % 2 == 0) ? 0 : 1;
        GameObject body =Instantiate(bodyPrefab,new Vector3(1000,1000,0),Quaternion.identity);//实例化一个蛇身
        body.GetComponent<Image>().sprite = bodySprites[index];//在蛇身的sprite组件上添加相应序号的图片
        body.transform.SetParent(canvas,false);//将生成的蛇身跟蛇头放在同一级目录下（canvas下面）
        bodyList.Add(body.transform);//更新生成身体后的transform

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //也可以使用collision.tag =="Food"
        if (collision.gameObject.CompareTag("Food"))//吃到食物时
        {
            Destroy(collision.gameObject);//不可写成Destroy(collision),这样就只销毁碰撞器不销毁游戏物体
            GameController.Instance.UpdateUI();
            Grow();//蛇身增长
            FoodMaker.Instance.FoodPop(Random.Range(0, 100) < 20 ? true : false);

        }else if (collision.gameObject.CompareTag("Reward"))//吃到奖励时
        {
            Destroy(collision.gameObject);
            GameController.Instance.UpdateUI(100);
            Grow();
        }
        else if (collision.gameObject.CompareTag("Body"))//撞到自身时
        {
            Debug.Log("DIE");
        }
        else
        {//撞到边界时
            switch (collision.gameObject.name)
            {
                case "Up":
                    transform.localPosition = new Vector3(transform.localPosition.x,-transform.localPosition.y+20,transform.localPosition.z);
                        break;
                case "Down":
                    transform.localPosition = new Vector3(transform.localPosition.x,-transform.localPosition.y-35,transform.localPosition.z);
                        break;
                case "Left":
                    transform.localPosition = new Vector3(-transform.localPosition.x+190,transform.localPosition.y,transform.localPosition.z);
                        break;
                case "Right":
                    transform.localPosition = new Vector3(-transform.localPosition.x+245,transform.localPosition.y,transform.localPosition.z);
                        break;
                   
            }

        }
    }

}
