using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    public bool initialized;
    public long rewardGivingTimeTicks;
    public GameObject rewardMenu;
    public Text remaininTimeText; 

    private void Update()
    {
        if (initialized)
        {
            if(LevelController.instance.startMenu.activeInHierarchy)
            {
                long currentTime = System.DateTime.Now.Ticks;
                long remainingTime = rewardGivingTimeTicks - currentTime;
                if(remainingTime <= 0)
                {
                    GiveReward();
                }
                else
                {
                    System.TimeSpan timeSpan = System.TimeSpan.FromTicks(remainingTime);
                    remaininTimeText.text = string.Format("{0}:{1}:{2}", timeSpan.Hours.ToString("D2"), timeSpan.Minutes.ToString(), timeSpan.Seconds.ToString());
                }
            }
        }
    }
    public void InitiakizeDailyReward()
    {
        if (PlayerPrefs.HasKey("LastDailyReward"))
        {
            //Gets last last taken time
            rewardGivingTimeTicks = long.Parse(PlayerPrefs.GetString("lastDailyReward")) + 864000000000;
            long currentTime = System.DateTime.Now.Ticks;
            if(currentTime >= rewardGivingTimeTicks)
            {
                GiveReward();
            }
        }
        else
        {
            GiveReward();
        }


        initialized = true;
    }
    public void GiveReward()
    {
        LevelController.instance.GiveMoneyToPlayer(100);
        rewardMenu.SetActive(true);
        PlayerPrefs.SetString("lastDailyReward",System.DateTime.Now.Ticks.ToString());
        rewardGivingTimeTicks = long.Parse(PlayerPrefs.GetString("lastDailyReward")) + 864000000000;
    }
    public void TapToReturnButton()
    {
        rewardMenu.SetActive(false);
    }
}
