using UnityEngine;

public class PlayerChipsAnimator : MonoBehaviour
{
  //public GameField GameField;                // Скрипт игрового поля
  public float CellMoveDuration = 0.3f;      // Длительность перемещения между ячейками

  private GameStateChanger GameStateChanger;  // Скрипт изменения состояния игры
  private PlayerChip  _playerChip;           // Префаб фишки текущего игрока
  private bool        isAnimatedNow;          // Флаг, который указывает, выполняется ли сейчас анимация
  private Vector2[]   _movementCells;        // Массив координат, по которым нужно переместиться
  private int         _currentCellId;        // Индекс текущей ячейки, которую анимируют
  private float       _cellMoveTimer;        // Временной счётчик для анимации
  private Vector2     _startPosition;        // Начальная позиция перемещения
  private Vector2     _endPosition;          // Конечная позиция перемещения
  
  // Update is called once per frame
  void Update()
  {
    Animation();      // Вызываем метод управления анимацией
  }

  public void AnimateChipMovement(PlayerChip playerChip, Vector2[] movementCells)
  {
    GameStateChanger = FindObjectOfType<GameStateChanger>();
    SetPlayerChipAndMovementCellsArray(playerChip, movementCells);
    isAnimatedNow  = true;          // Задаём флаг, который указывает, что анимация идёт сейчас
    _currentCellId = -1;            // Устанавливаем начальное значение текущей ячейки
    ToNextCell();                   // Начинаем движение к следующей ячейке
  }

  private void SetPlayerChipAndMovementCellsArray(PlayerChip playerChip, Vector2[] movementCells)
  {
    _playerChip    = playerChip;    // Сохраняем переданную фишку в переменную
    _movementCells = movementCells; // Получаем массив ячеек, через которые фишка должна пройти
  }

  private void Animation()
  {
    // Если анимация сейчас не выполняется
    if (!isAnimatedNow) {
      return;   // Выходим из метода
    }

    chipArrivedToNextCell();
    MovingChipByDurationTime();
  }

  private void MovingChipByDurationTime()
  {
    _playerChip.SetPosition(Vector2.Lerp(_startPosition, _endPosition, _cellMoveTimer));    // Вычисляем промежуточную позицию фишки между начальной и конечной позицией
    _cellMoveTimer += Time.deltaTime / CellMoveDuration;                                    // Увеличиваем таймер на основе прошедшего времени
  }

  private void ToNextCell()
  {
    _currentCellId++;     // Увеличиваем текущий номер ячейки на 1

    // Если текущий номер ячейки больше или равен общему количеству ячеек - 1
    if (_currentCellId >= _movementCells.Length - 1) {
      EndAnimation();     // Завершаем анимацию
      return;             // Выходим из метода
    }

    SetNewStartAndEndChipPosition();
    
    _cellMoveTimer = 0;   // Сбрасываем таймер перемещения на 0
  }

  private void EndAnimation()
  {
    isAnimatedNow = false;                               // Задаём флагу isAnimateNow значение false, то есть указываем, что анимация завершилась
    GameStateChanger.ContinueGameAfterChipAnimation();   // Продолжаем игру после анимации фишки с помощью функции ContinueGameAfterChipAnimation() из класса GameStateChanger
  }

  private void chipArrivedToNextCell()
  {
    // Если таймер движения фишки достиг значения 1
    if (_cellMoveTimer >= 1) {
      ToNextCell(); // Переходим к следующей ячейке
    }
  }

  private void SetNewStartAndEndChipPosition()
  {
    _startPosition = _movementCells[_currentCellId];   // Получаем начальную позицию для анимации из класса GameField с помощью текущего номера ячейки
    _endPosition = _movementCells[_currentCellId + 1]; // Получаем конечную позицию для анимации из класса GameField с помощью следующего номера ячейки
  }
}
