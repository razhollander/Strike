using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MagneticPinShot : BasicPinShot
{
    const float MAX_DISTANCE_PERCENT = 2;
    const float ZERO = 0;
    const float ONE = 1;

    [Header("MagneticPinShot")]
    [SerializeField] MagnetLaserStrike _magnetLaserStrike;
    [SerializeField] float _radius = 10;
    [SerializeField] float _pullAmount;

    bool isPulling = true;
    List<ObejctPulled> _objectsPulledList;
    GameManager gm;
    private void Awake()
    {
        _objectsPulledList = new List<ObejctPulled>();
    }
    private void Start()
    {
        gm = GameManager.Instance;
    }
    private void Update()
    {
        if (isPulling)
        {
            FindObjectsInRadius();
            UpdateEffects();
        }
    }
    private void FixedUpdate()
    {
        if (isPulling)
        {
            PullObjects();
        }
    }

    protected override IEnumerator DestroySelf(float delay = 0)
    {
        foreach (var pulledObject in _objectsPulledList)
        {
            pulledObject.MagnetLaserStrike.gameObject.SetActive(false);
        }

        _objectsPulledList.Clear();

        return base.DestroySelf(delay);
    }

    protected override void SetComponents(bool isEnabled)
    {
        isPulling = isEnabled;
        base.SetComponents(isEnabled);
    }
    private void UpdateEffects()
    {
        Vector3 pos = transform.position;

        foreach (var pulledObject in _objectsPulledList)
        {
            pulledObject.MagnetLaserStrike.transform.position = pos;
            pulledObject.MagnetLaserStrike.transform.LookAt(pulledObject.SuckableObject.transform);
            float _lazerAlpha = Mathf.Clamp(MAX_DISTANCE_PERCENT * (_radius - pulledObject.Distance) / _radius, ZERO, ONE);
            pulledObject.MagnetLaserStrike.SetAlpha(_lazerAlpha);
        }
    }
    private void PullObjects()
    {
        Vector3 pos = transform.position;

        foreach (var pulledObject in _objectsPulledList)
        {
            Vector3 force = (pos-pulledObject.SuckableObject.transform.position).normalized * _pullAmount / pulledObject.Distance;
            pulledObject.SuckableObject.AddForce(force);
        }
    }
    private void FindObjectsInRadius()
    {
        var suckableObjects = gm.GetSuckableObjects();
        Vector3 pos = transform.position;

        List<ObejctPulled> newObjectsPulledList = new List<ObejctPulled>();

        foreach (var suckableObject in suckableObjects)
        {
            float distance = Vector3.Distance(suckableObject.transform.position, pos);
            var objectPulled = _objectsPulledList.Find(x => x.SuckableObject == suckableObject);
            bool isAlreadyInList = objectPulled != null;
            if (distance < _radius)
            {
                if (!isAlreadyInList)
                {
                    if (suckableObject.IsActive)
                    {
                        var newPulledObject = new ObejctPulled(suckableObject, _magnetLaserStrike.Get<MagnetLaserStrike>(null, true), distance);
                        newPulledObject.MagnetLaserStrike.Target = suckableObject.transform;
                        newObjectsPulledList.Add(newPulledObject);
                    }
                }
                else
                {
                    objectPulled.Distance = distance;
                }
            }
            else
            {
                if (isAlreadyInList)
                {
                    objectPulled.Disable();
                    _objectsPulledList.Remove(objectPulled);
                }
            }
        }

        newObjectsPulledList.AddRange(_objectsPulledList);
        _objectsPulledList.Clear();

        foreach (var objectPulled in newObjectsPulledList)
        {
            if(objectPulled.SuckableObject.IsActive)
            {
                _objectsPulledList.Add(objectPulled);
            }
            else
            {
                objectPulled.Disable();
            }
        }
    }
    private class ObejctPulled
    {
        public SuckableObject SuckableObject;
        public MagnetLaserStrike MagnetLaserStrike;
        public float Distance;

        public ObejctPulled(SuckableObject suckableObject, MagnetLaserStrike magnetLaserStrike, float distance)
        {
            SuckableObject = suckableObject;
            MagnetLaserStrike = magnetLaserStrike;
            Distance = distance;
        }
        public void Disable()
        {
            MagnetLaserStrike.gameObject.SetActive(false);
        }
    }
}

