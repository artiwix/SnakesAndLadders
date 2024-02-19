using UnityEngine;

public class AutoPlayer : MonoBehaviour
{
    public PlayerChipsAnimator PlayersChipsAnimator;
    public GameCubeAnimator CubeThrowAnimator;
    public GameStateChanger GameStateChanger;
    public GameCubeThrower GameCubeThrower;

    public float NewCellMoveDuration = 0.01f;

    private float _oldCellMoveDuration;
    private AnimationClip _oldCubeThrowAnimationClip;

    private bool _isActive;

    [ContextMenu("StartAutoPlay")]
    private void StartAutoPlay()
    {
        _oldCellMoveDuration = PlayersChipsAnimator.CellMoveDuration;
        _oldCubeThrowAnimationClip = CubeThrowAnimator.GetAnimationClip();

        PlayersChipsAnimator.CellMoveDuration = NewCellMoveDuration;
        CubeThrowAnimator.SetAnimationClip(null);

    _isActive = true;
    }

    [ContextMenu("StopAutoPlay")]
    private void StopAutoPlay()
    {
        PlayersChipsAnimator.CellMoveDuration = _oldCellMoveDuration;
        CubeThrowAnimator.SetAnimationClip(_oldCubeThrowAnimationClip);

        _isActive = false;
    }

    private void Update()
    {
        DoAutoPlay();
    }

    private void DoAutoPlay()
    {
        if (!_isActive)
        {
            return;
        }

        if(!GameStateChanger.ThrowButton.interactable)
        {
            return;
        }
        GameCubeThrower.ThrowCube();
        CubeThrowAnimator.OnAnimationEnd();
    }
}
