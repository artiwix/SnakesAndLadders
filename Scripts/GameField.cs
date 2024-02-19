using UnityEngine;

public class GameField : MonoBehaviour
{
  
  public Vector2 CellSize;              // Размер ячейки (по X и Y)
  public int CellsCount = 100;          // Общее количество ячеек на игровом поле
  public int CellsInRow = 10;           // Количество ячеек в одном ряду

  private Transform FirstCellPoint = null;      // Позиция первой ячейки
  private Vector2[]   _cellsPositions;  // Массив из позиций каждой ячейки
  private Vector2[,] _cellsPositions2;  // Массив из позиций каждой ячейки
  private TransitionSettings _transitionSettings;      // Скрипт с настройками переходов

  public void FillCellsPositions()
  {
    _transitionSettings = transform.Find("TransitionSettings").GetComponent<TransitionSettings>();

    FirstCellPoint = transform.GetChild(0);
    _cellsPositions = new Vector2[CellsCount];// Создаём массив с размером, равным общему количеству ячеек

    float xSign = 1;        // Заводим переменную, которая отслеживает, где создаются новые ячейки (они будут добавляться вправо и влево)
    _cellsPositions[0] = FirstCellPoint.position; // Делаем позицию первой ячейки в массиве равной заданной позиции первой ячейки

    // Проходим по каждой ячейке, начиная со второй
    for (int i = 1; i < _cellsPositions.Length; i++)
    {
      bool needUp = i % CellsInRow == 0;  // Узнаём, нужно ли подниматься на новый ряд ячеек

      if (needUp)  // Если нужно подниматься на новый ряд
      {
        xSign *= -1;  // Меняем направление движения на противоположное
        _cellsPositions[i] = _cellsPositions[i - 1] + Vector2.up * CellSize.y; // Позиция ячейки получается путём смещения на высоту одной ячейки вверх
      }
      else    // Если не нужно подниматься на новый ряд:
      { 
        float deltaX = xSign * CellSize.x;                                     // Смещение по горизонтали равно ширине одной клетки, умноженной на знак смещения
        _cellsPositions[i] = _cellsPositions[i - 1] + Vector2.right * deltaX;  // Позиция ячейки определяется, когда мы смещаем её на указанное значение по горизонтали
      }
    }
  }

  public Vector2 GetCellPosition(int id)
  {
    // Если номер ячейки некорректный
    if (id < 0 || id >= _cellsPositions.Length) {
      return Vector2.zero;       // Возвращаем нулевые значения (0, 0)
    }
    return _cellsPositions[id];  // Иначе возвращаем позицию ячейки
  }

  public Vector2[] GetArrayOfCoord(int[] idsArray)
  {
    Vector2[] cellsPositionArray = new Vector2[idsArray.Length];
    for(int index = 0; index < idsArray.Length; index++)
    {
      cellsPositionArray[index] = GetCellPosition(idsArray[index]);
    }
    return cellsPositionArray;
  }

  public int GetTransitionResultCellId(int startCellId)
  {
    return _transitionSettings.GetTransitionResultCellId(startCellId);
  }
}
