using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]

public class ProgressBarCircle : MonoBehaviour {
    [Header("Title Setting")]
    public string Title;
    public Color TitleColor;
    public Font TitleFont;    

    [Header("Bar Setting")]
    public Color BarColor;
    public Color BarBackGroundColor;
    public Color MaskColor;
    public Sprite BarBackGroundSprite;
    [Range(1f, 100f)]
    public int Alert = 50;
    public Color BarAlertColor;

    [Range(1f, 100f)]
    public int Alert2 = 20;
    public Color BarAlertColor2;

    [Header("Sound Alert")]
    public AudioClip sound;
    public bool repeat = false;
    public float RepearRate = 1f;

    [Header("Sound Alert2")]
    public AudioClip sound2;
    public bool repeat2 = false;
    public float RepearRate2 = 1f;

    [Header("Hurt Sound")]
    public AudioSource hurt1;
    public AudioSource hurt2;

    private Image bar, barBackground,Mask;
    private float nextPlay;
    private AudioSource audiosource;
    private Text txtTitle;
    public float barValue = 100;
    public float totaltime = 0;
    public bool flag1 = false;
    public bool flag2 = false;

    public float BarValue
    {
        get { return barValue; }

        set
        {
            value = Mathf.Clamp(value, 0, 100);
            barValue = value;
            UpdateValue(barValue);

        }
    }

    private void Awake()
    {

        txtTitle = transform.Find("Text").GetComponent<Text>();
        barBackground = transform.Find("BarBackgroundCircle").GetComponent<Image>();
        bar = transform.Find("BarCircle").GetComponent<Image>();
        audiosource = GetComponent<AudioSource>();
        Mask= transform.Find("Mask").GetComponent<Image>();
    }

    private void Start()
    {
        txtTitle.text = Title;
        txtTitle.color = TitleColor;
        txtTitle.font = TitleFont;
       

        bar.color = BarColor;
        Mask.color = MaskColor;
        barBackground.color = BarBackGroundColor;
        barBackground.sprite = BarBackGroundSprite;

        UpdateValue(barValue);


    }

    void UpdateValue(float val)
    {
        bar.fillAmount = -(val / 100) + 1f;

        txtTitle.text = Title + " " + val + "%";

        if (Alert >= val && Alert2 < val)
        {
            barBackground.color = BarAlertColor;
        }
        else if (Alert2 >= val)
        {
            barBackground.color = BarAlertColor2;
        }
        else
        {
            barBackground.color = BarBackGroundColor;
        }
        if (!flag1)
        {
            if (barValue == Alert)
            {
                hurt1.Play();
                flag1 = true;
            }
        }
        if (!flag2) 
        {
           if (barValue == Alert2) { hurt2.Play(); flag2 = true; }
        }
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {

            UpdateValue(100);
            txtTitle.color = TitleColor;
            txtTitle.font = TitleFont;
            Mask.color = MaskColor;
            bar.color = BarColor;
            barBackground.color = BarBackGroundColor;
            barBackground.sprite = BarBackGroundSprite;

        }
        else
        {
            if (!Inventory.inventoryActivated && !PauseMenu.is_Pause)
            {
                totaltime += Time.deltaTime;
                if (totaltime > 1 && barValue > 0)
                {
                    UpdateValue(--barValue);
                    totaltime = 0;
                }
                if (Alert >= barValue && Alert2 < barValue && Time.time > nextPlay)
                {
                    nextPlay = Time.time + RepearRate;
                    audiosource.PlayOneShot(sound);
                }
                else if (Alert2 >= barValue && Time.time > nextPlay)
                {
                    nextPlay = Time.time + RepearRate2;
                    audiosource.PlayOneShot(sound2);
                }
            }
            else
            {
                if (barValue > 100) barValue = 100;
                UpdateValue(barValue);
            }
        }
    }

}