using UnityEngine;

// ���� ������Ʈ�� ��� �������� �����̴� ��ũ��Ʈ
public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f; // �̵� �ӵ�
    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.instance.isGameover)
        {
            // ���� ������Ʈ�� ���� �ӵ��� �������� �����̵��ϴ� ó��
            // �ʴ� speed�� �ӵ��� �������� �����̵�
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
