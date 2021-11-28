using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ConfineLineHandler : MonoBehaviour
{
     [SerializeField] private ConfineLine[] confineLines;
     [SerializeField] private float lerpSpeed = 1.0f;
     [SerializeField] private float spaceLine = 8f;
     
     public ConfineLine[] GetConFineLines() => confineLines;
     public GameObject LineObject;

     //public AnimationCurve lerpCurve;

     /// <summary>
     /// 패턴을 해제합니다.
     /// </summary>
     public void UnlockLine()
     {
          for (int i = 0; i < confineLines.Length; i++)
          {
               Destroy(confineLines[i].transform.gameObject);
          }
     }

     /// <summary>
     /// 플레이어 주변에 패턴을 생성합니다.
     /// </summary>
     public void ActiveLine(Vector2 activePos)
     {
          for (int i = 0; i < confineLines.Length; i++)
          {
               confineLines[i].OnActive();
          }
     }

     private List<GameObject> lines = new List<GameObject>();
     /// <summary>
     /// 테스트 코드 => 라인 오브젝트를 규칙성있게 생성합니다.
     /// </summary>
     public void LineObjectCreate(Vector2 pos, float offset)
     {
          var createPos = pos;
          
          // Square
          lines.Add(Instantiate(LineObject, new Vector2(pos.x - offset, pos.y ), Quaternion.Euler(0, 0, 0)));
          lines.Add(Instantiate(LineObject, new Vector2(pos.x + offset, pos.y), Quaternion.Euler(0, 0, 180)));
          lines.Add(Instantiate(LineObject, new Vector2(pos.x, pos.y - offset ), Quaternion.Euler(0, 0, 90)));
          lines.Add(Instantiate(LineObject, new Vector2(pos.x, pos.y + offset), Quaternion.Euler(0, 0, 270)));

          StartCoroutine(CreateLineEffect(100));
     }

     /// <summary>
     /// 사각형을 시각적인 효과를 더해 활성화합니다.
     /// </summary>
     /// <param name="toOffset"></param>
     /// <returns></returns>
     private IEnumerator CreateLineEffect(float fromOffset)
     {
          float curOffeset = fromOffset;
          var pos = FindObjectOfType<PlayerObject>().transform.position;

          while (curOffeset >= spaceLine + 0.05f)
          {
               curOffeset = Mathf.Lerp(curOffeset, spaceLine, Time.deltaTime * lerpSpeed);
               
               lines[0].transform.position = new Vector2(pos.x - curOffeset, pos.y);
               lines[1].transform.position = new Vector2(pos.x + curOffeset, pos.y);
               lines[2].transform.position = new Vector2(pos.x, pos.y - curOffeset);
               lines[3].transform.position = new Vector2(pos.x, pos.y + curOffeset);

               yield return null;
          }
     }
}
