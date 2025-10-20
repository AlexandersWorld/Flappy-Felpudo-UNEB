using UnityEngine;

public class DirectionalLighting : MonoBehaviour
{
    private Light _light;
    [SerializeField] private float transitionDuration = 2f;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    private void Start()
    {
        if (_light != null)
        {
            _light.color = Color.white;
        }
    }

    public void TurnLightRed()
    {
        if (_light != null)
        {
            StopAllCoroutines();
            StartCoroutine(TransitionToColor(Color.red));
        }
    }

    private System.Collections.IEnumerator TransitionToColor(Color targetColor)
    {
        Color startColor = _light.color;
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / transitionDuration);
            _light.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        _light.color = targetColor;
    }
}
