using UnityEngine;

public class TransitionSettings : MonoBehaviour
{
  public int GetTransitionResultCellId(int startCellId)
  {
    // Проходим в цикле по всему массиву с данными о перемещениях по змеям и лестницам
    for (int i = 0; i < transform.childCount; i++)
    {
      int resultCellId = transform.GetChild(i).GetComponent<TransitionData>().GetTransitionResultCellId(startCellId);  // Получаем новый номер ячейки из TransitionData для данного значения startCellId
      
      // Если resultCellId больше или равен 0
      if (resultCellId >= 0) {
        return resultCellId;  // Значит, перемещение возможно
      }
    }
    
    return -1;  // Если условие не выполнилось, возвращаем -1 — это значит, что перемещения по змее или лестнице нет 
  }
}
