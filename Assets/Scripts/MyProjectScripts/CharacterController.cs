using UnityEngine;

public class CharacterController : MonoSingleton<CharacterController>
{
    private float _lastFrameFingerPosX;
    private float _moveFactorX;

    [SerializeField] private float speedX;
    public float speedZ;

    [SerializeField] private float xMax;
    [HideInInspector] public float velocity = 0f;

    public float smoothTime = 0.1F;

    private float smoothX = 0;

    private void Update()
    {
        MoveHorizontal();
        MoveVertical();
    }

    private void MoveHorizontal()
    {
        transform.Translate(transform.forward * speedZ * Time.deltaTime);
    }

    private float swerveAmount;

    private void MoveVertical()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPosX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPosX;
            _lastFrameFingerPosX = Input.mousePosition.x;

            swerveAmount += _moveFactorX * speedX * Time.deltaTime;
            swerveAmount = Mathf.Clamp(swerveAmount, -xMax, xMax);

            smoothX = Mathf.SmoothDamp(smoothX, swerveAmount, ref velocity, smoothTime);
            transform.position = new Vector3(swerveAmount, transform.position.y, transform.position.z);
        }
    }
}