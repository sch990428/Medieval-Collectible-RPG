using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 싱글톤 패턴을 사용할 모든 게임 오브젝트가 상속하는 제너릭 싱글톤 클래스
// MonoBehaviour를 상속하는 모든 클래스에 대해 적용할 수 있다
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            // 만일 싱글톤 객체가 연결되지 않았다면 T의 타입을 갖는 게임 오브젝트를 먼저 찾아본다
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                // 그마저도 존재하지 않으면 T의 이름과 타입을 갖는 게임 오브젝트를 만들어준다
                // 그리고 T를 컴포넌트로 추가한다
                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name, typeof(T));
                    instance = go.AddComponent<T>();
                }
            }

            return instance;
        }
    }

	private void Awake()
	{
        // 부모 객체가 존재하는 경우에는 부모까지 포함하여 파괴되지않도록 설정
        if (transform.parent != null && transform.root != null)
        {
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
	}
}
