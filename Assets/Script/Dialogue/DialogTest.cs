using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogTest : MonoBehaviour
{
	public GameObject DialoguePanel;
	public bool fadechk;
	public string SceneToLoad;
	
	[SerializeField]
	private	DialogSystem	dialogSystem01;
	
	//[SerializeField]
	//private	TextMeshProUGUI	textCountdown;
	public Image FadeOut;

	public GameObject Day_StoryDialogue;

	private IEnumerator Start()
	{
		
		//scene2.gameObject.SetActive(false);
		//scene3.gameObject.SetActive(false);

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
		dialogSystem01.gameObject.SetActive(true);
		yield return new WaitUntil(()=>dialogSystem01.UpdateDialog());

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
		
		
		
		//textCountdown.gameObject.SetActive(true);
		//textCountdown.text = "The End";
		
		if (fadechk)
        {
			FadeOut.gameObject.SetActive(true);
			yield return new WaitForSeconds(2);
			//UnityEditor.EditorApplication.ExitPlaymode();
			SceneManager.LoadScene(SceneToLoad);
		}
		
		Day_StoryDialogue.gameObject.SetActive(false);
		DialoguePanel.SetActive(false);




	}
}

