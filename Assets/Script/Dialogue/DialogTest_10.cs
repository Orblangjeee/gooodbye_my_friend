using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogTest_10 : MonoBehaviour
{
	

	[SerializeField]
	private DialogSystem_9 dialogSystem_choice;
	[SerializeField]
	private GameObject choiceBtn;

	//[SerializeField]
	//private	TextMeshProUGUI	textCountdown;


	private IEnumerator Start()
	{

		
		yield return new WaitForSeconds(2);
		
		//int count = 1;
		//while (count > 0)
		//{
		//	textCountdown.text = count.ToString();
		//	count--;

		//	yield return new WaitForSeconds(1);
		//}
		//textCountdown.gameObject.SetActive(false);

		// 첫 번째 대사 분기 시작
		yield return new WaitUntil(()=>dialogSystem_choice.UpdateDialog());

		// 대사 분기 사이에 원하는 행동을 추가할 수 있다.
		// 캐릭터를 움직이거나 아이템을 획득하는 등의.. 현재는 5-4-3-2-1 카운트 다운 실행
		//textCountdown.gameObject.SetActive(true);
		//int count = 5;
		//while ( count > 0 )
		//{
		//	textCountdown.text = count.ToString();
		//	count --;

		//yield return new WaitForSeconds(1);
		//}
		//textCountdown.gameObject.SetActive(false);

		//두 번째 대사 분기 시작
		choiceBtn.gameObject.SetActive(true);

		//textCountdown.gameObject.SetActive(true);
		//textCountdown.text = "The End";

		//yield return new WaitForSeconds(2);

		//UnityEditor.EditorApplication.ExitPlaymode();

	}
	public void StoryRout()
	{
		Debug.Log("oneplus2");

		DataManager.instance.nowPlayer.storyScore += 1;
	}
}

