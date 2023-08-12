using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image content;

    private float currentFill;

    [SerializeField]
    private Text valueText;//当前血量文本

    [SerializeField]
    private float lerpSpeed;//血条缓降速度

    public float MyMaxValue { get; set; }//最大值

    private float currentValue;//当前值

    public float MyCurrentValue 
    {
        get
        {
            return currentValue;
        }
        set
        {
            if (value > MyMaxValue) //检查当前生命值是否大于最大生命值，如果大于就将他设置为最大生命值，即确保生命值不会超过最大值
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue;

            valueText.text = currentValue + "/" + MyMaxValue;//设置当前血量文本值
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
        //content.fillAmount = currentValue / MyMaxValue;
    }

    public void Initialize(float currentValue,float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
