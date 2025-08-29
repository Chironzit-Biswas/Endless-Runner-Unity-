using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using Unity.Mathematics;
using UnityEngine.Rendering;
using System;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 3f;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] public float boostSpeed;
    [SerializeField] private float xInput;
    [SerializeField] private Rigidbody rb;

    private float[] checkpoint = { 100f, 200f, 400f, 600f };


    public int zposition_score=0;
    public TextMeshProUGUI zPositionscoreText;

    
    public int coin_score = 0;
    public TextMeshProUGUI coinScoreText;






    void Start()
    {
      rb = GetComponent<Rigidbody>();
        boostSpeed = runSpeed * 2;

        //zposition_score Ui Display 
        zposition_score = 0;
        zPositionscoreText.text = zposition_score .ToString();
        //zposition_score Ui Display 

        //coin_score Ui Display
        coin_score = 0;
        coinScoreText.text = coin_score.ToString();
        //coin_score Ui Display
    }


    void FixedUpdate()
    {
       move();
    }

    void move()
    {
        xInput = Input.GetAxis("Horizontal");
        transform.Translate(xInput * speed * Time.deltaTime, 0, runSpeed * Time.deltaTime);
        zposition_score++;
        zPositionscoreText.text = zposition_score.ToString();
        powerUp();
    }

    

   void powerUp()
    {
        float zPos = transform.position.z;

        for (int i = 0; i < checkpoint.Length; i++)
        {   
            if (zPos > checkpoint[i])
            {
                float multiplier = i + 2; // 100f ? x2, 200f ? x3, 400f ? x4, 600f ? x5
                transform.Translate(xInput * speed * Time.deltaTime, 0, runSpeed * multiplier * Time.deltaTime);
                Debug.Log("Boost Speed Activated x" + multiplier + " | Speed: " + boostSpeed);
            }
        }
        




        //if (transform.position.z > 100f)
        //{
        //    transform.Translate(xInput * speed * Time.deltaTime, 0, runSpeed * 2 * Time.deltaTime);
        //    Debug.Log("Boost Speed Activated" + boostSpeed);
        //}
        //if (transform.position.z > 200f)
        //{
        //    transform.Translate(xInput * speed * Time.deltaTime, 0, runSpeed * 3 * Time.deltaTime);
        //    Debug.Log("Boost Speed Activated" + boostSpeed);
        //}
        //if (transform.position.z > 400f)
        //{
        //    transform.Translate(xInput * speed * Time.deltaTime, 0, runSpeed * 4 * Time.deltaTime);
        //    Debug.Log("Boost Speed Activated" + boostSpeed);
        //}
        //if (transform.position.z > 600f)
        //{
        //    transform.Translate(xInput * speed * Time.deltaTime, 0, runSpeed * 5 * Time.deltaTime);
        //    Debug.Log("Boost Speed Activated" + boostSpeed);
        //}
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag== "coin")
        {
        Destroy(other.gameObject, 0.1f);
            coin_score++;
            coinScoreText.text =coin_score.ToString();
            Debug.Log("coin collecting "+coin_score);
        }
    }
}
