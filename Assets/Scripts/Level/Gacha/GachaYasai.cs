using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaYasai : MonoBehaviour
{
    // 1秒の移動速度
    [SerializeField]
    float _speed = 1;

    Vector3 _startPos;
    Vector3 _goalPos;
    Vector3 _dir;

    const int Height = 540;
    const int Width = 960;
    const int Mergin = 20;

    public void Init()
    {
        _startPos = new Vector3(Random.Range(-(Width / 2 + Mergin), Width / 2 + Mergin), Random.Range(-(Height / 2 + Mergin), Height / 2 + Mergin));
        _goalPos = new Vector3(Random.Range(-(Width / 2 + Mergin), Width / 2 + Mergin), Random.Range(-(Height / 2 + Mergin), Height / 2 + Mergin));
        _dir = (_goalPos - _startPos).normalized;
        transform.localPosition = _startPos;

        var scale = Random.Range(0.1f, 1f);
        transform.localScale = new Vector3(scale, scale, 1);
    }

    public void Move()
    {
        transform.localPosition = transform.localPosition + _dir * _speed * Time.deltaTime;

        var x = transform.localPosition.x;
        var y = transform.localPosition.y;

        // 画面端まで来たら再設定
        if (x < -Width / 2 || Width / 2 < x || y < -Height / 2 || Height / 2 < y)
        {
            _startPos = transform.localPosition;
            _goalPos = new Vector3(Random.Range(-(Width / 2 + Mergin), Width / 2 + Mergin), Random.Range(-(Height / 2 + Mergin), Height / 2 + Mergin));
            _dir = (_goalPos - _startPos).normalized;
        }
    }
}
