
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPCScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 6.0f;

    private CharacterController _charController;
    private Camera child;
    private float force_down;
    private Transform _transform;
    private Vector3 velocity;
    GameObject go;
    bool flag;
    MouseLook _player;
    MouseLook _player1;
    private static int controll = 1;

    void Start()
    {
        _transform = transform;
        _charController = GetComponent<CharacterController>();
        child = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        flag = false;
        _player1 = GameObject.Find("Main Camera").GetComponent<MouseLook>();
        //controll = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLevelChanger>().num;

    }

 
    void Update()
    {

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        if (_charController.isGrounded)
            if (Input.GetKeyDown(KeyCode.Space)) 
                force_down = -4.5f;

        if (_charController.isGrounded && force_down > 0) force_down = 1;
        else force_down += 10 * Time.deltaTime;

        velocity = ((_transform.forward * deltaZ + _transform.right * deltaX + Vector3.down * force_down) * Time.deltaTime);
        _charController.Move(velocity);

        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            speed = 12;
            child.fieldOfView += 1;
            //Debug.Log(speed);
        }
        else
        {
            child.fieldOfView -= 1;
            speed = 6;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (flag)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _player.enabled = false;
                _player1.enabled = false;
                Time.timeScale = 0;
                flag = !flag;           
            }
            else
            {
                Time.timeScale = 1;
                flag = !flag;
                _player.enabled = true;
                _player1.enabled = true;
            }      
        }

        child.fieldOfView = Mathf.Clamp(child.fieldOfView, 60, 75);
        _charController.height = Input.GetKey(KeyCode.LeftControl) == true ? (_charController.height /= 2) : (_charController.height = 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Respawn")
        {
            controll += 1;
            SceneManager.LoadScene(controll);
        }
        else if (other.tag == "Fall")
        {
            controll = 1;
            SceneManager.LoadScene(0);
        }
    }
}
