using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
	public Text hintText;
	public Text secretText;
    public GameObject Wall1;
    public GameObject Wall2;
	public bool HintState;
	public string[] Hints;
	public int HintCount;
    private Rigidbody rb;
    private int count;
	private int scount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
		SetSecretText();
        winText.text = "";
		UpdateHintState();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.AddForce(movement * speed);
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
		
		if (Input.GetKeyDown(KeyCode.K))
        {
            if (HintState == false)
			{
				HintState = true;
			}
			else
			{
				HintState = false;
			}
			UpdateHintState();
        }
		
		if (Input.GetKeyDown(KeyCode.J))
        {
			HintState = true;
            HintCount += 1;
			if (HintCount >= Hints.Length)
			{
				HintCount -= Hints.Length;
			}
			UpdateHintState();
        }
		
		if (Input.GetKeyDown(KeyCode.H))
        {
			HintState = true;
            HintCount -= 1;
			if (HintCount < 0)
			{
				HintCount += Hints.Length;
			}
			UpdateHintState();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
		
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            if (count == 5)
            {
                Wall1.gameObject.SetActive(false);
            }

            if (count == 10)
            {
                Wall2.gameObject.SetActive(false);
            }
        }
		if (other.gameObject.tag == "Secret")
		{
			other.gameObject.SetActive(false);
            scount = scount + 1;
			SetSecretText();
		}
		if (other.gameObject.tag == "danger")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
	
	void SetCountText()
	{
		countText.text = "Points: " + count.ToString();
		if (count == 15)
		{
			winText.text = "WW, CD!\nPress R to restart, Escape to exit";
		}
	}
	
	void SetSecretText()
	{
		if (scount != 0)
		{
			secretText.text = "Secrets: " + scount.ToString();
		}
		else
		{
			secretText.text = "";
		}
	}
	
	void UpdateHintState()
	{
		if (HintState == true)
		{
			hintText.text = Hints[HintCount] + "\n" + (1 + HintCount).ToString() + "/" + Hints.Length.ToString();
		}
		else
		{
			hintText.text = "";
		}
	}
}