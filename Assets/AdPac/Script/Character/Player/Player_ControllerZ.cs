using UnityEngine;
using System.Collections;

public class Player_ControllerZ : Character_Manager{

    /*
    プレイヤーの操作用
    Jump()//ジャンプ
    Move()//移動

    */

    //使うもの
    private CharacterController playerController;//キャラクタコントローラで動かす場合
    private Animator animator;//アニメーション設定用
	private Static save;

    //初期パラメタ(邪魔なのでインスペクタに表示しない)
    [System.NonSerialized]
    public int max_HP, max_MP, base_Pow, base_Def;
    [System.NonSerialized]
    public float base_Sp, base_Ju;

    void Start()
    {
        playerController = GetComponent<CharacterController>();//rigidbodyを使う場合は外す
        animator = GetComponentInChildren<Animator>();//アニメータを使うとき
		save = GameObject.FindGameObjectWithTag ("Manager").GetComponent<Static> ();

		//HPとMPの引継ぎ
		H_point = save.H_Point;
		M_point = save.M_Point;

        //初期パラメタを保存
        max_HP = H_point;
        max_MP = M_point;
        base_Pow = power;
        base_Def = def;
        base_Sp = speed;
        base_Ju = jump;

    }

    void Update()
    {
        //HPがなくなった時の処理
        if (H_point <= 0)
        {

            Application.LoadLevel(Application.loadedLevel);

        }

        //アニメーションリセット///////////////////////////////////////////////////////////
        move_direction = new Vector3(0.0f, move_direction.y, 0.0f);
        //キーボードからの入力読み込み/////////////////////////////////////////////////////

        float InputX = 0.0f;
        float InputY = 0.0f;

        Vector3 inputDirection = Vector3.zero;//入力された方向

        if (flag_move)
        {

            InputX = Input.GetAxis("Horizontal");
            InputY = Input.GetAxis("Vertical");
            inputDirection = new Vector3(InputX, 0, InputY);//入力された方向

        }

        //ジャンプ
        if (playerController.isGrounded)
        {
            flag_jump = true;
            move_direction = Vector3.zero;
            animator.SetBool("Jump", false);
            if (Input.GetButtonDown("Jump"))
            {
                if (flag_jump) { Jump(); }
                    
            }

        }

        //キャラクタの方向回転
        //this.transform.Rotate(0,Input.GetAxis("Mouse X") * character_parameter.now_speed * Time.deltaTime * 90,0);

        if(!GetF_Watch())this.transform.Rotate(0, InputX * speed * Time.deltaTime * 12, 0);//注目してたら回さない

        //キャラクタの方向を取得
        direction = transform.TransformDirection(Vector3.forward);

        //キャラクタ移動処理
        if (inputDirection.magnitude > 0.1)
        {

            _Move();
            animator.SetFloat("Speed", move_direction.magnitude);

        }
        else
        {

            animator.SetFloat("Speed", 0);

        }

        //キャラにかかる重力決定（少しふわふわさせてる）
        if (move_direction.y > -2)
        {

            
            move_direction.y += Physics.gravity.y * Time.deltaTime;

        

        }

        //キャラの移動実行（動かす方向＊動かすスピード＊補正）
        playerController.Move(move_direction * speed * Time.deltaTime);

    }

    //ジャンプ
    void Jump()
    {
        flag_jump = false;
        move_direction.y = jump;
        animator.SetBool("Jump", true);

    }

    //実際に動かす方向決定
    void _Move()
    {

        if (Input.GetKey(KeyCode.W))
        {
            move_direction.x += direction.x;
            move_direction.z += direction.z;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move_direction.x -= direction.x / 2;
            move_direction.z -= direction.z / 2;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move_direction.x -= direction.z / 10;
            move_direction.z += direction.x / 10;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move_direction.x += direction.z / 10;
            move_direction.z -= direction.x / 10;
        }
    }

}
