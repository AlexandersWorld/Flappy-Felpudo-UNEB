using UnityEngine;
using System.Collections;

public class MoveFundo : MonoBehaviour {

  float larguraTela;

  SpriteRenderer grafico;

    [SerializeField] private float offsetSpeed = 1.0f;

  void Start(){
  
    grafico = GetComponent<SpriteRenderer>();

  }

  void Update(){
      
    float offset = Time.time * offsetSpeed;

    grafico = GetComponent<SpriteRenderer>();

    grafico.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
 
  }


}