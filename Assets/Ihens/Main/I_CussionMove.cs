using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_CussionMove : IhenBase
{
    [Header("Cussion Move")]
    [SerializeField] private List<Transform> _cussionList = new List<Transform>();
    [SerializeField] private float _moveSpeed = 5f;    
    [SerializeField] private List<float> _targetYPos = new List<float>();
    [SerializeField] private Transform _target;
    private Player player;
    private Vector3 _targetPos;
    private bool? _fase = null;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        _targetPos = _target.position;
    }

    
    void Update()
    {
        if(player.IsMove && player.IsCurpet)
        {
            int goalCount = 0;
            for(int i = 0; i < _cussionList.Count; i++)
            {
                if(_fase == true) return;
                else if(_fase == null)
                {
                    _cussionList[i].position = Vector3.MoveTowards(_cussionList[i].position, _targetPos, _moveSpeed * Time.deltaTime);
                    if(Vector3.Distance(_cussionList[i].position, _targetPos) < 0.1f)
                    {
                        goalCount++;
                    }
                }
                else
                {
                    Vector3 targetPos = _targetPos + new Vector3(0, _targetYPos[i], 0);
                    _cussionList[i].position = Vector3.MoveTowards(_cussionList[i].position, targetPos, _moveSpeed * Time.deltaTime);
                    if(Vector3.Distance(_cussionList[i].position, targetPos) < 0.1f)
                    {
                        goalCount++;
                    }
                }
            }

            if(goalCount == _cussionList.Count && _fase == null)
            {
                _fase = false;
            }
            else if(goalCount == _cussionList.Count && _fase == false)
            {
                _fase = true;
            }
        }
    }

    
}
