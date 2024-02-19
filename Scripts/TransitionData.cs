using UnityEngine;

public class TransitionData : MonoBehaviour
{
  public IntPair[] CellsTransitionPairsIds;

  public int GetTransitionResultCellId(int cellId)
  {
    // Проходим в цикле по всем начальным ячейкам
    for (int i = 0; i < CellsTransitionPairsIds.Length; i++)
    {
      if (CellsTransitionPairsIds[i].Start == cellId) { // Если номер начальной ячейки совпадает с переданным
        return CellsTransitionPairsIds[i].End;  // Возвращаем номер конечной ячейки для этой змеи или лестницы
      } 
    }
    return -1;    // Если условие не выполнилось, возвращаем -1 — это значит, что перемещения по змее или лестнице нет
  }

  [System.Serializable]
  public class IntPair
  {
    public int Start;
    public int End;

    public IntPair(int first, int second)
    {
      Start = first;
      End = second;
    }
  }
}
