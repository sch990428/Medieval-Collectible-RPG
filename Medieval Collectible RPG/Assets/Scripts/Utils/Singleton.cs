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
                instance = (T)FindObjectOfType<T>();

                // 그마저도 존재하지 않으면 T의 이름과 타입을 갖는 게임 오브젝트를 만들어준다
                // 그리고 T를 컴포넌트로 추가한다
                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                }
            }

			return instance;
        }
    }

	public virtual void Awake()
	{
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
				Debug.Log($"{typeof(T).Name}을 파괴합니다");
				Destroy(gameObject);
            }
        }
	}
}
