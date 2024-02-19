using UnityEngine;

public class GameCubeThrower : MonoBehaviour
{
  public GameStateChanger  GameStateChanger;  // Скрипт изменения состояния игры
  public GameCube          GameCubePrefab;    // Префаб для создания кубика
  private GameCubeAnimator  CubeThrowAnimator; // Скрипт анимации кубика

  private int      _cubeValue;    // Значение, которое выпало на кубике
  private GameCube _gameCube;     // Созданный объект кубика
 
  void Start()
  {
    CubeThrowAnimator = GetComponent<GameCubeAnimator>();
    CreateGameCube();   // Создаём новый кубик при запуске игры
  }

  private void CreateGameCube()
  {
    Transform GameCubePoint = CubeThrowAnimator.GetTransform();
    _gameCube = Instantiate(GameCubePrefab, GameCubePoint.position, GameCubePoint.rotation, GameCubePoint);    // Создаём новый кубик в указанной позиции и с указанным углом вращения
    _gameCube.HideCube();                                                                                      // Скрываем кубик, чтобы его не было видно в начале игры
  }

  public void ThrowCube()
  {
    _cubeValue = _gameCube.ThrowCube();       // Получаем случайное значение броска кубика
    CubeThrowAnimator.PlayAnimation(this);    // Проигрываем анимацию броска
  }

  public void ContinueAfterCubeAnimation() {
    GameStateChanger.DoPlayerTurn(_cubeValue);      // Передаём значение, которое выпало на кубике, в скрипт изменения состояния игры
  }
}
