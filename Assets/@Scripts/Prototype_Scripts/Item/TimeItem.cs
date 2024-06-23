using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeDropItem : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    [SerializeField] private PlayerController playerController;

    // 아이템 획득 시 실행할 액션을 위한 델리게이트 선언
    public delegate void OnItemPickup();
    public static event OnItemPickup ItemPickedUp; // 아이템 획득 시 호출할 이벤트

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        playerController = playerObj.GetOrAddComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌한 경우
        {
            Pickup(); // 아이템을 획득하는 함수 호출
        }
    }

    protected virtual void Pickup()
    {
        // 아이템을 획득하는 로직 작성
        Debug.Log("아이템을 획득했습니다!");

        // 아이템을 획득한 후에 실행할 작업들...       

        // 아이템 획득 이벤트 호출
        ItemPickedUp?.Invoke();

        // 아이템 오브젝트 비활성화
        gameObject.SetActive(false);
    }

    // 아이템 드랍 방식
    private void ItemDropLogic()
    {
        // playerController.

    }
}
