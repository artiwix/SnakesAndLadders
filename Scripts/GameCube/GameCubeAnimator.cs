using UnityEngine;

public class GameCubeAnimator : MonoBehaviour
{
  private GameCubeThrower GameCubeThrower;        // Скрипт бросков кубика
  private Animation CubeAnimation = null;         // Ссылка на компонент анимации кубика

  void Start()
  {
    CubeAnimation = GetComponent<Animation>();
  }
  // Проигрываем анимацию
  public void PlayAnimation(GameCubeThrower gameCubeThrower) {
    GameCubeThrower = gameCubeThrower;
    CubeAnimation.Play(); 
  }

  // Этот метод мы вызовем позже внутри анимации
  public void OnAnimationEnd() { GameCubeThrower.ContinueAfterCubeAnimation(); }  // Продолжаем действия после анимации

  public AnimationClip GetAnimationClip() { return CubeAnimation.clip; }
  public void SetAnimationClip(AnimationClip animationClip) { CubeAnimation.clip = animationClip; }
  public Transform GetTransform() { return transform; }
}
