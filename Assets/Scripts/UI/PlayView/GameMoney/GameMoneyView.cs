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
    [SerializeField] float scalePunchAmount = 1;
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
    public void StartEffectCorutine(int moneyValue, Vector3 startPos)
    {
        StartCoroutine(StartAddEffect(moneyValue, startPos));
    }
    public IEnumerator StartAddEffect(int moneyValue, Vector3 startPos)
    {
        GameObject img = Instantiate(_coinImage.gameObject, transform);
        Vector3 endPos = Vector3.zero;
        yield return new WaitForEndOfFrame();

        Vector2 thisPointScreenSpace = CameraManager.instance.MainCamera.WorldToScreenPoint(((RectTransform)transform).position);
        Vector2 spawnPointScreenSpace = CameraManager.instance.MainCamera.WorldToScreenPoint(startPos);
        ((RectTransform)img.transform).localPosition = spawnPointScreenSpace - thisPointScreenSpace;
        img.GetComponent<RectTransform>().localRotation = Quaternion.identity;
        img.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        ParticleSystem collectEffect = img.transform.GetComponentInChildren<ParticleSystem>();

        img.transform.DOLocalMove(endPos, animationTime).SetEase(_ease);
        img.transform.DOScale(1, animationTime).SetEase(Ease.OutExpo);
        img.transform.DOLocalRotate(new Vector3(0, 360 * 4, 0), animationTime, RotateMode.LocalAxisAdd).SetEase(_ease);

        yield return new WaitForSeconds(animationTime);

        collectEffect.Play();
        SetNumber(moneyValue);
        img.transform.DOPunchScale(Vector3.one, 0.4f, 1, scalePunchAmount).OnComplete(() => Destroy(img));
    }
}
