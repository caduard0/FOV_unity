using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast_FOV : MonoBehaviour{
    Vector2 origin;
    [SerializeField] float FOV = 180;
    [SerializeField] float MAG = 1;
    [SerializeField] int DEN = 3;
    

    RaycastHit2D[] hit;

    void Awake()
    {
        hit = new RaycastHit2D[DEN];
    }


    void FixedUpdate(){
        origin = transform.position;

        CalcFOVLines(origin, FOV, MAG, DEN);
    }


    void CalcFOVLines(Vector2 _origin, float _FOV, float _MAG = 1, int _DEN = 3){
        if(_DEN < 3){ _DEN = 3; }

        Vector2[] DIR = new Vector2[_DEN];
        float[] _fov_arr = new float[_DEN];

        var deg_self_rot = transform.eulerAngles.z * Mathf.Deg2Rad;
        float _fov = _FOV / 2 * Mathf.Deg2Rad;

        var step = _fov * 2 / (_DEN - 1);
        var curr = -_fov;

        for (int i = 0; i < _DEN; i++){
            _fov_arr[i] = curr;
            curr = curr + step;

            DIR[i] = to_xy_dir(_fov_arr[i] + deg_self_rot);

            hit[0] = Physics2D.Raycast(_origin, DIR[i], _MAG);

            if (hit[0].collider != null){
                Debug.DrawRay(_origin, DIR[i] * _MAG, Color.red);
            } else {
                Debug.DrawRay(_origin, DIR[i] * _MAG, Color.green);
            }
        }
    }

    float to_ang_dir(Vector2 xy_dir){
        return Mathf.Atan(xy_dir.x / xy_dir.y);
    }

    Vector2 to_xy_dir(float ang_dir){
        return new Vector2(Mathf.Cos(ang_dir), Mathf.Sin(ang_dir));
    }
}
