using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour
{
    // speedを制御する
    public float speed = 10;
    public float jumpSpeed= 8.0f;
    public float upspeed = 5.0f;
    public float gravity= 20.0f;

    /*旋回関係*/
    public float angle = 1.0f;
    public float max_angle = 30.0f;

    private Vector3 moveDirection = Vector3.zero;
    //private 

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        this.transform.Rotate(0, 0, 0);
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float z = 1;
        Vector3 v = this.transform.position;
        v.x += (float)(x * 0.05) * speed;
        v.z += (float)(y * 0.05) * speed;
        //float pps = 0;

        // 重力を計算
        //v.y -= (float)(z * 0.05) * gravity;
        
        if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
        {
            transform.Rotate(new Vector3(0, 0, -5));
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
        {

            transform.Rotate(new Vector3(0, 0, 5));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 0));
           
        }

        

        if (Input.GetKey(KeyCode.Space))//スペースでジャンプ
        {
            v.y += (float)(z * 0.05) * upspeed;
        }
        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            v.y -= (float)(z * 0.05) * upspeed;
        }
        //マイクラ式降下
        */


        this.transform.position = v;

        

    }
}
