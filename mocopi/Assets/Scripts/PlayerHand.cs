using UnityEngine;

public class PlayerHand : MonoBehaviour, IHandHolder
{
    [SerializeField] private Transform handTransform;
    public Transform HandTransform => handTransform;

    public GameObject HeldItem { get; private set; } = null;

    public void AttachItem(GameObject item)
    {
        if (item == null) return;

        HeldItem = item;
        item.transform.SetParent(handTransform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    public void DetachItem()
    {
        if (HeldItem == null) return;

        HeldItem.transform.SetParent(null);
        Rigidbody rb = HeldItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        HeldItem = null;
    }

    /// <summary>
    /// ���ݕێ����Ă���A�C�e�����A�w��̃^�[�Q�b�g�ʒu�ɓ����鏈���B
    /// throwForce �̒l�ɉ����āA���B���邽�߂̔��ˊp�x���v�Z���܂��B
    /// </summary>
    /// <param name="target">������̃^�[�Q�b�g Transform</param>
    /// <param name="force">������́i�����j</param>
    public void ThrowItem(Transform target, float force)
    {
        if (HeldItem == null || target == null) return;

        GameObject itemToThrow = HeldItem;
        HeldItem = null;
        itemToThrow.transform.SetParent(null);

        Rigidbody rb = itemToThrow.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = itemToThrow.AddComponent<Rigidbody>();
        }
        rb.isKinematic = false;

        // ���݂̎茳�ʒu����^�[�Q�b�g�܂ł̕ψʂ����߂�
        Vector3 displacement = target.position - handTransform.position;
        // ���������̂݁iY�����������j
        Vector3 horizontalDisplacement = new Vector3(displacement.x, 0, displacement.z);
        float d = horizontalDisplacement.magnitude;  // ��������
        float h = displacement.y;                      // �����̍�
        float v = force;                               // �����Ƃ��Ă� throwForce
        float g = -Physics.gravity.y;                  // �d�͉����x�i���̒l�j

        // ���ʎ����v�Z�Fv^4 - g*(g*d^2 + 2*h*v^2)
        float disc = v * v * v * v - g * (g * d * d + 2 * h * v * v);
        if (disc < 0)
        {
            // ������͂����肸�^�[�Q�b�g�ɓ��B�ł��Ȃ��ꍇ�́A�ʏ�̕����ɗ͂�������
            Debug.LogWarning("�w��̗͂ł̓^�[�Q�b�g�ɓ��B�ł��܂���B");
            rb.AddForce((target.position - handTransform.position).normalized * v, ForceMode.Impulse);
            return;
        }

        float sqrtDisc = Mathf.Sqrt(disc);
        // ��O���̉���I�ԁiv^2 - sqrtDisc�j
        float angle = Mathf.Atan((v * v - sqrtDisc) / (g * d));

        // �����x�N�g�����v�Z�F���������Ɛ��������ɕ�����
        Vector3 initialVelocity = horizontalDisplacement.normalized * (v * Mathf.Cos(angle)) + Vector3.up * (v * Mathf.Sin(angle));
        rb.AddForce(initialVelocity, ForceMode.Impulse);
    }
}
