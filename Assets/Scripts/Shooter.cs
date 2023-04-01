using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    public int damage = 25;
    public int hit_distance = 10;
    // Start is called before the first frame update
    private Camera _camera;
    private int bullet = 0;
    private bool _is_reload = false;

    private GameObject world;
    void Start()
    {
        world = GameObject.FindGameObjectWithTag("GameController");
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        bullet = 30;
    }
    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size/2;
        float posY = _camera.pixelHeight / 2 - size/2;
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.green;
        style.fontSize = 20;
        GUI.Label(new Rect(posX, posY, size, size), "O", style); // Костыль
        GUI.Label(new Rect(_camera.pixelWidth - size - 30, _camera.pixelHeight - size- 30, size + 50, size + 50), this.bullet.ToString(), style);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            if (Physics.Raycast(ray, out RaycastHit hit, hit_distance) && !_is_reload)
            {
                GameObject hitObject = hit.transform.gameObject;
                Debug.Log(hit.transform.name);
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null && hitObject.CompareTag("Enemy"))
                {
                    target.ReactToHit(damage);
                    StartCoroutine(SphereIndicator(hit.point));
                }
                else
                {
                    
                }
                StartCoroutine(decrement_bullet());
                Debug.Log(this.bullet);
            }
        }
         
    }
    private IEnumerator decrement_bullet()
    {
        if (this.bullet >= 1)
        {
            this.bullet -= 1;
        }
        else
        {
            _is_reload = true;
            yield return new WaitForSeconds(2);
            this.bullet = 30;
            _is_reload = false;
        }
    }
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = (GameObject)Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), pos, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }
}
