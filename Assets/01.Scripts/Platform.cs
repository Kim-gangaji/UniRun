using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;
    bool stepped;
    int count;

    private void OnEnable()
    {
        count = 0;
        stepped = false;

        for (int i = 0; i < obstacles.Length; i++)
        {
            if (Random.Range(0, 3) == 2)
            {
                obstacles[i].SetActive(true);
                count++;
                //count = count + 1; count += 1; ++count;
            }
            else
                obstacles[i].SetActive(false);
        }
    }

    //충돌
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!stepped && coll.transform.tag == "Player")
        {
            stepped = true;
            int newScore = count + 1;
            GameManager.instance.AddScore(newScore);
        }
    }
}
