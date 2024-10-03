using UnityEngine;

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
				instance = FindObjectOfType<T>();

				// 그마저도 존재하지 않으면 T의 이름을 갖는 게임 오브젝트를 만들어준다
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
		// 스크립트 인스턴스가 로드될 때 instance가 없었다면 DontDestroyOnLoad에 넣는다
		if (instance == null)
		{
			instance = this as T;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			// instance가 이미 존재하고 그것이 자신과 같은 인스턴스가 아니면 자신을 파괴한다
			if (instance != this)
			{
				Destroy(gameObject);
			}
		}
	}
}