using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSeq : MonoBehaviour {
	public Image IntroImage;
	public string DesignatedLevel;

	public List<RectTransform> LineMasks;

	public Image PressAnyButton;

	private List<Vector2> LMStartPos; //Line Mask Initial Positions
	private List<Vector2> LMEndPos; //Line Mask Final Positions
	public float MoveDistance = 842.1f;
	public float LMMoveDuration = 3;
	private bool Animating;
	private IEnumerator Animation;

	public List<Sprite> IntroSeqs;
	private int pointer = 0;
	public List<int> DialogueLines;
	

	// Use this for initialization
	void Start () {
		IntroImage.sprite = IntroSeqs [pointer];

		LMStartPos = new List<Vector2>();
		LMEndPos = new List<Vector2>();
		for (int i = 0; i < LineMasks.Count; i++) {
			LMStartPos.Add(LineMasks[i].position);
			Vector2 EndPos = new Vector2 ((LMStartPos[i].x + MoveDistance), LMStartPos[i].y);
			LMEndPos.Add(EndPos);
		}

		Animating = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Animating){
			Animation = AnimateLines();
			StartCoroutine(Animation);
		}
		if ((Input.anyKeyDown) && (!Animating) && (Animation == null)){
			NextSeq();
		}
		if (Input.GetKeyUp(KeyCode.Escape))
			SceneManager.LoadScene(DesignatedLevel, LoadSceneMode.Single);
	}

	IEnumerator AnimateLines(){
		Animating = false;
		PressAnyButton.enabled = true;
		for(int i = 0; i < DialogueLines[pointer]; i++)
			yield return StartCoroutine(AnimateType(i, LineMasks[i], LMStartPos[i], LMEndPos[i], LMMoveDuration));
		PressAnyButton.enabled = false;
		Animation = null;
	}

    IEnumerator AnimateType(int LMIndex, RectTransform LineMask, Vector2 origin, Vector2 target, float duration) {
		float journey = 0f;
		while (journey <= duration) {
			journey = journey + Time.deltaTime;
			float percent = Mathf.Clamp01(journey / duration);
			LineMask.position = Vector2.Lerp(origin, target, percent);
			yield return null;
		}
	}

	void ResetLineMasks(){
		for(int i = 0; i < LineMasks.Count; i++)
			LineMasks[i].position = LMStartPos[i];
	}

	void NextSeq(){
		Animating = true;
		ResetLineMasks();
		pointer += 1;
		if (pointer >= IntroSeqs.Count)
			SceneManager.LoadScene(DesignatedLevel, LoadSceneMode.Single);
		IntroImage.sprite = IntroSeqs[pointer];
	}
}
