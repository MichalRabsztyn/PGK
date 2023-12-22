using UnityEngine;

public class WeaponMovementController : MonoBehaviour
{
    public float swayAmount = 0.02f;
    public float swaySpeed = 2f;
    public float recoilAmount = 0.1f;
    public float recoilSpeed = 5f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 swayOffset = new Vector3(-mouseX, -mouseY, 0) * swayAmount;
        Vector3 targetSwayPosition = originalPosition + swayOffset;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetSwayPosition, Time.deltaTime * swaySpeed);     
    }

    public void HandleRecoil(bool bShot)
    {
        if (bShot)
        {
            Vector3 recoilOffset = new Vector3(0, 0, -recoilAmount);
            Vector3 recoilPosition = Vector3.Lerp(transform.localPosition, originalPosition + recoilOffset, Time.deltaTime * recoilSpeed);
            transform.localPosition = recoilPosition;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * recoilSpeed);
        }
    }
}
