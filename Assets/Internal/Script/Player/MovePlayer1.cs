using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovePlayer1 : MonoBehaviour
{
    private Rigidbody rb;
    Transform child;
    GameObject childObject;
    public float rotationSpeed = 10f; // ȸ�� �ӵ�

    public float power = 10000f;
    public float maxSpeed = 8.0f;
    public float duration = 3f;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        child = transform.GetChild(0); ;
        childObject = child.gameObject;
    }

    /// <summary>
    /// �÷��̾��� �⺻���� Ű �Է� ó��
    /// </summary>
    void Update()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;

        // Ű �Է� ó��
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveHorizontal = -1; // ���� �̵�
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveHorizontal = 1; // ������ �̵�
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVertical = 1; // ���� �̵�
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVertical = -1; // �Ʒ��� �̵�
        }

        // ���� ���� ���
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // �̵� �������� ȸ��
        if (movement != Vector3.zero)
        {
            // �̵� �������� ȸ���ϰ� Y�� �������� 90�� ȸ��
            Quaternion targetRotation = Quaternion.LookRotation(movement) * Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // �ε巯�� ȸ��
        }

        // ���� ����
        rb.AddForce(movement * power * Time.deltaTime, ForceMode.Force);

        // �ִ� �ӵ� ����
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}