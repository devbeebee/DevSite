using UnityEngine;

public class PlayerCamera : Singleton<PlayerCamera>
{
    public bool GamePaused = false;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    private Vector2 mouseLook;
    private Vector2 smoothV;
    private Transform player;
    public Vector3 cashedPosition;
    bool NotFirstperson = false;
    protected override void Awake()=>  base.Awake();

    public void MoveCameraAndReParent(Transform newparet)
    {
        NotFirstperson = true;
        transform.parent = newparet;
        transform.localPosition = Vector3.zero;
        transform.localRotation = newparet.localRotation;
    }
    public void ResetCamera()
    {
        NotFirstperson = false;
        transform.parent = player;
        transform.localPosition = cashedPosition;
    }
    void Start()
    {
        cashedPosition = transform.localPosition;
           player = transform.parent;
    }
    private void Update()
    {
        if (GamePaused || NotFirstperson)
        {
            return;
        }
        Vector2 md = new Vector2(NewInput.Instance.GetMouseAxis(InputAxis.X), NewInput.Instance.GetMouseAxis(InputAxis.Y));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.localRotation = Quaternion.AngleAxis(mouseLook.x, player.up);
        
    }
}
