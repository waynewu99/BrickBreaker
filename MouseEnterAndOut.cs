using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEnterAndOut : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {

	public static bool MouseOnButton = false;

	public void OnPointerEnter (PointerEventData eventData){
		MouseOnButton = true;
	}
	public void OnPointerExit (PointerEventData eventData){
		MouseOnButton = false;
	}

}
