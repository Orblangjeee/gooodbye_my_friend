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

		// ù ��° ��� �б� ����
		yield return new WaitUntil(()=>dialogSystem_choice.UpdateDialog());

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

