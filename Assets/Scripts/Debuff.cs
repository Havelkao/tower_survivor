using System;
using System.Collections;
using UnityEngine;

public class Debuff : MonoBehaviour
{
    public DebuffableProp prop;
    public float value;
    public float duration;
    private Func<float> getAction;
    private Action<float> setAction;

    public void SetCallbacks(Func<float> getter, Action<float> setter)
    {
        getAction = getter;
        setAction = setter;
    }

    public IEnumerator ApplyWithRevert(IDebuffable debuffable)
    {
        setAction(getAction() - value);
        yield return new WaitForSeconds(duration);
        setAction(getAction() + value);
        debuffable.RemoveDebuff(this);
    }
}