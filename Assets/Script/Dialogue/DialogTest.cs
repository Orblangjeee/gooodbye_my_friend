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

		// ù ��° ��� �б� ����
		dialogSystem01.gameObject.SetActive(true);
		yield return new WaitUntil(()=>dialogSystem01.UpdateDialog());

		// ��� �б� ���̿� ���ϴ� �ൿ�� �߰��� �� �ִ�.
		// ĳ���͸� �����̰ų� �������� ȹ���ϴ� ����.. ����� 5-4-3-2-1 ī��Ʈ �ٿ� ����
		//textCountdown.gameObject.SetActive(true);
		//int count = 5;
		//while ( count > 0 )
		//{
		//	textCountdown.text = count.ToString();
		//	count --;

		//yield return new WaitForSeconds(1);
		//}
		//textCountdown.gameObject.SetActive(false);

		//�� ��° ��� �б� ����
		
		
		
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

