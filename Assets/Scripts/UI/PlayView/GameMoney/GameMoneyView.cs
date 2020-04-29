using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameMoneyView : Countable
{
    [SerializeField] private Ease _ease = Ease.InSine;
    [SerializeField] Image _coinImage;
    [SerializeField] float animationTime = 0.5f;
    [SerializeField] ParticleSystem collectEffect;

    private Transform _playViewTransform;
    private Transform PlayViewTransform
    {
        get
        {
            if (_playViewTransform == null)
            {
                _playViewTransform = GameObject.FindGameObjectWithTag(PlayView.PLAY_CANVAS_TAG).transform;
                return _playViewTransform;
            }
            else
                return _playViewTransform;
        }
    }
    public void StartAddEffect(int moneyValue, Vector3 startPos)
    {
        StartCoroutine(StartAddEffectCoroutin(moneyValue, startPos));
    }
    private IEnumerator StartAddEffectCoroutin(int moneyValue, Vector3 startPos)
    {
        Image img = Instantiate(_coinImage);
        Vector3 endPos = Vector3.zero;

        img.transform.SetParent(transform);
        img.GetComponent<RectTransform>().position = CameraManager.instance.MainCamera.WorldToScreenPoint(startPos);
        img.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        img.transform.DOScale(1, animationTime).SetEase(Ease.OutExpo);
        img.transform.DOLocalRotate(new Vector3(0,360*4,0), animationTime, RotateMode.LocalAxisAdd).SetEase(_ease);

        img.transform.DOLocalMove(endPos, animationTime).SetEase(_ease);
        yield return new WaitForSeconds(animationTime);
        Destroy(img.gameObject);
        EndEffect();
        SetNumber(moneyValue);
    }
    private void EndEffect()
    {
        collectEffect.Play();
    }
}
